import { DepartmentInfo } from './department-info';
import { DomainUser } from './domain-user';

export class DeanDepartmentChair extends DomainUser {
  department: DepartmentInfo;
  isDean: boolean;
}
