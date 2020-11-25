import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Appointment } from '../Models/AppointmentModel';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {
  baseUrl: string = "https://localhost:5001/api/appointments";
  constructor(private http: HttpClient, private userService: UserService) { }

  isSuccesfully: boolean;
  createAppointment(appointment: Appointment){
    this.http.post(this.baseUrl + "/createApp", appointment, {
      headers: {
        'Authorization': 'Bearer ' + this.userService.token
      }
    }).subscribe(app => {
      console.log(app);
      this.isSuccesfully = true;
    })
  }

  getPastAppointments(){
    return this.http.get<Appointment[]>(this.baseUrl + "/historyAppointments",{
      headers: {
        "Authorization": "Bearer " + this.userService.token
      }
    });
  }
}
