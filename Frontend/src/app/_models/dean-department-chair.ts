import { DepartmentInfo } from './department-info';
import { DomainUser } from './domain-user';

export interface DeanDepartmentChair extends DomainUser {
  department: DepartmentInfo;
  isDean: boolean;
}
