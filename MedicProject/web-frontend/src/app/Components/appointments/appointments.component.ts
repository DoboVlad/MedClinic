import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Appointment } from 'src/app/Models/AppointmentModel';
import { AppointmentService } from 'src/app/Services/AppointmentService/appointment.service';
import { AccountService } from 'src/app/Services/account.service';
import { DeleteAppointmentComponent } from '../delete-appointment/delete-appointment.component';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { CalendarEvent } from 'angular-calendar';

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
  appointments: Appointment[];

  constructor(public appService: AppointmentService, public accountService: AccountService, private dialog: MatDialog,
    public appointmentService: AppointmentService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.minDate = new Date();
    var date = new Date();
    this.newDate = new Date(date.setMonth(date.getMonth()+1));
    this.appForm = new FormGroup({
      "date": new FormControl(null, Validators.required),
      "hour": new FormControl(null, Validators.required),
    });

   this.getAppointments();
  }

  getAppointments(){
    this.appService.getAppointmentsForPacient().subscribe(appointments => {
      var date = new Date();
      appointments.forEach(app => {
        let [hours, minutes] = app.hour.split(':');
        app.date = new Date(app.date);
        app.date.setHours(+hours);
        app.date.setMinutes(+minutes);
        app.difference = {days: 0, hours: 0};
        app.difference.days = Math.floor(+((app.date.getTime() - date.getTime()) / (1000 * 3600 * 24)).toFixed(1));
        app.difference.hours = Math.floor(+(((new Date(app.date).getTime() - date.getTime()) / (1000 * 3600)) % 24).toFixed(1));
      })
      this.appointments = appointments;
    });
  }

  handleEvent(event: number, date: Date): void {
    var currentDate = new Date();
    if(date >= currentDate){
      const dialogRef = this.dialog.open(DeleteAppointmentComponent, {data: {id: event}});
      dialogRef.afterClosed().subscribe(result => {
        if(this.appointmentService.info != null){
          this.toastr.info(this.appointmentService.info);
          this.getAppointments();
        }
        this.appointmentService.info = null;
      });
    }
    else {
      this.toastr.info("You can't edit this appointment anymore.");
    }
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

    this.app.hour = this.appForm.get("hour").value;
    console.log(this.app);
    this.appService.createAppointment(this.app).subscribe(response => {
      console.log(response);
      this.getAppointments();
    });
  }
}
