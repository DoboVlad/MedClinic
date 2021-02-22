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
import {DoctorAppointmentsComponent} from './Components/doctor-appointments/doctor-appointments.component';
import {RequestsComponent} from './Components/requests/requests.component';
import {DoctorPatientsComponent} from './Components/doctor-patients/doctor-patients.component';
import { ChangeDoctorComponent } from './Components/change-doctor/change-doctor.component';
import { ActivateAccountComponent } from './Components/activate-account/activate-account.component';
import { UpdateAccountComponent } from './Components/update-account/update-account.component';

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
  {path: 'doctorappointments', component: DoctorAppointmentsComponent, canActivate: [RoleGuard]},
  {path: 'requests', component: RequestsComponent, canActivate: [RoleGuard]},
  {path: 'doctorpatients', component: DoctorPatientsComponent, canActivate: [RoleGuard]},
  {path: 'change-doctor', component: ChangeDoctorComponent},
  {path: 'updateAccount', component: UpdateAccountComponent},
  {path: "**", component: HomeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuard, ApproveGuard, RoleGuard, LoggedInGuard]
})
export class AppRoutingModule { }
