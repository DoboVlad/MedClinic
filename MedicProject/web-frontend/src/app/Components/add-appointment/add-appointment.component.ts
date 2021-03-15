import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Appointment } from 'src/app/Models/AppointmentModel';
import { AppointmentService } from 'src/app/Services/AppointmentService/appointment.service';

@Component({
  selector: 'app-add-appointment',
  templateUrl: './add-appointment.component.html',
  styleUrls: ['./add-appointment.component.css']
})
export class AddAppointmentComponent implements OnInit {
  addAppointmentForm: FormGroup;
  app: Appointment;
  constructor(private appointmentService: AppointmentService, private route: Router) { }
  hours: Appointment[];
  ngOnInit(): void {
    this.addAppointmentForm = new FormGroup({
      'date': new FormControl(null, Validators.required),
      "hour": new FormControl(null, Validators.required),
      "name": new FormControl()
    });
  }

  getSchedule(){
    const date = this.addAppointmentForm.get("date").value.toISOString();
    this.appointmentService.getMedicSchedule(date).subscribe(hours => {
      this.hours = hours;
    });
  }

  onSubmit(){
    this.app = {...this.addAppointmentForm.value};
    this.app.firstName = this.addAppointmentForm.get("name").value;
    this.appointmentService.createAppointment(this.app).subscribe(result => {
      this.appointmentService.info = "Appointment was succesfully created!";
      this.route.navigate(["/doctorappointments"]);
    });
  }

  back(){

  }

}
