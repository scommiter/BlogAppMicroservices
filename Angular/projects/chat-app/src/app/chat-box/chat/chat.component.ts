import { AfterViewChecked, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MESSAGE_TYPE } from '../../enum/message-type';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.scss',
  exportAs: 'ChatComponent'
})
export class ChatComponent implements OnInit, AfterViewChecked {
  messageType = MESSAGE_TYPE;

  messages: Message[] = [
    {
      content: 'Hello, my name is Lupin',
      type: MESSAGE_TYPE.RECEIVER
    },
    {
      content: 'Hello, Lupin',
      type: MESSAGE_TYPE.SENDER
    }
  ];

  ngOnInit(): void {
  }

  sendMessage(input: HTMLInputElement): void {
    this.messages.push({ content: input.value, type: MESSAGE_TYPE.SENDER });
    input.value = '';
  }

  onKeyPress(event: KeyboardEvent, input: HTMLInputElement): void {
    if (event.key === 'Enter') {
      this.sendMessage(input);
    }
  }
  

  @ViewChild('chatBody') private chatBody!: ElementRef;

  // Hàm này sẽ được gọi sau khi mỗi tin nhắn được thêm vào
  scrollToBottom(): void {
    try {
      this.chatBody.nativeElement.scrollTop = this.chatBody.nativeElement.scrollHeight;
    } catch(err) { }
  }

  ngAfterViewChecked(): void {
    this.scrollToBottom();
  }

  closeModal() {
    const modal = document.getElementById('myModal');
    if (modal) {
      modal.style.display = 'none';
    }
  }
}


export interface Message{
  content: string;
  type: MESSAGE_TYPE;
}

