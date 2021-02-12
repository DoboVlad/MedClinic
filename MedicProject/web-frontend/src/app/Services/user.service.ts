import { Injectable } from '@angular/core';
import { User } from '../Models/UserModel';
import {HttpClient, HttpParams} from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl: String = environment.apiUrl;
  error: any;
  constructor(private http: HttpClient, private router: Router) { }
  user: User;
  token: string;
  role: number;
  isUserLoggedIn: boolean;
  isFetching: boolean;
  language: number = 0;
  isApproved: boolean;
  isValidated: number = 0;
  //use post method to login the user
  //call the base url using /login endpoint
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

  //get all doctors from database
  getDoctors(){
    return this.http.get<User[]>(this.baseUrl + "/users/getDoctors");
  }

  //search a doctor by it's name
  searchDoctor(name: string){
    const params = new HttpParams().set("str", name);
    console.log(this.baseUrl + "api/users/searchDoctor" + params);
    return this.http.get<User[]>(this.baseUrl + "/users/searchDoctor?",{params});
  }

  // get a single user from api
  myAccount(){
    return this.http.get<User>(this.baseUrl + "/users/MyAccount", {
      headers: {
        'Authorization' : 'Bearer ' + this.token
      }
    });
  }

  medicAccount(){
    return this.http.get<User>(this.baseUrl + '/users/MyAccountMedic', {
      headers: {
        'Authorization' : 'Bearer ' + this.token
      }
    });
    }

    getAllPatients(){
      return this.http.get<User[]>(this.baseUrl + '/users/getPatients', {
        headers: {
          'Authorization': 'Bearer ' + this.token
        }
      });
    }

    getUnapprovedUsers(){
      return this.http.get<User[]>(this.baseUrl + '/users/getUnapprovedUsers', {
        headers: {
          'Authorization': 'Bearer ' + this.token
        }
      });
    }

    approveUser(id: number){
        this.http.put(this.baseUrl + '/users/ApproveUser?id=' + id, id, {
          headers: {
            'Authorization': 'Bearer ' + this.token
          }
        }).subscribe(user => {
          this.router.navigate(["/home"]);
        });
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
