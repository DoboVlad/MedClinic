import { HttpClient } from '@angular/common/http';
import { Message } from '../../Models/Message';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AccountService } from '../account.service';
import { HttpTransportType, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { User } from 'src/app/Models/UserModel';
import { take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = environment.apiUrl;
  private messageThreadSource = new BehaviorSubject<Message[]>([]);
  messageThread$ = this.messageThreadSource.asObservable();

  constructor(private http: HttpClient, private accountService: AccountService) { }

  hubConnection: HubConnection;

  startConnection = (otherEmail?: string) => {
    this.hubConnection= new HubConnectionBuilder()
      .withUrl('https://localhost:5001/hubs/MessageHub?email=' + otherEmail, {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,
        accessTokenFactory: () => this.accountService.token
      })
      .withAutomaticReconnect()
      .build();

      this.hubConnection
        .start()
        .then(() => {
          console.log('Hub connection started!');
        }).catch((error) => {
          console.log(error);
    });

    this.hubConnection.on("ReceiveMessage", messages => {
      console.log(messages);
      this.messageThreadSource.next(messages);
    });

    this.hubConnection.on("MessageReceived", message => {
      console.log(message);
      this.messageThread$.pipe(take(1)).subscribe(messages => {
        this.messageThreadSource.next([...messages, message])
      });
    });
  }

  stopConnection(){
    this.hubConnection.stop();
  }

  async sendMessage(message: Message){
    return this.hubConnection.invoke("NewMessage", message)
      .catch(err => console.error(err));
  }

  // getMessages(email?: string){
  //   var emailToSend;
  //   if(email){
  //     emailToSend = "/" + email;
  //   }
  //   else emailToSend = "";
  //   var obs =  this.http.get<Message[]>(this.baseUrl + '/messages/thread' + emailToSend, {
  //     headers:{
  //       "Authorization": "Bearer " + this.accountService.token
  //     }
  //   }).subscribe(messages => {
  //     this.messageThread$.subscribe(message => {
  //       this.messageThreadSource.next([...message, messages]);
  //     }
  //   });

  //   return this.messageThread$;
  // }

  // sendMessage(message: Message){
  //   this.http.post(this.baseUrl + '/messages/send', message, {
  //     headers:{
  //       "Authorization": "Bearer " + this.accountService.token
  //     }
  //   }).subscribe(response => {
  //     console.log(response);
  //   });
  // }
}
