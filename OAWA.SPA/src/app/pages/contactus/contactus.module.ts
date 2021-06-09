import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContactusComponent } from './contactus.component';
import { ContactusRoutes } from './contactus.routing';

@NgModule({
  imports: [
    CommonModule,
    ContactusRoutes
  ],
  declarations: [ContactusComponent]
})
export class ContactusModule { }
