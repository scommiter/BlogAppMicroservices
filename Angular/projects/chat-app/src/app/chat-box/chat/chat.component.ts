import {
  AfterViewChecked,
  Component,
  ElementRef,
  EventEmitter,
  OnDestroy,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { MESSAGE_TYPE } from '../../enum/message-type';
import { MessageService } from '../../services/message.service';
import { AuthLibService } from 'auth-lib';
import { User } from 'oidc-client-ts';
import { Subject, takeUntil } from 'rxjs';
import { PageResultDto } from 'shared-lib';
import { ChatBoxService } from 'shared-lib';
import { MessageDto } from '../../models/message';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.scss',
  exportAs: 'ChatComponent',
})
export class ChatComponent implements OnInit, OnDestroy, AfterViewChecked {
  messageType = MESSAGE_TYPE;
  @Output() closeChat = new EventEmitter<void>();
  userName: string = '';
  receiver: string = 'lupan';
  private ngUnsubscribe = new Subject<void>();
  messages: Message[] = [
    {
      content: 'Hello, my name is Lupin',
      type: MESSAGE_TYPE.RECEIVER,
    },
    {
      content: 'Hello, Lupin',
      type: MESSAGE_TYPE.SENDER,
    },
  ];
  messageDtos: MessageDto[] = [];
  private _user!: User;

  constructor(
    private messageService: MessageService,
    private authService: AuthLibService,
    private chatBoxService: ChatBoxService
  ) {}

  ngOnInit(): void {
    this._user = JSON.parse(localStorage.getItem('user')!);
    this.userName = this._user.profile.sub;

    this.getMessages();

    this.messageService.createHubConnection(this._user, this.receiver);
  }

  ngOnDestroy(): void {
    this.messageService.stopHubConnection();
  }

  getMessages() {
    this.messageService
      .getMessages(this.userName, this.receiver, 1, 10)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PageResultDto<MessageDto>) => {
          this.messages = this.mapListMessageDtoToMessage(response.items);
          console.log('Hello', this.messages);
        },
        error: () => {},
      });
  }

  onKeyPress(event: KeyboardEvent, input: HTMLInputElement): void {
    if (event.key === 'Enter') {
      this.sendMessage(input);
    }
    this.getMessages();
  }

  @ViewChild('chatBody') private chatBody!: ElementRef;

  // function call when ever new message added
  scrollToBottom(): void {
    try {
      this.chatBody.nativeElement.scrollTop =
        this.chatBody.nativeElement.scrollHeight;
    } catch (err) {}
  }

  ngAfterViewChecked(): void {
    this.scrollToBottom();
  }

  closeModal() {
    this.chatBoxService.closeChat();
  }

  private sendMessage(input: HTMLInputElement): void {
    this.messageService.sendMessage(this.receiver, input.value).then(() => {
      input.value = '';
    });
  }

  private mapListMessageDtoToMessage(dtoList: MessageDto[]): Message[] {
    return dtoList.map((dto) => ({
      content: dto.content,
      type:
        dto.sender === this.userName
          ? MESSAGE_TYPE.SENDER
          : MESSAGE_TYPE.RECEIVER,
    }));
  }
}

export interface Message {
  content: string;
  type: MESSAGE_TYPE;
}
