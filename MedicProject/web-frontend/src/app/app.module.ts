import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './Components/home/home.component';
import { LoginComponent } from './Components/login/login.component';
import { RegisterComponent } from './Components/register/register.component';
import { DoctorsComponent } from './Components/doctors/doctors.component';
import { AppointmentsComponent } from './Components/appointments/appointments.component';
import { ProfileComponent } from './Components/profile/profile.component';
import { ForgotPasswordComponent } from './Components/forgot-password/forgot-password.component';
import { TermsComponent } from './Components/terms/terms.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { SpinnerComponent } from './Components/spinner/spinner.component';
import { WaitingComponent } from './Components/waiting/waiting.component';
import { DoctorAppointmentsComponent } from './Components/doctor-appointments/doctor-appointments.component';
import { RequestsComponent } from './Components/requests/requests.component';
import { DoctorPatientsComponent } from './Components/doctor-patients/doctor-patients.component';
import { HistoricComponent } from './Components/historic/historic.component';
import { ChangeDoctorComponent } from './Components/change-doctor/change-doctor.component';
import {MatInputModule} from '@angular/material/input';
import { FullCalendarModule } from '@fullcalendar/angular';
import dayGridPlugin from '@fullcalendar/daygrid'; // a plugin
import interactionPlugin from '@fullcalendar/interaction';
import { ActivateAccountComponent } from './Components/activate-account/activate-account.component'; // a plugin

FullCalendarModule.registerPlugins([ // register FullCalendar plugins
  dayGridPlugin,
  interactionPlugin
]);

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    DoctorsComponent,
    AppointmentsComponent,
    ProfileComponent,
    ForgotPasswordComponent,
    TermsComponent,
    SpinnerComponent,
    WaitingComponent,
    DoctorAppointmentsComponent,
    RequestsComponent,
    DoctorPatientsComponent,
    HistoricComponent,
    ChangeDoctorComponent,
    ActivateAccountComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatInputModule,
    FullCalendarModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
