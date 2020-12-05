import { MessageSignal } from './../Models/MessageSignal';
import { AppConsts } from './../AppConsts';
import { EventEmitter, Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';


@Injectable({
  providedIn: 'root'
})
export class ChatServiceService {

  private hubConnection:signalR.HubConnection;
  signalReceived = new EventEmitter<MessageSignal>();
  constructor() { 
    this.buildConnection();
    this.startConnection();
  }

  private buildConnection = () => {
  //      abp.signalr = {
  //     autoConnect: true,
  //     connect: undefined,
  //     hubs: undefined,
  //     qs: AppConsts.authorization.encryptedAuthTokenName + '=' + encodeURIComponent(encryptedAuthToken),
  //     remoteServiceBaseUrl: AppConsts.remoteServiceBaseUrl,
  //     startConnection: undefined,
  //     url: '/signalr'
  // };


    this.hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(AppConsts.remoteServiceBaseUrl + "/signalrchat")
    .build();
  }



  private startConnection = () => {
    this.hubConnection
    .start()
    .then(() =>{
      console.log("connection chat started...");;
      this.registerSignalEvents();
    })
    .catch(err =>{
      console.log("Error while starting conneciton: "+err);
      
      setTimeout(function(){
        this.startConnection();
      },3000);
    });
  }

  private registerSignalEvents(){
    this.hubConnection.on("checkMessage",(data:MessageSignal) => {
      this.signalReceived.emit(data);
    });
  }
}
