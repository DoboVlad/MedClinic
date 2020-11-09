import { Injectable } from '@angular/core';
import { User } from '../Models/UserModel';
import {HttpClient} from '@angular/common/http';

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
}
