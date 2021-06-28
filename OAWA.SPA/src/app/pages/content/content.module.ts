import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContentComponent } from './content.component';
import { ContentRoutes } from './content.routing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CreateComponent } from './create/create.component';
import { ViewContentComponent } from './view-content/view-content.component';
import { ViewContentModule } from './view-content/view-content.module';

@NgModule({
  imports: [
    CommonModule,
    ContentRoutes,
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [ContentComponent, CreateComponent, ViewContentComponent]
})
export class ContentModule { }
