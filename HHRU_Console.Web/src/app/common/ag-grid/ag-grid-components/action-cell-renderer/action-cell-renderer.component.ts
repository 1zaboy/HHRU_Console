import { ChangeDetectionStrategy, Component, NgZone } from '@angular/core';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { Action, ActionIconMap } from 'src/app/common/models/action';
import { ActionService } from 'src/app/common/services/action.service';

@Component({
  selector: 'app-action-cell-renderer',
  templateUrl: './action-cell-renderer.component.html',
  styleUrls: ['./action-cell-renderer.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ActionCellRendererComponent implements ICellRendererAngularComp {

  action: Action;
  tag: number | string | any;
  key: string;
  needClickEvent: boolean;
  customData: any;

  constructor(
    private readonly _actions: ActionService,
    private readonly _zone: NgZone
  ) {
    //
  }

  agInit(params: any): void {
    this.action = params.action;
    this.tag = params.data.tag;
    this.key = params.data.key;
    this.customData = params.action.data;
    this.needClickEvent = params.needClickEvent;
  }

  refresh(): boolean {
    return true;
  }

  get display(): any {
    return (!!this.tag && (this.tag.editable === undefined || this.tag.editable))
      || (!!this.key && this.customData);
  }

  getActionIcon(): string {
    return ActionIconMap[this.action.actionKey];
  }

  onGridAction(): void {
    this._zone.run(() => {
      this._actions.makeEvent(this.action, {tag: this.tag, key: this.key, customData: this.customData});
    });
  }
}
