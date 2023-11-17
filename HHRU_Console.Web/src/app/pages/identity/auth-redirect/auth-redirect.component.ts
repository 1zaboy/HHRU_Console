import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-auth-redirect',
  template: '',
})
export class AuthRedirectComponent {

  constructor(
    private readonly _authService: AuthService,
    private readonly _route: ActivatedRoute,
  ) {
    this._route.queryParams
      .pipe(map(x => [x[this._authService.RETURN_CODE], x['state']]))
      .subscribe(([code, state]) => {
        this._authService.authRedirect(code, state);
      });
  }
}
