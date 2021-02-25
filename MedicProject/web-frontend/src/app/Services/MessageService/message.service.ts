import { HttpClient } from '@angular/common/http';
import { Message } from '../../Models/Message';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AccountService } from '../account.service';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = environment.apiUrl;


  constructor(private http: HttpClient, private accountService: AccountService) { }

  getMessages(email?: string){
    var emailToSend;
    if(email){
      emailToSend = "/" + email;
    }
    else emailToSend = "";
    return this.http.get<Message[]>(this.baseUrl + '/messages/thread' + emailToSend, {
      headers:{
        "Authorization": "Bearer " + this.accountService.token
      }
    });
  }

  sendMessage(message: Message){
    this.http.post(this.baseUrl + '/messages/send', message, {
      headers:{
        "Authorization": "Bearer " + this.accountService.token
      }
    }).subscribe(response => {
      console.log(response);
    });
  }
}
