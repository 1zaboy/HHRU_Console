import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { Action, ActionKey } from '../models/action';

export class ActionEvent<T = any> {
  timeStamp: number;
  data: T;
}

@Injectable({
  providedIn: 'root'
})
export class ActionService {
  private _actionEvents: { [actionKey: number]: Subject<ActionEvent> } = {};
  private _actionEvents$: { [actionKey: number]: Observable<ActionEvent> } = {};

  constructor() {
    for (const key of Object.keys(ActionKey).filter(k => !Number.isNaN(ActionKey[k]))) {
      this._actionEvents[ActionKey[key]] = new Subject<ActionEvent>();
      this._actionEvents$[ActionKey[key]] = this._actionEvents[ActionKey[key]].asObservable();
    }
  }

  pinToActionEvent(key: ActionKey) {
    return this._actionEvents$[key];
  }

  makeEvent(action: Action, data?: any) {
    const targetSubject: Subject<ActionEvent> = this._actionEvents[action.actionKey];

    const d = data ? data : action.data;

    if (targetSubject !== null) {
      targetSubject.next({ timeStamp: action.timeStamp, data: d });
    }
  }
}
