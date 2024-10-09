import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from 'oidc-client-ts';
import { BehaviorSubject, map, Observable, take } from 'rxjs';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { PORT } from 'auth-lib';
import { IMessage, MessageDto } from '../models/message';
import { PageResultDto } from 'shared-lib';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  private hubConnection!: HubConnection;
  private messageThreadSource = new BehaviorSubject<IMessage[]>([]);
  messageThread$ = this.messageThreadSource.asObservable();

  constructor(private http: HttpClient) {}

  createHubConnection(user: User, otherUsername: string) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(PORT.hubUrl + 'message?user=' + otherUsername, {
        accessTokenFactory: () => user.access_token,
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start().catch((err) => console.log(err));

    this.hubConnection.on('ReceiveMessageThread', (messagesThread) => {
      this.messageThreadSource.next(messagesThread);
      //this.stream.TotalPage = messagesThread.totalPage;
    });

    this.hubConnection.on('NewMessage', (message) => {
      this.messageThread$.pipe(take(1)).subscribe((messages) => {
        this.messageThreadSource.next([...messages, message]);
      });
    });
  }

  stopHubConnection() {
    if (this.hubConnection) {
      this.hubConnection.stop();
    }
  }

  clearMessages() {}

  async sendMessage(username: string, content: string) {
    return this.hubConnection
      .invoke('SendMessage', { author: username, content })
      .catch((error) => console.log(error));
  }

  getMessages(
    currentUsername: string,
    receiver: string,
    pageNumber: number,
    pageSize: number
  ): Observable<PageResultDto<MessageDto>> {
    let params = new HttpParams();
    params = params.append('CurrentUsername', currentUsername.toString());
    params = params.append('Receiver', receiver);
    params = params.append('PageNumber', pageNumber.toString());
    params = params.append('PageSize', pageSize.toString());

    return this.http.get<PageResultDto<MessageDto>>(
      `${PORT.chatAPI}/Chats/getMessages`,
      { params }
    );
  }
}
