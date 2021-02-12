import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from './Services/user.service';
import jwt_decode from "jwt-decode";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  constructor(public userService: UserService, private router: Router){}

  ngOnInit(){
   this.userService.autoAuthUser();
   if(this.userService.token == null){ // check if we have data in local storage
    this.userService.isUserLoggedIn = false;
  } else {
    var decoded: any = jwt_decode(this.userService.token);
    this.userService.role = decoded.role;
    this.userService.isApproved = decoded.Approved;
    this.userService.isValidated = decoded.Validated;
    this.userService.isUserLoggedIn = true;
    }
  }

  setLanguage(lang: number){
    this.userService.language = lang;
    console.log(this.userService.language);
  }

  logout(){
    this.userService.role = 0;
    this.userService.clearAuthData();
    this.router.navigate(["/home"]);
  }
}
