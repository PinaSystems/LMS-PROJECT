import { Routes, RouterModule } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { ViewContentComponent } from './view-content/view-content.component';

const routes: Routes = [
  { 
    path: '',
    component: CreateComponent 
  },
  { 
    path: 'create',
    component: CreateComponent 
  },
  { 
    path: 'view',
    component: ViewContentComponent 
  }
];

export const ContentRoutes = RouterModule.forChild(routes);
