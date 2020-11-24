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
    RequestsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
