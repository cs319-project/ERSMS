import { ActorsType } from '../_types/actors-type';

export class LoggedInUser {
  userDetail: Array<ActorsType> = [];
  roles: string[];
  token: string;
}
