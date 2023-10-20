import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './_guards/auth.guard';
import { LoginComponent } from './pages/identity/login/login.component';
import { OverviewComponent } from './pages/overview/overview.component';
import { AuthRedirectComponent } from './pages/identity/auth-redirect/auth-redirect.component';

const routes: Routes = [
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
  { path: '', pathMatch: 'full', redirectTo: 'overview' },
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
