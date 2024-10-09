import { Component, OnInit } from '@angular/core';
import { MessageService } from './../services/message.service';

@Component({
  selector: 'app-chat-box',
  templateUrl: './chat-box.component.html',
  styleUrl: './chat-box.component.scss',
})
export class ChatBoxComponent implements OnInit {
  constructor(private messageService: MessageService) {}

  ngOnInit(): void {}
  isOpen = false;

  openModal(): void {
    this.isOpen = true;
  }

  closeModal(): void {
    this.isOpen = false;
  }
}
