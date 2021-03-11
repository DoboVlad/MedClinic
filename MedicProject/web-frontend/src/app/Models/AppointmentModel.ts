import { User } from './UserModel';

export class Appointment{
  id?: number;
  hour?: string;
  date?: Date;
  user?: User;
  startHour?: Date;
  endHour?: Date;
  title?: string;
  firstName? : string;
  lastName? : string;
}
