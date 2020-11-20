import {Doctor} from './DoctorModel';

export class User{
  id? : number;
  firstName?: String;
  lastName?: String;
  email?: String;
  password?: String;
  repeatPassword?: String;
  description?: string;
  dateOfBirth?: Date;
  gender?: string;
  cnp?: string;
  age?: number;
  phoneNumber?: String;
  isApproved?: number;
  token?: string;
  doctorId?: number;
  doctor?: Doctor;
}
