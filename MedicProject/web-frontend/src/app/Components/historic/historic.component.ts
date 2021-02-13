import { Component, OnInit } from '@angular/core';
import { Appointment } from 'src/app/Models/AppointmentModel';
import { AppointmentService } from 'src/app/Services/AppointmentService/appointment.service';
import { AccountService } from '../../Services/account.service';

@Component({
  selector: 'app-historic',
  templateUrl: './historic.component.html',
  styleUrls: ['./historic.component.css']
})
export class HistoricComponent implements OnInit {
  appointments: Appointment[];
  constructor(public accountService: AccountService, private appService: AppointmentService) { }

  ngOnInit(): void {
    this.accountService.isFetching = true;
    this.appService.getPastAppointments().subscribe(app => {
        this.appointments = app;
        this.accountService.isFetching = false;
    });
  }

}
