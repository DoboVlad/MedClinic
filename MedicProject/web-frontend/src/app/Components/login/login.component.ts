import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import {User} from '../../Models/UserModel';
import {UserService} from '../../Services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user: User;
  loginForm: FormGroup;
  constructor(public userService: UserService, private router: Router) { }


  ngOnInit(): void {
    //initialize the reactive form
    this.userService.error = null;
    this.loginForm = new FormGroup({
      "email": new FormControl(null,[Validators.required, Validators.email]),
      "password": new FormControl(null, Validators.required)
    });
  }

  //when the form is submitted, this method apply
  onSubmit(){
    this.user = {...this.loginForm.value};
    this.userService.logInUser(this.user);
    this.router.navigateByUrl("/profile");
  }

  error(){
    if(this.userService.error != null){
      return true;
    }else {
      return false;
    }

  }
}
