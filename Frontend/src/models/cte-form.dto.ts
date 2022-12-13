import {GUID} from "../utils/guid";
import {ApprovalDto} from "./approval.dto";
import {TransferredCourseGroupDto} from "./transferred-course-group.dto";

export class CteFormDto {
  id: GUID;
  firstName: string;
  lastName: string;
  idNumber: string;
  department: string;
  hostUniversityName: string;
  transferredCourseGroup: ArrayLike<TransferredCourseGroupDto>;
  submissionTime: Date;
  approvalTime: Date;
  chairApproval: ApprovalDto;
  deanApproval: ApprovalDto;
  exchangeCoordinatorApproval: ApprovalDto;
  facultyOfAdministrationBoardApproval: ApprovalDto;
}
