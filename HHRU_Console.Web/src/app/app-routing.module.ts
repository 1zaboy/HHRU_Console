import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './_guards/auth.guard';
import { LoginComponent } from './pages/identity/login/login.component';
import { OverviewComponent } from './pages/overview/overview.component';
import { AuthRedirectComponent } from './pages/identity/auth-redirect/auth-redirect.component';
import { RefreshComponent } from './common/components/refresh/refresh.component';
import { ResumeUpComponent } from './pages/resume-up/resume-up.component';

const routes: Routes = [
  {
    path: 'refresh',
    component: RefreshComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'authredirect',
    component: AuthRedirectComponent,
  },
  {
    path: 'overview',
    canActivate: [AuthGuard],
    component: OverviewComponent,
  },
  {
    path: 'resumeup',
    canActivate: [AuthGuard],
    component: ResumeUpComponent,
  },
  { path: '', pathMatch: 'full', redirectTo: 'overview' },
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
