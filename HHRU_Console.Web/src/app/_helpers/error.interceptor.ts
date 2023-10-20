import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { StorageService } from '../common/services/storage.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private readonly _router: Router,
    private readonly _storage: StorageService,
  ) {
    //
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(err => {
      if (err.status === 401) {
        this._storage.localStorageClear();
        this._storage.localStorageSetItem('LOGIN_DATA', err.error);
        this._router.navigate(['/login']);
      }

      const error = err.message;
      return throwError(error);
    }))
  }
}
