import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { User } from 'src/app/Models/UserModel';
import { AccountService } from 'src/app/Services/account.service';
import { MedicService } from 'src/app/Services/MedicService/medic.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  user: User;
  doctors: User[];
  constructor(public accountService: AccountService,
    private medicService: MedicService) { }

  ngOnInit(): void {
    this.medicService.getDoctors().subscribe(doctors => {
      this.doctors = doctors;
    });
    this.registerForm = new FormGroup({
      "firstName": new FormControl(null, Validators.required),
      "lastName": new FormControl(null, Validators.required),
      "cnp": new FormControl(null, Validators.required),
      "dateOfBirth": new FormControl(null, Validators.required),
      "email": new FormControl(null, Validators.required),
      "phoneNumber": new FormControl(null, Validators.required),
      "password": new FormControl(null, Validators.required),
      "repeatPassword": new FormControl(null, Validators.required),
      "doctorId": new FormControl(null, Validators.required),
      "City": new FormControl(null, Validators.required),
      "Street": new FormControl(null, Validators.required),
      "HomeNumber": new FormControl(null, Validators.required),
      "County": new FormControl(null, Validators.required),
      "Appartment": new FormControl(null),
      "Entrance": new FormControl(null),
    });
  }

  onSubmit(){
    this.user = {...this.registerForm.value};
    console.log(this.user);
    this.accountService.registerUser(this.user);
  }
}
