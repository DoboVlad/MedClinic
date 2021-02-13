import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { CalendarOptions } from '@fullcalendar/angular';
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
  calendarOptions: CalendarOptions;

  constructor(public appService: AppointmentService, public accountService: AccountService) { }

  ngOnInit(): void {
    this.minDate = new Date();
    var date = new Date();
    this.newDate = new Date(date.setMonth(date.getMonth()+1));
    this.appForm = new FormGroup({
      "hour": new FormControl(null, Validators.required),
      "date": new FormControl(null, Validators.required)
    });

    // this.calendarOptions = {
    //   initialView: 'dayGridMonth',
    //   weekends:false,
    //   events:function(info, success, fail){
    //       req.get("https://localhost:5001/api/appointments/historyAppointments").type('json')
    //         .query({
    //           start: info.start.valueOf(),
    //           end: info.end.valueOf()
    //         })
    //         .end(function(err, res){
    //           if (err) {
    //             fail(err);
    //           } else {
    //             success(
    //               Array.prototype.slice.call(
    //                 res.getElementsByTagName('event')
    //               ).map(function(eventEl){
    //                 return {
    //                   title: eventEl.getAttribute('title'),
    //                   start: eventEl.getAttribute('start')
    //                 }
    //               })
    //             )
    //         }
    //     })
    //   }
    // }
  }



  onSubmit(){
    this.app = {...this.appForm.value};
    console.log(this.app);
    this.appService.createAppointment(this.app);
  }
}
