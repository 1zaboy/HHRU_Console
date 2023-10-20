import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'HHRU_Console.Web';
  constructor(
    private readonly _auth: AuthService,
  ) {}

  get authUser() {
    return this._auth.check();
  }
}
