import { Component, OnInit } from '@angular/core';
import { Appointment } from 'src/app/Models/AppointmentModel';
import { AppointmentService } from 'src/app/Services/AppointmentService/appointment.service';
import { AccountService } from 'src/app/Services/account.service';
import { CalendarOptions } from '@fullcalendar/angular';

@Component({
  selector: 'app-doctor-appointments',
  templateUrl: './doctor-appointments.component.html',
  styleUrls: ['./doctor-appointments.component.css']
})
export class DoctorAppointmentsComponent implements OnInit {
  appointments: Appointment[];
  calendarOptions: CalendarOptions;
  constructor(public accountService: AccountService, private appService: AppointmentService) { }

  ngOnInit(): void {
    this.accountService.isFetching = true;
    this.appService.getAllMedicAppointments().subscribe(appointments => {
      this.appointments = appointments;
      console.log(appointments);
      this.accountService.isFetching = false;
    });

      this.calendarOptions = {
        initialView: 'dayGridMonth',
        timeZoneParam: 'UTC',
        weekends:false,
        eventSources:[
          {
            url: 'https://localhost:5001/api/appointments/nextAppointments',
            method: 'GET',
            color: '#B9F1DA',
            textColor: 'black',
          },
        ],
      }
  }


}
