
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import * as signalR from '@microsoft/signalr';
import { MessageModel } from '../models/message.model';

import { environment } from '../environments/environment';
import { UserModel } from '../models/user.model';


@Injectable({
  providedIn: 'root'
})
export class ChatService {
  public hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(`${environment.apiUrl}/chat`)
    .configureLogging(signalR.LogLevel.Information)
    .build();

  public messages$ = new BehaviorSubject<any>([]);
  public connectedUsers$ = new BehaviorSubject<UserModel[]>([]);
  public messages: any[] = [];
  public users: UserModel[] = [];

  constructor() {
    this.hubConnection.on("OnJoin", (message: MessageModel) => {
      this.messages = [...this.messages, message];
      this.messages$.next(this.messages);
    })

    this.hubConnection.on("LoadMessages", (loadedMessages: MessageModel[]) => {
      const newMessages = loadedMessages.map(m => ({
        text: m.text,
        date: m.date,
        sentiment: m.sentiment,
        userId: m.userId,
        user: m.user
      }));
      this.messages = [...this.messages, ...newMessages];
      this.messages$.next(this.messages);
    });

    this.hubConnection.on("ReceiveConnectedUsers", (users: any[]) => {
      console.log(users);
      this.connectedUsers$.next(users);
    });

    this.hubConnection.on("ReceiveMessage", (message: MessageModel) => {
      const receivedMessage = new MessageModel();
        receivedMessage.text = message.text,
        receivedMessage.sentiment = message.sentiment,
        receivedMessage.date = message.date,
        receivedMessage.userId = message.userId
        receivedMessage.user = message.user
      this.messages = [...this.messages, receivedMessage];
      this.messages$.next(this.messages);
    });
  }

  public async startConnection() {
    try {
      await this.hubConnection.start()
    } catch (error) {
      console.log(error);
    }
  }

  public async joinRoom(user: string) {
    await this.hubConnection.start()
    return this.hubConnection.invoke("OnJoin", user);
  }

  public async sendMessage(message: string) {
    this.hubConnection.invoke('SendMessage', message);
  }

  public async leaveChat() {
    return this.hubConnection.stop();
  }
}