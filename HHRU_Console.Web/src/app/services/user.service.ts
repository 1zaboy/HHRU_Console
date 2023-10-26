import { Injectable } from '@angular/core';
import { ApiService } from '../common/services/api.service';
import { BehaviorSubject, Observable, Subject, map, tap } from 'rxjs';
import { User } from '../models/user';
import { StorageService } from '../common/services/storage.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  protected userLoaded = new BehaviorSubject<User>(null);
  userLoaded$ = this.userLoaded.asObservable().pipe(tap(x => this._storage.localStorageSetItem('user', x)));

  constructor(
    private readonly _api: ApiService,
    private readonly _storage: StorageService,
  ) { }

  loadUser(): void {
    this._api.loadUser()
      .subscribe({
        next: (x) => {
          this.userLoaded.next(x);
        },
        error: (err) => {
          this.userLoaded.next(null);
        }
      })
  }

  logout(): Observable<void> {
    return this._api.logout();
  }
}
