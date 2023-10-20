import { Injectable } from '@angular/core';
import { HttpBackend, HttpClient, HttpErrorResponse } from '@angular/common/http';

@Injectable()
export class ConfigurationService {

  private _httpClientForConfig: HttpClient;

  private _settings: IAppSettings | null = null;
  get settings(): IAppSettings | null {
    return this._settings;
  }

  constructor(httpBackend: HttpBackend) {
    this._httpClientForConfig = new HttpClient(httpBackend);
  }

  loadConfig(): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      if (this._settings != null) {
        resolve();
      }

      this._httpClientForConfig.get<IAppSettings>('/assets/appconfig.json')
        .subscribe({
          next: (response) => {
            if (response.api.url == null) {
              response.api.url = document.getElementsByTagName('base')[0].href;
            }
            this._settings = response;
            resolve();
          },
          error: (response: HttpErrorResponse) => {
            reject(`Can't load config: ${response.message} (${JSON.stringify(response.error)})`);
          }
        })
    });
  }
}

export interface IAppSettings {
  api: {
    url: string;
  };
}
