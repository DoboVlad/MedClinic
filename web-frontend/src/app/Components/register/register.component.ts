import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { User } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  user: User;
  doctors: User[];
  constructor(public userService: UserService) { }

  ngOnInit(): void {
    this.userService.getDoctors().subscribe(doctors => {
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
      "doctorId": new FormControl(null, Validators.required)
    });
  }

  onSubmit(){
    this.user = {...this.registerForm.value};
    console.log(this.user);
    this.userService.registerUser(this.user);
  }
}
