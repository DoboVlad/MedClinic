import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Appointment } from '../../Models/AppointmentModel';
import { AccountService } from '../account.service';

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {
  baseUrl: string = environment.apiUrl + "/api/appointments";
  constructor(private http: HttpClient, private accountService: AccountService) { }

  isSuccesfully: boolean;
  createAppointment(appointment: Appointment){
    this.http.post(this.baseUrl + "/createApp", appointment, {
      headers: {
        'Authorization': 'Bearer ' + this.accountService.token
      }
    }).subscribe(app => {
      console.log(app);
      this.isSuccesfully = true;
    })
  }

  getPastAppointments(){
    return this.http.get<Appointment[]>(this.baseUrl + "/historyAppointments",{
      headers: {
        "Authorization": "Bearer " + this.accountService.token
      }
    });
  }

  getAllMedicAppointments(){
    return this.http.get<Appointment[]>(this.baseUrl + "/allDoctorApp/1",{
      headers: {
        "Authorization": "Bearer " + this.accountService.token
      }
    });
  }
}