import { DomainUser } from '../../../_models/domain-user';

export interface CreateUser {
  appUser: DomainUser;
  password: string;
}
