import { Routes, RouterModule } from '@angular/router';
import { NewsletterComponent } from './newsletter.component';
import { UploadComponent } from './upload/upload.component';
import { ViewComponent } from './view/view.component';

const routes: Routes = [
  { 
    path: '',
    component: UploadComponent 
  },
  { 
    path: 'upload',
    component: UploadComponent 
  },
  { 
    path: 'view',
    component: ViewComponent 
  },
  { 
    path: 'viewfile',
    component: ViewComponent 
  },
];

export const NewsletterRoutes = RouterModule.forChild(routes);
