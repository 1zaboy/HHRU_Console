import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { ConfigurationService } from 'src/app/configuration/configuration.service';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user';
import { Grid } from '../models/grid';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private readonly LOGIN_REDIRECT = 'api/Account/loginredirect';
  private readonly LOGOUT = 'api/Account/logout';
  private readonly LOAD_USER = 'api/Account/me';
  private readonly LOAD_RESPONSES = 'api/Response';

  protected baseUrl: string;

  constructor(
    readonly _configService: ConfigurationService,
    private readonly _http: HttpClient,
  ) {

    this.baseUrl = _configService.settings?.api.url ?? '';
  }

  logout() {
    return this._http.get(`${this.baseUrl}${this.LOGOUT}`);
  }

  auth(code: string, state: string): Observable<string> {
    return this._http.get(`${this.baseUrl}${this.LOGIN_REDIRECT}`, { responseType: 'text', params: { code: code, state: state } });
  }

  loadUser(): Observable<User> {
    return this._http.get<User>(`${this.baseUrl}${this.LOAD_USER}`);
  }

  loadResponses(): Observable<Grid> {
    return this._http.get<Grid>(`${this.baseUrl}${this.LOAD_RESPONSES}`);
  }
}
