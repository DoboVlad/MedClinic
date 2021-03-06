import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router } from '@angular/router';
import {User} from '../../Models/UserModel';
import {AccountService} from '../../Services/account.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user: User;
  loginForm: FormGroup;
  constructor(public accountService: AccountService, private router: Router) { }


  ngOnInit(): void {
    //initialize the reactive form
    this.accountService.error = null;
    this.loginForm = new FormGroup({
      "email": new FormControl(null,[Validators.required, Validators.email]),
      "password": new FormControl(null, Validators.required)
    });
  }
  //when the form is submitted, this method apply
  onSubmit(){
    this.user = {...this.loginForm.value};
    this.accountService.logInUser(this.user);
    this.router.navigateByUrl("/profile");
  }

  error(){
    if(this.accountService.error != null){
      return true;
    }else {
      return false;
    }

  }
}
