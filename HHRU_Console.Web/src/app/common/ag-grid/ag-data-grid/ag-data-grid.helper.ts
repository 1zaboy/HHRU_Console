import { Observable, Subject } from 'rxjs';
import { Injectable } from '@angular/core';
import { DataManager } from './managers/data-manager';
import { Layout } from '../../models/grid';

export interface IHelperStorage {
  cellsSelected$: Observable<void>;
  outsideChanges$: Observable<void>;
  filterChanged$: Observable<void>;
  checkValueChanged$: Observable<void>;
  styleChanged$: Observable<number[]>;
  sizeChanged$: Observable<string[]>;

  dataManager: DataManager;
}

export class HelperStorage implements IHelperStorage {
  _cellsSelected = new Subject<void>();
  _outsideChanges = new Subject<void>();
  _filterChanged = new Subject<void>();
  _checkValueChanged = new Subject<void>();
  _styleChanged = new Subject<number[]>();
  _sizeChanged = new Subject<string[]>();

  cellsSelected$ = this._cellsSelected.asObservable();
  outsideChanges$ = this._outsideChanges.asObservable();
  filterChanged$ = this._filterChanged.asObservable();
  checkValueChanged$ = this._checkValueChanged.asObservable();
  styleChanged$ = this._styleChanged.asObservable()
  sizeChanged$ = this._sizeChanged.asObservable()

  dataManager: DataManager;
}


@Injectable()
export class AgDataGridHelper extends HelperStorage {

  constructor(private _layout: Layout) {
    super();
    this.dataManager = new DataManager(_layout, []);

    this._layout.gridHelper = this;

    this._cellsSelected.next();
  }

  removeLink(): void {
    this._layout.gridHelper = null;
  }
}
