import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Pdf } from 'src/app/Models/PdfModel';
import { User } from 'src/app/Models/UserModel';
import { AccountService } from '../account.service';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  pdfData: Pdf;
  constructor(private router: Router,
    private http: HttpClient,
     private account: AccountService) { }

  // get a single user from api
  myAccount(){
    return this.http.get<User>(this.account.baseUrl + "/users/MyAccount", {
      headers: {
        'Authorization' : 'Bearer ' + this.account.token
      }
    });
  }

    getApprovedPatients(){
      return this.http.get<User[]>(this.account.baseUrl + '/users/getApprovedUsers',{
        headers: {
          'Authorization': 'Bearer ' + this.account.token
        }
      })
    }

    getAllPatients(){
      return this.http.get<User[]>(this.account.baseUrl + '/users/getPatients', {
        headers: {
          'Authorization': 'Bearer ' + this.account.token
        }
      });
    }

    deletePatient(id: number){
      return this.http.delete(this.account.baseUrl + "/users/DeletePatient/" + id, {
        headers: {
          'Authorization': 'Bearer ' + this.account.token
        }
      });
    }

    generatePdf(pdf: Pdf){
      return this.http.get("https://localhost:5001/api/pdf/generatePDF?id=" + pdf.id +
      '&&reason=' + pdf.reason + '&&treatment=' + pdf.treatment +
      '&&sendTo=' + pdf.sendTo + '&&diagnostic=' + pdf.diagnostic);
    }
}
