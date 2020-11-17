import { User } from './UserModel';

export class Appointment{
  id?: number;
  hour?: string;
  date?: Date;
  user?: User;
}
