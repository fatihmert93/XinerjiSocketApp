import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class UserchatService {

  userList = new Array();
  private hubConnection!: signalR.HubConnection;


  constructor() { }

  private startInvoke(){
    this.hubConnection.invoke("GetCovidList").catch((err) => {
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
