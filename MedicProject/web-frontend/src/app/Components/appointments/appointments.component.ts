import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Appointment } from 'src/app/Models/AppointmentModel';
import { AppointmentService } from 'src/app/Services/appointment.service';

@Component({
  selector: 'app-appointments',
  templateUrl: './appointments.component.html',
  styleUrls: ['./appointments.component.css']
})
export class AppointmentsComponent implements OnInit {
  appForm: FormGroup;
  app: Appointment

  constructor(public appService: AppointmentService) { }

  ngOnInit(): void {
    this.appForm = new FormGroup({
      "hour": new FormControl(null, Validators.required),
      "date": new FormControl(null, Validators.required)
    });
  }

  onSubmit(){
    this.app = {...this.appForm.value};
    this.appService.createAppointment(this.app);

  }
}
