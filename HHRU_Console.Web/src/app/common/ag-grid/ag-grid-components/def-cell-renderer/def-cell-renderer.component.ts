import { ChangeDetectionStrategy, Component, OnDestroy } from '@angular/core';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { AgDataGridHelper } from '../../ag-data-grid/ag-data-grid.helper';
import { Subject } from 'rxjs';
import { LocaleFormats } from '../../../models/localeFormats';
import { DEFAULT_FORMATS } from 'src/app/common/models/localeMap';
import { ICellRendererParams } from 'ag-grid-community';

type ValueType = string | number | Date;

@Component({
  selector: 'app-def-cell-renderer',
  templateUrl: './def-cell-renderer.component.html',
  styleUrls: ['./def-cell-renderer.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DefCellRendererComponent implements OnDestroy, ICellRendererAngularComp {
  helper: AgDataGridHelper;
  value: ValueType;

  selectable: boolean;
  clickable: boolean;
  editable: boolean;


  private readonly _destroy$ = new Subject<void>();

  local: string;
  locales: LocaleFormats;

  constructor() {
    this.locales = DEFAULT_FORMATS;
    this.local = ''
  }

  refresh(params: ICellRendererParams): boolean {
    return true;
  }

  ngOnDestroy(): void {
    this._destroy$.next();
    this._destroy$.complete();
    this.helper = null;
  }

  agInit(params: ICellRendererParams): void {
    this.value = params.getValue();

    this.clickable = false;
    this.selectable = false;
    this.editable = true;
  }

  get needView(): boolean {
    return this.value != null;
  }
}
