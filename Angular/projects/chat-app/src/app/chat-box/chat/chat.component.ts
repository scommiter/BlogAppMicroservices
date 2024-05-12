import { AfterViewChecked, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MESSAGE_TYPE } from '../../enum/message-type';
import { MessageService } from '../../services/message.service';
import { AuthLibService } from 'auth-lib';
import { User } from 'oidc-client-ts';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.scss',
  exportAs: 'ChatComponent'
})
export class ChatComponent implements OnInit, OnDestroy, AfterViewChecked {
  messageType = MESSAGE_TYPE;
  userName: string = '';
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
  private _user!: User;

  constructor(
    public messageService: MessageService,
    public authService: AuthLibService
  ){
  }

  ngOnInit(): void {
    this._user = JSON.parse(localStorage.getItem('user')!);
    this.userName = this._user.profile.sub;
    this.messageService.createHubConnection(this._user, this.userName);
  }

  ngOnDestroy(): void {
    this.messageService.stopHubConnection();
  }

  sendMessage(input: HTMLInputElement): void {
    // this.messages.push({ content: input.value, type: MESSAGE_TYPE.SENDER });
    this.messageService.sendMessage(this.userName, input.value).then(() => {
      input.value = '';
    })
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

