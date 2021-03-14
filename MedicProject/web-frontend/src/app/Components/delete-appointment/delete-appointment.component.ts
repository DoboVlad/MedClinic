import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Appointment } from 'src/app/Models/AppointmentModel';
import { AppointmentService } from 'src/app/Services/AppointmentService/appointment.service';

@Component({
  selector: 'app-delete-appointment',
  templateUrl: './delete-appointment.component.html',
  styleUrls: ['./delete-appointment.component.css']
})
export class DeleteAppointmentComponent implements OnInit {
  editAppointmentForm: FormGroup;
  hours: Appointment[];
  appointment: Appointment;

  constructor(private appointmentService: AppointmentService,  @Inject(MAT_DIALOG_DATA) public data: any, private router: Router) { }

  ngOnInit(): void {
    this.appointmentService.getAnAppoinmentById(this.data.id).subscribe(appointment => {
      this.appointment = appointment;
      this.editAppointmentForm = new FormGroup({
        'date': new FormControl(this.appointment[0].date, Validators.required),
        "hour": new FormControl(null, Validators.required)
      });
      this.getSchedule(this.appointment[0].date);
    })
  }

  getSchedule(data?: Date){
    var date: Date;
    if(data){
      date = data;
    }
    else {
      date = this.editAppointmentForm.value.date;
      const newDate = new Date(date);
      console.log(newDate);
    }
    this.appointmentService.getMedicSchedule(date).subscribe(hours => {
      this.hours = hours;
    });
  }

  onSubmit(){
    const date = {...this.editAppointmentForm.value};
    date.Id = this.data.id;
    this.appointmentService.updateAppointment(date).subscribe(result => {
      console.log("Programare schimbata cu succes");
    });
  }

  onDelete(){
    this.appointmentService.deleteAppointmentById(this.data.id).subscribe(response => {})
  }

  back(){
    this.router.navigate(["/doctorappointments"]);
  }
}
