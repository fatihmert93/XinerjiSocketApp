import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { Form, NgForm } from '@angular/forms';
import { CovidService } from './services/covid.service';
import { UserchatService } from './services/userchat.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'socketapp';

  columnNames = ["Tarih","İstanbul", 'Ankara','İzmir', 'Konya', 'Antalya'];

  options: any = {legend: {position: 'Bottom'}};

  username = '';

  messageContent = '';

  connected = false;

  constructor(public covidService: CovidService, public userchatService: UserchatService){

  }
  ngOnInit(): void {

    this.covidService.startConnection();
    this.covidService.startListener();

    this.userchatService.startConnection();
    this.userchatService.startUsersListener();

    this.userchatService.startMessagesListener();
    
  }


  connect(form: NgForm){

    console.log(form);
    
    if(form.value.username.length == 0){
      alert('username cannot be empty!');
      return;
    }

    this.userchatService.connect(form.value.username);

    form.resetForm();

    this.connected = true;
  }

  sendMessage(form: NgForm){

    if(!this.connected){
      alert('you must be connect first');
      return;
    }
    
    this.userchatService.sendGroupMessage(form.value.messageContent);

    form.resetForm();

  }

}
