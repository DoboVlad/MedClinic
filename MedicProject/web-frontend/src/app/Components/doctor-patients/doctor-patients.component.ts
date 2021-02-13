import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Models/UserModel';
import { AccountService } from 'src/app/Services/account.service';
import { PatientService } from 'src/app/Services/PatientService/patient.service';

@Component({
  selector: 'app-doctor-patients',
  templateUrl: './doctor-patients.component.html',
  styleUrls: ['./doctor-patients.component.css']
})
export class DoctorPatientsComponent implements OnInit {
  patients: User[];
  constructor(public patientService: PatientService, public accountService: AccountService) { }
  dataSource: any;
  ngOnInit(): void {
    this.accountService.isFetching = true;
    this.patientService.getAllPatients().subscribe(patients => {
      this.patients = patients
      this.accountService.isFetching = false;
    })
  }

}
