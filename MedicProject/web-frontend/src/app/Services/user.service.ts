import { Injectable } from '@angular/core';
import { User } from '../Models/UserModel';
import {HttpClient, HttpParams} from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl: String = "https://localhost:5001";
  constructor(private http: HttpClient, private router: Router) { }
  user: User;
  token: string;
  isUserLoggedIn: boolean;
  isFetching: boolean;
  isApproved: boolean;
  //use post method to login the user
  //call the base url using /login endpoint
  logInUser(user: User){
    this.http.post<User>(this.baseUrl + "/api/users/login", user).subscribe(user => {
      this.user = user;
      this.saveAuthData(user.token);
      this.isUserLoggedIn = true;
      console.log(this.user.isApproved);
      if(this.user.isApproved == 1){
        this.isApproved = true;
        this.router.navigateByUrl("/profile");
      }
      else this.isApproved = false;
    });

  }

  registerUser(user: User){
    this.http.post(this.baseUrl + "/api/users/register", user).subscribe(user =>{
      this.user = user;
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
      token: token
    }
  }

  //get all doctors from database
  getDoctors(){
    return this.http.get<User[]>(this.baseUrl + "/api/users/getDoctors");
  }

  //search a doctor by it's name
  searchDoctor(name: string){
    const params = new HttpParams().set("str", name);
    console.log(this.baseUrl + "api/users/searchDoctor" + params);
    return this.http.get<User[]>(this.baseUrl + "/api/users/searchDoctor?",{params});
  }

  // get a single user from api
  myAccount(){
    return this.http.get<User>(this.baseUrl + "/api/users/MyAccount", {
      headers: {
        'Authorization' : 'Bearer ' + this.token
      }
    });
  }

  medicAccount(){
    return this.http.get<User>(this.baseUrl + '/api/users/MyAccountMedic', {
      headers: {
        'Authorization' : 'Bearer ' + this.token
      }
    });
    }
}
