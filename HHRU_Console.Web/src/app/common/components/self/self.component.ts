import { Component, OnInit } from '@angular/core';
import { filter, map } from 'rxjs';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-self',
  templateUrl: './self.component.html',
  styleUrls: ['./self.component.scss']
})
export class SelfComponent implements OnInit {
  constructor(
    private readonly _user: UserService,
  ) { }

  get userEmail() {
    return this._user.userLoaded$.pipe(
      filter(x => x != null),
      map(x => x.email)
    );
  }

  ngOnInit(): void {
    this._user.loadUser();
  }

  onLogout() {
    this._user.logout().subscribe();
  }
}
