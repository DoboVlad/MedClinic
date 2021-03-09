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
  hours: Appointment[];

  constructor(public appService: AppointmentService, public accountService: AccountService) { }

  ngOnInit(): void {
    this.minDate = new Date();
    var date = new Date();
    this.newDate = new Date(date.setMonth(date.getMonth()+1));
    this.appForm = new FormGroup({
      "date": new FormControl(null, Validators.required),
      "hour": new FormControl(null, Validators.required),
    });
  }

  onSelectedDate(){
    const date = this.appForm.get("date").value;
    this.appService.getMedicSchedule(date).subscribe(hours => {
      this.hours = hours;
    });
  }

  onSubmit(){
    this.app = {...this.appForm.value};
    this.app.date = this.appForm.get("date").value;
    var replaceIndex = this.appForm.get("hour").value.hour.indexOf("-");
    console.log(replaceIndex);

    this.app.hour = this.appForm.get("hour").value.hour.substring(0, replaceIndex);
    console.log(this.app);
    this.appService.createAppointment(this.app);
  }
}
