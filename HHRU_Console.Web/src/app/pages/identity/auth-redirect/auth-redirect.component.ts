import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { map, forkJoin, of as just } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-auth-redirect',
  template: '',
})
export class AuthRedirectComponent implements OnInit {

  constructor(
    private readonly _authService: AuthService,
    private readonly _route: ActivatedRoute,
    private readonly _router: Router,
  ) {

    const a = this._route.queryParams
      .pipe(map(x => [x[this._authService.RETURN_CODE], x['state']]))
      .subscribe(([code, state]) => {
        this._authService.authRedirect(code, state);
      });
  }

  ngOnInit(): void {
  }
}
