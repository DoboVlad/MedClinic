import { Component, OnInit } from '@angular/core';
import { UserService } from '../../Services/user.service';


@Component({
  selector: 'app-change-doctor',
  templateUrl: './change-doctor.component.html',
  styleUrls: ['./change-doctor.component.css']
})
export class ChangeDoctorComponent implements OnInit {

  constructor(public userService: UserService) { }

  ngOnInit(): void {
  }

}
