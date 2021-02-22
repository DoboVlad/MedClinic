import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
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

  constructor(private accountService: AccountService, private MedicService: MedicService, private router: Router) { }

  ngOnInit(): void {
    this.MedicService.medicAccount().subscribe(medic => {
      this.authenticatedUser = medic;
      this.updateAccountForm = new FormGroup({
        'firstName': new FormControl(this.authenticatedUser.firstName, [Validators.required]),
        'lastName': new FormControl(this.authenticatedUser.lastName, Validators.required),
        'email': new FormControl(this.authenticatedUser.email, [Validators.required, Validators.email]),
        'phoneNumber': new FormControl(this.authenticatedUser.phoneNumber, Validators.required),
        'description': new FormControl(this.authenticatedUser.description, Validators.required),
        //'password': new FormControl(null)
      });
    });
  }


  onSubmit(){
    const user : User = {...this.updateAccountForm.value};
    this.accountService.updateUser(user).subscribe(response => {
      this.router.navigateByUrl('/profile');
    })
  }

  back(){
    this.router.navigate(['/profile']);
  }
}
