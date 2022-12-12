import {DepartmentInfoDto} from "./department-info.dto";
import {SemesterInfoDto} from "./semester-info.dto";
import {CteFormDto} from "./cte-form.dto";
import {PreApprovalFormDto} from "./pre-approval-form.dto";
import {EquivalanceRequestDto} from "./equivalance-request.dto";

export class StudentDto {
  entranceYear: number;
  major: DepartmentInfoDto;
  minors: ArrayLike<DepartmentInfoDto>;
  cgpa: number;
  exchangeScore: number;
  preferredSemester: SemesterInfoDto;
  preferredSchools: ArrayLike<string>;
  exchangeSchool: string;
  cteForms: ArrayLike<CteFormDto>;
  preApprovalForms: ArrayLike<PreApprovalFormDto>;
  equivalenceRequestForms: ArrayLike<EquivalanceRequestDto>;
}
