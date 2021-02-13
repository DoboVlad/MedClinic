import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-doctor-patients',
  templateUrl: './doctor-patients.component.html',
  styleUrls: ['./doctor-patients.component.css']
})
export class DoctorPatientsComponent implements OnInit {
  patients: User[];
  constructor(public userService: UserService) { }
  dataSource: any;
  ngOnInit(): void {
    this.userService.isFetching = true;
    this.userService.getAllPatients().subscribe(patients => {
      this.patients = patients
      this.userService.isFetching = false;
    })
  }

}
