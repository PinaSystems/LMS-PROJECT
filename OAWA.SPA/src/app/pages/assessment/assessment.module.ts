import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AssessmentComponent } from './assessment.component';
import { UploadComponent } from './upload/upload.component';
import { ViewComponent } from './view/view.component';
import { AssessmentRoutes } from './assessment.routing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ScoringComponent } from './scoring/scoring.component';

@NgModule({
  imports: [
    CommonModule,
    AssessmentRoutes,
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [AssessmentComponent, UploadComponent, ViewComponent, ScoringComponent]
})
export class AssessmentModule { }
