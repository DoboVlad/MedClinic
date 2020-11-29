import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from './Services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'frontend';
  constructor(public userService: UserService, private router: Router){}

  ngOnInit(){
   this.userService.autoAuthUser();
   if(this.userService.token == null){ // check if we have data in local storage
    this.userService.isUserLoggedIn = false;
  } else {
    this.userService.isUserLoggedIn = true;
    this.userService.isApproved = true;
    console.log("is Appproved: " + this.userService.isApproved);
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
