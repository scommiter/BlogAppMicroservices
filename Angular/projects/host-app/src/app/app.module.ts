import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavComponent } from './nav/nav.component';
import { AuthLibModule } from 'auth-lib';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AuthLibModule
  ],
  providers: [

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
