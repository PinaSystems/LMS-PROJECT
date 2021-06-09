import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatComponent } from './chat.component';
import { ChatRoutes } from './chat.routing';

@NgModule({
  imports: [
    CommonModule,
    ChatRoutes
  ],
  declarations: [ChatComponent]
})
export class ChatModule { }
