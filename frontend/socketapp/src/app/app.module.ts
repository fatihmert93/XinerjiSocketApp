import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GoogleChartsModule } from 'angular-google-charts';
import { CovidService } from './services/covid.service';
import { UserchatService } from './services/userchat.service';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    GoogleChartsModule
  ],
  providers: [
    CovidService,
    UserchatService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
