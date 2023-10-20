import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
  constructor(
    private readonly _actionService: ActionService,
    private readonly _user: UserService,
    private readonly _api: ApiService,
    private readonly _reouter: Router,
  ) {
    _actionService.pinToActionEvent(ActionKey.VIEW_VACANCY)
      .subscribe(x => {
        console.log(x)
        window.open(`${x.data.customData}`)
      })

  }

  get user() {
    return this._user.userLoaded$;
  }

  grid: Grid = null;

  ngOnInit(): void {
    this._user.loadUser();

    this._api.loadResponses().subscribe(x => {
      x.layout.actions = [
        new Action(ActionType.action, ActionKey.VIEW_VACANCY, "View Vacancy", null, 'https://hh.ru/vacancy/82344096'),
      ];
      this.grid = x;
    });
  }

  logout() {
    this._api.logout().subscribe();
  }

  getMe() {
    this._api.loadResponses().subscribe(x => {
      console.log(x);
    });
  }

}
