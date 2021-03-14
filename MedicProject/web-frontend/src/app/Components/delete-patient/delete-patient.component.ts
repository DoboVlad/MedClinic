import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { PatientService } from 'src/app/Services/PatientService/patient.service';

@Component({
  selector: 'app-delete-patient',
  templateUrl: './delete-patient.component.html',
  styleUrls: ['./delete-patient.component.css']
})
export class DeletePatientComponent implements OnInit {
  deleteUserForm: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private patientService: PatientService) { }

  ngOnInit(): void {
    console.log(this.data);
    console.log(this.data.user.id);
    this.deleteUserForm = new FormGroup({});
  }

  onDelete(){
    console.log(this.data.user.id);
    this.patientService.deletePatient(this.data.user.id).subscribe(result => {
      console.log(result);
    })
  }

  cancel(){}

}
