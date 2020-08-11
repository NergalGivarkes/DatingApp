import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import { AppComponent } from './app.component';

import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import {ErrorInterceptorProvider} from "./_services/error.interceptor";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {BsDropdownModule} from "ngx-bootstrap/dropdown";
import { MemebrListComponent } from './memebr-list/memebr-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import {RouterModule} from "@angular/router";
import {appRoutes} from "./routes";

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      MemebrListComponent,
      ListsComponent,
      MessagesComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      BrowserAnimationsModule,
     BsDropdownModule.forRoot(),
     RouterModule.forRoot(appRoutes)
   ],
   providers: [
      AuthService,
     ErrorInterceptorProvider
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }