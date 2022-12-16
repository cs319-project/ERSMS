import { DepartmentInfo } from './department-info';
import { SemesterInfo } from './semester-info';
import { CteForm } from './cte-form';
import { PreApprovalForm } from './pre-approval-form';
import { EquivalenceRequest } from './equivalence-request';
import { DomainUser } from './domain-user';

export interface Student extends DomainUser {
  entranceYear: number;
  major: DepartmentInfo;
  minors: DepartmentInfo[];
  cgpa: number;
  exchangeScore: number;
  preferredSemester: SemesterInfo;
  preferredSchools: string[];
  exchangeSchool: string;
  cteForms: CteForm[];
  preApprovalForms: PreApprovalForm[];
  equivalenceRequestForms: EquivalenceRequest[];
}
