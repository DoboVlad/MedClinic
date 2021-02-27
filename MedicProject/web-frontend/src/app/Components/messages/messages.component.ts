import { Message } from '../../Models/Message';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { MessageService } from '../../Services/MessageService/message.service';
import { AccountService } from 'src/app/Services/account.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { User } from 'src/app/Models/UserModel';
import { PatientService } from 'src/app/Services/PatientService/patient.service';
@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit, OnDestroy {
  messages: Message[] = [];
  constructor(public messageService: MessageService,
     public accountService: AccountService,
     private patientService: PatientService) { }
  chatForm: FormGroup;
  patients: User[];
  email : string;
  message: Message = {};

  ngOnInit(): void {
    this.messageService.startConnection();
    this.chatForm = new FormGroup({
      "Content": new FormControl(null, Validators.required)
    });

    if(this.accountService.role == 1){
      this.patientService.getApprovedPatients().subscribe(users => {
        this.patients = users;
      });
    }
  }

  getMessage(email: string){
    this.messageService.stopConnection();
    this.messageService.startConnection(email);
    this.email = email;
  }

  openForm() {
    document.getElementById("myForm").style.display = "block";
  }

  closeForm() {
    document.getElementById("myForm").style.display = "none";
  }

  onSubmit(){
    this.message.content = this.chatForm.get('Content').value;
    this.message.receiverEmail = this.email;
    console.log(this.message);
    this.messageService.sendMessage(this.message).then(() => {
      this.chatForm.reset();
    });
  }

  ngOnDestroy(){
    this.messageService.stopConnection();
  }
}
