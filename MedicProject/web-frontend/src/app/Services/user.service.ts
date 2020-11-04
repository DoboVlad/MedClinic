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
}
