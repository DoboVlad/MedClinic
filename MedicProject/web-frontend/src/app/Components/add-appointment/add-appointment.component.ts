import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-appointment',
  templateUrl: './add-appointment.component.html',
  styleUrls: ['./add-appointment.component.css']
})
export class AddAppointmentComponent implements OnInit {
  addAppointmentForm: FormGroup;
  constructor() { }

  ngOnInit(): void {
    this.addAppointmentForm = new FormGroup({
      'date': new FormControl(null, Validators.required),
      "hour": new FormControl(null, Validators.required)
    });

  }

  onSubmit(){

  }

  back(){

  }

}
