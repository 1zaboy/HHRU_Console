import { Injectable, NgZone } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { StorageService } from '../common/services/storage.service';
import { AuthService } from '../services/auth.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(
    private readonly _storage: StorageService,
    private readonly _router: Router,
    private readonly _authService: AuthService,
  ) {
    //
  }

  async canActivate(_route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
    var token = this._storage.localStorageGetItem(this._authService.AUTH_TOKEN);

    if (token != null) {
      return true;
    }

    this._storage.localStorageClear();
    this._router.navigate(['/login']);
    return false;
  }
}
