import { NgDompurifySanitizer } from "@tinkoff/ng-dompurify";
import { TuiRootModule, TuiDialogModule, TuiAlertModule, TUI_SANITIZER, TuiSvgModule } from "@taiga-ui/core";
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
import { TuiAvatarModule, TuiCheckboxBlockModule } from "@taiga-ui/kit";
import { AgDataGridModule } from "./common/ag-grid/ag-data-grid.module";
import { RefreshComponent } from './common/components/refresh/refresh.component';
import { ResumeUpComponent } from './pages/resume-up/resume-up.component';
import { ResumeUpItemComponent } from './pages/resume-up/resume-up-item/resume-up-item.component';
import { ReactiveFormsModule } from "@angular/forms";

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
    SelfComponent,
    RefreshComponent,
    ResumeUpComponent,
    ResumeUpItemComponent,
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    TuiRootModule,
    TuiDialogModule,
    TuiAlertModule,
    TuiAvatarModule,
    AgDataGridModule,
    TuiSvgModule,
    TuiCheckboxBlockModule,
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
