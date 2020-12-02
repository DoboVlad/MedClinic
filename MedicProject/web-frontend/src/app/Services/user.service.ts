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
  role: number = 0;
  isUserLoggedIn: boolean;
  isFetching: boolean;
  language: number = 0;
  isApproved: boolean;
  //use post method to login the user
  //call the base url using /login endpoint
  logInUser(user: User){
    this.http.post<User>(this.baseUrl + "/api/users/login", user).subscribe(user => {
      this.user = user;
      this.saveAuthData(user.token, user.role);
      this.token = user.token;
      this.isFetching = true;
      this.role = user.role;
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
      this.role = +authInfo.role;
      this.isUserLoggedIn = true;
    }
  }

  saveAuthData(token: string, role: number){
    localStorage.setItem('token', token);
    localStorage.setItem('role', role.toString());
  }

  clearAuthData(){
    localStorage.removeItem("token");
    localStorage.removeItem("role");
    this.isUserLoggedIn = false;
  }

  getAuthData(){
    const token = localStorage.getItem("token");
    const role = localStorage.getItem("role");
    if(!token){
      return;
    }
    return{
      token: token,
      role: role
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
