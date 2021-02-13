import { Injectable } from '@angular/core';
import { User } from '../Models/UserModel';
import {HttpClient, HttpParams} from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl: String = environment.apiUrl;
  error: any;
  constructor(private http: HttpClient, private router: Router) { }
  user: User;
  token: string;
  role: number = 0;
  isUserLoggedIn: boolean;
  isFetching: boolean;
  language: number = 0;
  isApproved: boolean;
  isValidated: number = 0;


  logInUser(user: User){
    this.http.post<User>(this.baseUrl + "/users/login", user).subscribe(user => {
      this.user = user;
      this.saveAuthData(user.token);
      this.token = user.token;
      this.isFetching = true;
      this.role = user.isMedic;
      if(this.user.validated == 0){
        this.router.navigate(["/activate-account"]);
      }else{
      if(this.user.isApproved == 1){
        this.isApproved = true;
        this.isUserLoggedIn = true;
        this.isFetching = false;
        this.router.navigateByUrl("/profile");
      }
      else {
         this.isApproved = false;
         this.isUserLoggedIn = true;
         this.router.navigate(["/waiting"]);
      }
    }
    }, error => {
      this.error = error.status;
    });
  }

  registerUser(user: User){
    this.http.post(this.baseUrl + "/users/register", user).subscribe(user =>{
      this.user = user;
      if(this.user.validated == 0){
        this.router.navigate(["/activate-account"]);
      }
      this.router.navigate(["/home"]);
    });
  }

  autoAuthUser(){
    const authInfo = this.getAuthData();
    if(authInfo!= null){
      this.token = authInfo.token;
      this.isUserLoggedIn = true;
    }
  }

  saveAuthData(token: string){
    localStorage.setItem('token', token);
  }

  clearAuthData(){
    localStorage.removeItem("token");
    this.isUserLoggedIn = false;
  }

  getAuthData(){
    const token = localStorage.getItem("token");
    if(!token){
      return;
    }
    return{
      token: token,
    }
  }

    activateAccount(code: string){
      this.http.post<User>(this.baseUrl + '/users/VerifyAccount?token=' + code, null).subscribe(user => {
        if(user.isApproved == 1){
          this.isApproved = true;
          this.isUserLoggedIn = true;
          this.isFetching = false;
          this.router.navigateByUrl("/profile");
        }
        else {
           this.isApproved = false;
           this.isUserLoggedIn = true;
           this.router.navigate(["/waiting"]);
        }
      });
    }
}
