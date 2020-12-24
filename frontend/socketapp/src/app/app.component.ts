import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
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

  constructor(public covidService: CovidService, public userchatService: UserchatService){

  }
  ngOnInit(): void {

    this.covidService.startConnection();
    this.covidService.startListener();

    this.userchatService.startConnection();
    this.userchatService.startUsersListener();
    
  }

}
