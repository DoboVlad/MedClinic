import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/Models/UserModel';
import { AccountService } from '../account.service';

@Injectable({
  providedIn: 'root'
})
export class MedicService {

  constructor(private router: Router,
    private http: HttpClient,
     private account: AccountService) { }

  //get all doctors from database
  getDoctors(){
    return this.http.get<User[]>(this.account.baseUrl + "/users/getDoctors");
  }

  //search a doctor by it's name
  searchDoctor(name: string){
    const params = new HttpParams().set("str", name);
    console.log(this.account.baseUrl + "api/users/searchDoctor" + params);
    return this.http.get<User[]>(this.account.baseUrl + "/users/searchDoctor?",{params});
  }

  medicAccount(){
    return this.http.get<User>(this.account.baseUrl + '/users/MyAccountMedic', {
      headers: {
        'Authorization' : 'Bearer ' + this.account.token
      }
    });
    }

    getUnapprovedUsers(){
      return this.http.get<User[]>(this.account.baseUrl + '/users/getUnapprovedUsers', {
        headers: {
          'Authorization': 'Bearer ' + this.account.token
        }
      });
    }

    approveUser(id: number){
      this.http.put(this.account.baseUrl + '/users/ApproveUser?id=' + id, id, {
        headers: {
          'Authorization': 'Bearer ' + this.account.token
        }
      }).subscribe(user => {
        this.router.navigate(["/home"]);
      });
  }

    getMedicInfo(){
      return this.http.get<User>(this.account.baseUrl + '/users/getMedicInfo',{
        headers: {
          "Authorization": "Bearer " + this.account.token
        }
      })
    }
}
