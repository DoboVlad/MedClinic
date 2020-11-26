import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './Components/home/home.component';
import { LoginComponent } from './Components/login/login.component';
import { RegisterComponent } from './Components/register/register.component';
import { AppointmentsComponent } from './Components/appointments/appointments.component';
import { DoctorsComponent} from './Components/doctors/doctors.component';
import { ProfileComponent} from './Components/profile/profile.component';
import { ForgotPasswordComponent } from './Components/forgot-password/forgot-password.component';
import {TermsComponent} from './Components/terms/terms.component';
import {AuthGuard} from './Services/auth.guard';
import { WaitingComponent } from './Components/waiting/waiting.component';
import {ApproveGuard} from './Services/approve.guard';
import {DoctorAppointmentsComponent} from './Components/doctor-appointments/doctor-appointments.component';
import {RequestsComponent} from './Components/requests/requests.component';

const routes: Routes = [
  {path: 'home', component: HomeComponent},
  {path: 'appointments', component: AppointmentsComponent, canActivate: [AuthGuard, ApproveGuard]},
  {path: 'doctors', component: DoctorsComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'profile', component: ProfileComponent, canActivate: [AuthGuard, ApproveGuard]},
  {path: 'forgotpassword', component: ForgotPasswordComponent},
  {path: 'terms', component: TermsComponent},
  {path: 'waiting', component: WaitingComponent},
  {path: 'doctorappointments', component: DoctorAppointmentsComponent},
  {path: 'requests', component: RequestsComponent},
  {path: "**", component: HomeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuard, ApproveGuard]
})
export class AppRoutingModule { }
