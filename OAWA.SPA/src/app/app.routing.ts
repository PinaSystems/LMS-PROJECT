import { Routes, RouterModule } from '@angular/router';
import { PagesComponent } from './pages/pages.component';

const appRoutes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: '',
    redirectTo: 'password/forgot',
    pathMatch: 'full'
  },
  {
    path: '',
    redirectTo: 'password/reset',
    pathMatch: 'full'
  },
  {
    path: '',
    redirectTo: 'pages/index',
    pathMatch: 'full'
  },
  {
    path: '**',
    redirectTo: 'pages/index'
  }
];

export const routing = RouterModule.forRoot(appRoutes);
