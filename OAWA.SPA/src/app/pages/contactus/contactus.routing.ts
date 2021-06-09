import { Routes, RouterModule } from '@angular/router';
import { ContactusComponent } from './contactus.component';

const routes: Routes = [
  { 
    path: '',
    component: ContactusComponent 
  }
];

export const ContactusRoutes = RouterModule.forChild(routes);
