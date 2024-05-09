import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from 'oidc-client-ts';
import { BehaviorSubject, map, take } from 'rxjs';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { PORT } from "auth-lib";
import { IMessage } from '../models/message';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private hubConnection!: HubConnection;
  private messageThreadSource = new BehaviorSubject<IMessage[]>([]);
  messageThread$ = this.messageThreadSource.asObservable();

  constructor(private http: HttpClient) { }

  createHubConnection(user: User, otherUsername: string){
    
    this.hubConnection = new HubConnectionBuilder()
        .withUrl(PORT.hubUrl+ 'message?user=' + otherUsername, {
        accessTokenFactory: ()=> user.access_token
        }).withAutomaticReconnect().build()

    this.hubConnection.start().catch(err => console.log(err));

    this.hubConnection.on('ReceiveMessageThread', messagesThread => {
        this.messageThreadSource.next(messagesThread);
        //this.stream.TotalPage = messagesThread.totalPage;
    })

    this.hubConnection.on('NewMessage', message => {
        this.messageThread$.pipe(take(1)).subscribe(messages => {
            this.messageThreadSource.next([...messages, message])
        })
    })    
  }

  stopHubConnection(){
    if(this.hubConnection){
        this.hubConnection.stop();
    }
  }

  clearMessages(){
    
  }

  async sendMessage(username: string, content: string){  
    console.log(username, content);  
    return this.hubConnection.invoke('SendMessage', {recipientUsername: username, content})
    .catch(error => console.log(error));
  }

  getMessageThread(pageNumber: number, pageSize: number, recipientUsername: string){
    
  }
}
