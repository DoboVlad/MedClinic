import { Component, OnInit } from '@angular/core';
import { Appointment } from 'src/app/Models/AppointmentModel';
import { AppointmentService } from 'src/app/Services/appointment.service';
import { UserService } from '../../Services/user.service';

@Component({
  selector: 'app-historic',
  templateUrl: './historic.component.html',
  styleUrls: ['./historic.component.css']
})
export class HistoricComponent implements OnInit {
  appointments: Appointment[];
  constructor(public userService: UserService, private appService: AppointmentService) { }

  ngOnInit(): void {
    this.userService.isFetching = true;
    this.appService.getPastAppointments().subscribe(app => {
        this.appointments = app;
        this.userService.isFetching = false;
    });
  }

}
