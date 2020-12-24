import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class UserchatService {

  userList = new Array();

  messages = new Array();

  private hubConnection!: signalR.HubConnection;


  constructor() { }

  private startInvoke(){
    this.hubConnection.invoke("ReceiveGroupMessage").then((res: any[]) => {

    }).catch((err) => {
      console.log(err);
    });
  }

  public startUsersListener(){
    this.hubConnection.on('GetAllUsers', (users: any[]) => {

      this.userList = [];

      users.forEach((item) => {
        this.userList.push(item);
      });

      console.log(this.userList);

    } )
  }

  public startMessagesListener(){
    this.hubConnection.on('ReceiveGroupMessage', (msgs: any[]) => {

      this.messages = [];

      debugger;

      msgs.forEach((item) => {
        this.messages.push(item);
      });

      console.log(this.messages);

    } )
  }

  public connect(username:string){
    this.hubConnection.invoke("Connect",username).then((res) => {
        console.log(res);
    }).catch((err) => {
      console.log(err);
    });
  }

  public sendGroupMessage(message:string){
    this.hubConnection.invoke("SendGroupMessage",message).then((res) => {
        console.log(res);
    }).catch((err) => {
      console.log(err);
    });
  }

  startConnection() {

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:44307/UserChatHub')
      .build();

      this.hubConnection.start().then(() => {

        this.startInvoke();

      })
      .catch((err) => {
        console.log(err);
      });
  }
}
