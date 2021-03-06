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
import {AuthGuard} from './Services/Guards/auth.guard';
import {LoggedInGuard} from './Services/Guards/logged-in.guard';
import { WaitingComponent } from './Components/waiting/waiting.component';
import {ApproveGuard} from './Services/Guards/approve.guard';
import {RoleGuard} from './Services/Guards/role.guard';
import {DoctorPatientsComponent} from './Components/doctor-patients/doctor-patients.component';
import { ChangeDoctorComponent } from './Components/change-doctor/change-doctor.component';
import { ActivateAccountComponent } from './Components/activate-account/activate-account.component';
import { UpdateAccountComponent } from './Components/update-account/update-account.component';
import { MessagesComponent } from './Components/messages/messages.component';
import { CalendarComponent } from './Components/calendar/calendar.component';
import { HistoryComponent } from './Components/history/history.component';

const routes: Routes = [
  {path: 'home', component: HomeComponent},
  {path: 'appointments', component: AppointmentsComponent, canActivate: [AuthGuard, ApproveGuard]},
  {path: 'doctors', component: DoctorsComponent},
  {path: 'login', component: LoginComponent, canActivate:[LoggedInGuard]},
  {path: 'register', component: RegisterComponent, canActivate:[LoggedInGuard]},
  {path: 'profile', component: ProfileComponent, canActivate: [AuthGuard, ApproveGuard]},
  {path: 'forgotpassword', component: ForgotPasswordComponent, canActivate:[LoggedInGuard]},
  {path: 'terms', component: TermsComponent},
  {path: 'activate-account', component: ActivateAccountComponent, canActivate:[AuthGuard]},
  {path: 'waiting', component: WaitingComponent, canActivate:[AuthGuard]},
  {path: 'doctorpatients', component: DoctorPatientsComponent, canActivate: [RoleGuard]},
  {path: 'updateAccount', component: UpdateAccountComponent,  canActivate: [AuthGuard]},
  {path: 'messages', component: MessagesComponent,  canActivate: [AuthGuard, ApproveGuard]},
  {path: "doctorappointments", component: CalendarComponent,  canActivate: [RoleGuard]},
  {path: "history", component: HistoryComponent, canActivate: [AuthGuard, ApproveGuard]},
  {path: "**", component: HomeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuard, ApproveGuard, RoleGuard, LoggedInGuard]
})
export class AppRoutingModule { }
