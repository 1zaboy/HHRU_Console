import { Component, OnInit } from '@angular/core';
import { StorageService } from 'src/app/common/services/storage.service';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(
    private readonly _storage: StorageService,
    private readonly _auth: AuthService,
    private readonly _user: UserService,
  ) { }

  ngOnInit(): void {
    const token = this._storage.localStorageGetItem('LOGIN_DATA');
    if (token != null) {
      this._auth.signIn();
    } else {
      this._user.loadUser();
    }
  }
}
