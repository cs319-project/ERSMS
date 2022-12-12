import {GUID} from "../utils/guid";
import {RequestedCourseGroupDto} from "./requested-course-group.dto";
import {ApprovalDto} from "./approval.dto";

export class PreApprovalFormDto {
  id: GUID;
  firstName: string;
  lastName: string;
  idNumber: string;
  department: string;
  hostUniversityName: string;
  academicYear: string;
  semester: string;
  submissionTime: Date;
  approvalTime: Date;
  requestedCourseGroups: ArrayLike<RequestedCourseGroupDto>;
  exchangeCoordinatorApproval: ApprovalDto;
  facultyAdministrationBoardApproval: ApprovalDto;
}
