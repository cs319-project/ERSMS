import { DepartmentInfo } from './department-info';
import { SemesterInfo } from './semester-info';
import { CteForm } from './cte-form';
import { PreApprovalForm } from './pre-approval-form';
import { EquivalanceRequest } from './equivalance-request';
import { DomainUser } from './domain-user';

export class Student extends DomainUser {
  entranceYear: number;
  major: DepartmentInfo;
  minors: ArrayLike<DepartmentInfo>;
  cgpa: number;
  exchangeScore: number;
  preferredSemester: SemesterInfo;
  preferredSchools: ArrayLike<string>;
  exchangeSchool: string;
  cteForms: ArrayLike<CteForm>;
  preApprovalForms: ArrayLike<PreApprovalForm>;
  equivalenceRequestForms: ArrayLike<EquivalanceRequest>;
}
