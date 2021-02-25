import { Message } from '../../Models/Message';
import { Component, OnInit } from '@angular/core';
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
export class MessagesComponent implements OnInit {
  messages: Message[] = [];
  constructor(private messageService: MessageService,
     public accountService: AccountService,
     private patientService: PatientService) { }
  chatForm: FormGroup;
  patients: User[];
  email : string;
  message: Message = {};

  ngOnInit(): void {
    this.chatForm = new FormGroup({
      "Content": new FormControl(null, Validators.required)
    });

    if(this.accountService.role==0){
      this.messageService.getMessages().subscribe(response => {
        this.messages = response;
        console.log(response);
      });
    }

    if(this.accountService.role == 1){
      this.patientService.getApprovedPatients().subscribe(users => {
        this.patients = users;
      });
    }
  }

  getMessage(email: string){
    this.messageService.getMessages(email).subscribe(response => {
      this.messages = response;
      console.log(response);
    });
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
    this.messageService.sendMessage(this.message);
  }
}
