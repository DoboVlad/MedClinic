import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
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
  constructor(private appointmentService: AppointmentService) { }
  hours: Appointment[];
  ngOnInit(): void {
    this.addAppointmentForm = new FormGroup({
      'date': new FormControl(null, Validators.required),
      "hour": new FormControl(null, Validators.required)
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
    this.app.date = this.addAppointmentForm.get("date").value;
    var replaceIndex = this.addAppointmentForm.get("hour").value.hour.indexOf("-");
    console.log(replaceIndex);

    this.app.hour = this.addAppointmentForm.get("hour").value.hour.substring(0, replaceIndex);
    console.log(this.app);
    // this.appointmentService.createAppointment(this.app);
  }

  back(){

  }

}
