import {DomainUser} from "../../../_models/domain-user";
import {DepartmentInfo} from "../../../_models/department-info";
import {ActorsEnum} from "../../../_models/enum/actors-enum";

export interface ViewUser{
  user: DomainUser;
  userType: ActorsEnum;
  entranceYear: number;
  major: DepartmentInfo;
  minors: DepartmentInfo[];
  cgpa: number;
  isDean: boolean;

}
