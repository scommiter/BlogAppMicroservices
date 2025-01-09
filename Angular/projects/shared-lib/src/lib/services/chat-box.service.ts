import { Injectable } from '@angular/core';
import { BehaviorSubject, ReplaySubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ChatBoxService {
  isModalChatOpen: boolean = false;

  private closeChatSubject = new Subject<void>();
  closeChat$ = this.closeChatSubject.asObservable();

  closeChat() {
    this.closeChatSubject.next();
  }

  constructor() {}

  openModal() {
    this.isModalChatOpen = true;
  }

  closeModal() {
    this.isModalChatOpen = false;
  }

  toggleChat() {
    this.isModalChatOpen = !this.isModalChatOpen;
  }
}
