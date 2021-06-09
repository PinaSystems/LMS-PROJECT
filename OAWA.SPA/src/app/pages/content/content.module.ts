import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContentComponent } from './content.component';
import { ContentRoutes } from './content.routing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CreateComponent } from './create/create.component';

@NgModule({
  imports: [
    CommonModule,
    ContentRoutes,
    FormsModule,
    ReactiveFormsModule,
  ],
  declarations: [ContentComponent, CreateComponent]
})
export class ContentModule { }
