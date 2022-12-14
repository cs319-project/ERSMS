import { ActorsType } from '../_types/actors-type';

export interface LoggedInUser {
  userName: string;
  userDetails: any;
  roles: string[];
  token: string;
}
