import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  isModalChatOpen: boolean = false;

  constructor() { }

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
