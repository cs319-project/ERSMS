import { DepartmentInfo } from './department-info';

export interface Register {
  actorType: Required<string>;
  email: string;
  userName: Required<string>;
  firstName: Required<string>;
  lastName: Required<string>;
  password: Required<string>;
  department: DepartmentInfo;
  isDean: boolean;
  isCourseCoordinator: boolean;
}
