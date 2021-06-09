import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewsletterComponent } from './newsletter.component';
import { NewsletterRoutes } from './newsletter.routing';
import { ViewComponent } from './view/view.component';
import { UploadComponent } from './upload/upload.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    NewsletterRoutes,
    FormsModule,
    ReactiveFormsModule
    ],
  declarations: [NewsletterComponent, UploadComponent, ViewComponent]
})
export class NewsletterModule { }
