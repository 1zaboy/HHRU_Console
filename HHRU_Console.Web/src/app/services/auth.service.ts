import { Injectable } from '@angular/core';
import { ApiService } from '../common/services/api.service';
import { StorageService } from '../common/services/storage.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public readonly RETURN_CODE = "code"
  public readonly AUTH_TOKEN = "AUTH_TOKEN"

  constructor(
    private readonly _api: ApiService,
    private readonly _storage: StorageService,
    private readonly _router: Router,
  ) { }

  private auth() {
    const loginData: any = this._storage.localStorageGetItem('LOGIN_DATA');
    window.location.href = `https://hh.ru/oauth/authorize?client_id=${loginData.clientId}&response_type=code&state=${loginData.state}`;
  }

  authRedirect(code: string, state: string) {
    this._api.auth(code, state).subscribe(x => {
      this._storage.localStorageSetItem(this.AUTH_TOKEN, x);
      this._router.navigate(["/overview"]);
    });
  }

  signIn() {
    this.auth();
  }

  signOut() {
    this._storage.localStorageClear();
  }

  check(): boolean {
    const user = localStorage.getItem(this.AUTH_TOKEN);
    return user != null;
  }
}
