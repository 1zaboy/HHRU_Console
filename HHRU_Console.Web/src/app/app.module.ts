import { NgDompurifySanitizer } from "@tinkoff/ng-dompurify";
import { TuiRootModule, TuiDialogModule, TuiAlertModule, TUI_SANITIZER } from "@taiga-ui/core";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ConfigurationService } from './configuration/configuration.service';
import { ErrorInterceptor } from './_helpers/error.interceptor';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './pages/identity/login/login.component';
import { OverviewComponent } from './pages/overview/overview.component';
import { AuthRedirectComponent } from './pages/identity/auth-redirect/auth-redirect.component';
import { AuthInterceptor } from './_helpers/auth.interceptor';
import { MenuComponent } from './common/components/menu/menu.component';
import { SelfComponent } from './common/components/self/self.component';
import { TuiAvatarModule } from "@taiga-ui/kit";
import { AgDataGridModule } from "./common/ag-grid/ag-data-grid.module";

const appInitializerFn = (appConfig: ConfigurationService) => {
  return () => {
    return appConfig.loadConfig();
  };
};

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    OverviewComponent,
    AuthRedirectComponent,
    MenuComponent,
    SelfComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    TuiRootModule,
    TuiDialogModule,
    TuiAlertModule,
    TuiAvatarModule,
    AgDataGridModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    ConfigurationService,
    { provide: APP_INITIALIZER, useFactory: appInitializerFn, multi: true, deps: [ConfigurationService] },
    { provide: TUI_SANITIZER, useClass: NgDompurifySanitizer }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
