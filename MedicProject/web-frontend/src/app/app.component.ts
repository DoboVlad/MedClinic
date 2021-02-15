import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from './Services/account.service';
import jwt_decode from "jwt-decode";
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  constructor(public accountService: AccountService, private router: Router){}

  ngOnInit(){

   this.accountService.autoAuthUser();
   if(this.accountService.token == null){ // check if we have data in local storage
    this.accountService.isUserLoggedIn = false;
  } else {
    var decoded: any = jwt_decode(this.accountService.token);
    this.accountService.role = decoded.role;
    this.accountService.isApproved = decoded.Approved;
    this.accountService.isValidated = decoded.Validated;
    this.accountService.isUserLoggedIn = true;
    }
  }

  setLanguage(lang: number){
    this.accountService.language = lang;
    console.log(this.accountService.language);
  }

  logout(){
    this.accountService.role = 0;
    this.accountService.clearAuthData();
    this.router.navigate(["/home"]);
  }
}
