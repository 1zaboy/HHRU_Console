import { Component, OnInit } from '@angular/core';
import { Action, ActionKey, ActionType } from 'src/app/common/models/action';
import { Grid } from 'src/app/common/models/grid';
import { ActionService } from 'src/app/common/services/action.service';
import { ApiService } from 'src/app/common/services/api.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss']
})
export class OverviewComponent implements OnInit {
  grid: Grid = null;

  get user() {
    return this._user.userLoaded$;
  }

  constructor(
    private readonly _actionService: ActionService,
    private readonly _user: UserService,
    private readonly _api: ApiService,
  ) {
    _actionService.pinToActionEvent(ActionKey.VIEW_VACANCY)
      .subscribe(x => {
        window.open(`${x.data.customData}`)
      });
  }

  ngOnInit(): void {
    this._api.loadResponses().subscribe(x => {
      x.layout.actions = [
        new Action(ActionType.action, ActionKey.VIEW_VACANCY, "View Vacancy", null, 0),
      ];
      this.grid = x;
    });
  }
}
