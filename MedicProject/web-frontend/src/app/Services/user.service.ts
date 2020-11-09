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
  id: number;
  //use post method to login the user
  //call the base url using /login endpoint
  logInUser(user: User){
    this.http.post(this.baseUrl + "/login", user).subscribe(user => {
      this.user = user;
    });
  }

  registerUser(user: User){
    this.http.post(this.baseUrl + "/register", user).subscribe(user =>{
      this.user = user;
    });
  }

  autoAuthUser(){
    const authInfo = this.getAuthData();
  }

  saveAuthData(token: string, id: number){
    localStorage.setItem('token', token);
    localStorage.setItem('id', id.toString());
  }

  clearAuthData(){
    localStorage.removeItem("token");
    localStorage.removeItem("id");
  }

  getAuthData(){
    const token = localStorage.getItem("token");
    const id = localStorage.getItem("id");
    if(token && id){
      return;
    }
    return{
      token: token,
      id: id
    }
  }

  //search a doctor by it's name
  searchDoctor(name: string){
    const params = new HttpParams().set("name", name);
    console.log(this.baseUrl + "api/users/searchDoctor" + params);
    return this.http.get<User[]>(this.baseUrl + "/api/users/searchDoctor?",{params});
  }

  // get a single user from api
  myAccount(){
    const params = new HttpParams().set("id", name);
    return this.http.get<User>(this.baseUrl + "/api/users/getUser", {params});
  }
}
