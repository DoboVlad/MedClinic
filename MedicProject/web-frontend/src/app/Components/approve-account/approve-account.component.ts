import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AccountService } from 'src/app/Services/account.service';
import { MedicService } from 'src/app/Services/MedicService/medic.service';
import { PatientService } from 'src/app/Services/PatientService/patient.service';

@Component({
  selector: 'app-approve-account',
  templateUrl: './approve-account.component.html',
  styleUrls: ['./approve-account.component.css']
})
export class ApproveAccountComponent implements OnInit {
  approveUserForm: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private medicService: MedicService) { }

  ngOnInit(): void {
    this.approveUserForm = new FormGroup({});
  }

  approve(){
    this.medicService.approveUser(this.data.user.id).subscribe(user => {
      console.log(user);
    });;
  }

  cancel(){}

}
