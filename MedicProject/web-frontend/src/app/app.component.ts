import { Component, OnInit } from '@angular/core';
import { UserService } from './Services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'frontend';
  constructor(public userService: UserService){}

  ngOnInit(){
   this.userService.autoAuthUser();
   console.log(this.userService.isUserLoggedIn);
   if(this.userService.token == null){ // check if we have data in local storage
    this.userService.isUserLoggedIn = false;
  } else {
    this.userService.isUserLoggedIn = true;
  }
  }
}
