import { Routes, RouterModule } from '@angular/router';
import { ScoringComponent } from './scoring/scoring.component';
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
    path: 'scoring/:id',
    component: ScoringComponent 
  }
];

export const AssessmentRoutes = RouterModule.forChild(routes);
