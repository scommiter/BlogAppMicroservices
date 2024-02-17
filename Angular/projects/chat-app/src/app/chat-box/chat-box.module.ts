import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ChatComponent } from './chat/chat.component';
import { ChatBoxComponent } from './chat-box.component';
import { RouterModule } from '@angular/router';

const EXPORTS = [ChatComponent];

@NgModule({
  declarations: [ChatBoxComponent, ...EXPORTS],
  imports: [
    CommonModule
  ],
  exports: [...EXPORTS]
})
export class ChatBoxModule { 
  static exports = EXPORTS; 
}
