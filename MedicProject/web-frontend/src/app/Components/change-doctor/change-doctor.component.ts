import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../Services/account.service';


@Component({
  selector: 'app-change-doctor',
  templateUrl: './change-doctor.component.html',
  styleUrls: ['./change-doctor.component.css']
})
export class ChangeDoctorComponent implements OnInit {

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
  }

}
