import { Component, OnInit } from '@angular/core';
import { Appointment } from 'src/app/Models/AppointmentModel';
import { AppointmentService } from 'src/app/Services/appointment.service';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-doctor-appointments',
  templateUrl: './doctor-appointments.component.html',
  styleUrls: ['./doctor-appointments.component.css']
})
export class DoctorAppointmentsComponent implements OnInit {
  appointments: Appointment[];
  constructor(public userService: UserService, private appService: AppointmentService) { }

  ngOnInit(): void {
    this.userService.isFetching = true;
    this.appService.getAllMedicAppointments().subscribe(appointments => {
      this.appointments = appointments;
      console.log(appointments);
      this.userService.isFetching = false;
    });
  }
}
