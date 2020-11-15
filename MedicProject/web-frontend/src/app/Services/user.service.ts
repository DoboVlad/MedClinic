import { Injectable } from '@angular/core';
import { User } from '../Models/UserModel';
import {HttpClient, HttpParams} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl: String = "https://localhost:5001";
  constructor(private http: HttpClient) { }
  user: User;
  token: string;
  //use post method to login the user
  //call the base url using /login endpoint
  logInUser(user: User){
    this.http.post<User>(this.baseUrl + "/api/users/login", user).subscribe(user => {
      this.user = user;
      this.saveAuthData(user.token);
    });
  }

  registerUser(user: User){
    this.http.post(this.baseUrl + "/api/users/register", user).subscribe(user =>{
      this.user = user;
      console.log(user);
    });
  }

  autoAuthUser(){
    const authInfo = this.getAuthData();
  }

  saveAuthData(token: string){
    localStorage.setItem('token', token);
  }

  clearAuthData(){
    localStorage.removeItem("token");
  }

  getAuthData(){
    const token = localStorage.getItem("token");
    if(token){
      return;
    }
    return{
      token: token,
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
    const params = new HttpParams().set("id", name);
    return this.http.get<User>(this.baseUrl + "/api/users/getUser", {params});
  }
}
