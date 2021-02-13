import { Component, OnInit } from '@angular/core';
import { Appointment } from 'src/app/Models/AppointmentModel';
import { AppointmentService } from 'src/app/Services/AppointmentService/appointment.service';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-doctor-appointments',
  templateUrl: './doctor-appointments.component.html',
  styleUrls: ['./doctor-appointments.component.css']
})
export class DoctorAppointmentsComponent implements OnInit {
  appointments: Appointment[];
  constructor(public accountService: AccountService, private appService: AppointmentService) { }

  ngOnInit(): void {
    this.accountService.isFetching = true;
    this.appService.getAllMedicAppointments().subscribe(appointments => {
      this.appointments = appointments;
      console.log(appointments);
      this.accountService.isFetching = false;
    });
  }
}
