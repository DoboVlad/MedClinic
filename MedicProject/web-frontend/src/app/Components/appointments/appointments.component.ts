import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Appointment } from 'src/app/Models/AppointmentModel';
import { AppointmentService } from 'src/app/Services/AppointmentService/appointment.service';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-appointments',
  templateUrl: './appointments.component.html',
  styleUrls: ['./appointments.component.css']
})
export class AppointmentsComponent implements OnInit {
  appForm: FormGroup;
  app: Appointment;
  minDate: Date;
  newDate: Date;
  pastAppointment: Appointment[];


  constructor(public appService: AppointmentService, public accountService: AccountService) { }

  ngOnInit(): void {
    this.minDate = new Date();
    var date = new Date();
    this.newDate = new Date(date.setMonth(date.getMonth()+1));
    this.appForm = new FormGroup({
      "hour": new FormControl(null, Validators.required),
      "date": new FormControl(null, Validators.required)
    });

  }



  onSubmit(){
    this.app = {...this.appForm.value};
    console.log(this.app);
    this.appService.createAppointment(this.app);
  }
}
