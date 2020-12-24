import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GoogleChartsModule } from 'angular-google-charts';
import { CovidService } from './services/covid.service';
import { UserchatService } from './services/userchat.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    GoogleChartsModule,
    NgbModule,
    FormsModule
  ],
  providers: [
    CovidService,
    UserchatService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
