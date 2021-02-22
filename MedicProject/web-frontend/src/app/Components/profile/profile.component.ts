import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Models/UserModel';
import { Appointment } from 'src/app/Models/AppointmentModel';
import { AccountService } from 'src/app/Services/account.service';
import { AppointmentService } from 'src/app/Services/AppointmentService/appointment.service';
import { PatientService } from 'src/app/Services/PatientService/patient.service';
import { MedicService } from 'src/app/Services/MedicService/medic.service';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material/dialog';
import { UpdateAccountComponent } from '../update-account/update-account.component';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user: User;
  appointments: Appointment[];
  constructor(public accountService: AccountService,
     private appService: AppointmentService,
    private patientService: PatientService,
    private medicService: MedicService,
    private toastr: ToastrService,
    public dialog: MatDialog) {}

  ngOnInit(): void {
    this.accountService.isFetching = true;
    if(this.accountService.role == 0){
      this.patientService.myAccount().subscribe(user => {
        this.user = user;
        if(parseInt(this.user.cnp.substr(0,1)) == 1 || parseInt(this.user.cnp.substr(0,1)) == 5){
          this.user.gender = "Male";
        }else {
          this.user.gender = "Female";
        }
      if(user.isApproved == 1) {
        this.accountService.isApproved = true;
        this.accountService.isFetching = false;
      }
    });
  }
  if(this.accountService.role == 1){
    this.medicService.getUnapprovedUsers().subscribe(users => {
      if(users)
        this.toastr.info('You have new requests for new patients.', 'New users!');
    });

    this.getMedicInfo();
  }
  }

  getMedicInfo(){
    this.medicService.medicAccount().subscribe(user => {
      this.user = user;
      if(user.isApproved == 1) {
        this.accountService.isApproved = true;
        this.accountService.isFetching = false;
      }
    });
  }

  openDialog(){
    const dialogRef = this.dialog.open(UpdateAccountComponent);
    dialogRef.afterClosed().subscribe(result => {
      this.getMedicInfo();
    });
  }
}
