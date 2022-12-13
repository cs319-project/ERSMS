import {DepartmentInfoDto} from "./department-info.dto";

export class RegisterDto {
  actorType: Required<string>;
  email: string;
  userName: Required<string>;
  firstName: Required<string>;
  lastName: Required<string>;
  password: Required<string>;
  department: DepartmentInfoDto;
  isDean: boolean;
  isCourseCoordinator: boolean;
}
