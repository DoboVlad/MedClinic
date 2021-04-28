import { Message } from '../../Models/Message';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { MessageService } from '../../Services/MessageService/message.service';
import { AccountService } from 'src/app/Services/account.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { User } from 'src/app/Models/UserModel';
import { PatientService } from 'src/app/Services/PatientService/patient.service';
import { map } from 'rxjs/operators';
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
  active_tab: boolean = false;
  message: Message = {};
  activeUser: User;

  ngOnInit(): void {
    this.messageService.startConnection();
    this.chatForm = new FormGroup({
      "Content": new FormControl(null, Validators.required)
    });

    if(this.accountService.role == 1){
      this.patientService.getApprovedPatients().pipe(map(x => {
        return x.filter(element =>
          element.email != this.accountService.email && element.isApproved == 1
        )
      })).subscribe(users => {
        this.patients = users;
      });
    }
  }

  getMessage(patient){
    console.log(this.activeUser);
    patient.active = false;
    this.activeUser = patient;
    patient.active = !patient.active;
    console.log(patient.active);
    this.messageService.stopConnection();
    this.messageService.startConnection(patient.email);
    this.email = patient.email;
  }

  openForm() {
    document.getElementById("myForm").style.display = "block";
  }

  closeForm() {
    document.getElementById("myForm").style.display = "none";
  }

  onSubmit(){
    if(this.chatForm.get("Content").value == null){
      console.log(this.chatForm.get('Content'));
    }else{
      this.message.content = this.chatForm.get('Content').value;
      this.message.receiverEmail = this.email;
      this.messageService.sendMessage(this.message).then(() => {
        this.chatForm.reset();
      });
    }
  }

  ngOnDestroy(){
    this.messageService.stopConnection();
  }
}
