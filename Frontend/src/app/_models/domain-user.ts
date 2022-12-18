import { GUID } from '../../utils/guid';
import { AppUser } from './app-user';

export interface DomainUser {
  id: GUID;
  actorType: string;
  firstName: string;
  lastName: string;
  identityUser: AppUser;
}
