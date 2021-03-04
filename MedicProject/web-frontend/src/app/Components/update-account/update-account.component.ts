import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute, NavigationEnd, NavigationStart, Router } from '@angular/router';
import { filter } from 'rxjs/operators';
import { User } from 'src/app/Models/UserModel';
import { AccountService } from 'src/app/Services/account.service';
import { MedicService } from 'src/app/Services/MedicService/medic.service';

@Component({
  selector: 'app-update-account',
  templateUrl: './update-account.component.html',
  styleUrls: ['./update-account.component.css']
})
export class UpdateAccountComponent implements OnInit {
  authenticatedUser: User;
  updateAccountForm: FormGroup;
  previousUrl: string;
  currentUrl: string;

  constructor(private accountService: AccountService,
     private MedicService: MedicService,
      private router: Router,
     @Inject(MAT_DIALOG_DATA) public user?: any) { }

  ngOnInit(): void {
    if(this.router.url == '/profile'){
      this.MedicService.medicAccount().subscribe(medic => {
        this.authenticatedUser = medic;
        this.initForm();
      });
    } else {
      this.authenticatedUser = {...this.user.user};
      this.initForm();
    }

  }

  initForm(){
    this.updateAccountForm = new FormGroup({
      'firstName': new FormControl(this.authenticatedUser.firstName, [Validators.required]),
      'lastName': new FormControl(this.authenticatedUser.lastName, Validators.required),
      'email': new FormControl(this.authenticatedUser.email, [Validators.required, Validators.email]),
      'phoneNumber': new FormControl(this.authenticatedUser.phoneNumber, Validators.required),
      'description': new FormControl(this.authenticatedUser.description),
      //'password': new FormControl(null)
    });
  }


  onSubmit(){
    const user : User = {...this.updateAccountForm.value};
    if(this.router.url == '/profile'){
      this.accountService.updateMedic(user).subscribe(response => {
        this.router.navigateByUrl('/profile');
      })
    } else {
      this.accountService.updatePatient(user).subscribe(response => {
        console.log('user update');
        this.router.navigateByUrl('/doctorpatients');
      })
    }
  }

  back(){
    if(this.router.url == '/profile'){
      this.router.navigate(['/profile']);
    }
    else {
      this.router.navigate(['/doctorpatients']);
    }
  }
}
