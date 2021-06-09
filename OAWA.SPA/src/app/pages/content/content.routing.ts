import { Routes, RouterModule } from '@angular/router';
import { CreateComponent } from './create/create.component';

const routes: Routes = [
  { 
    path: '',
    component: CreateComponent 
  },
  { 
    path: 'create',
    component: CreateComponent 
  },
];

export const ContentRoutes = RouterModule.forChild(routes);
