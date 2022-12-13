import { GUID } from '../../utils/guid';
import { RequestedCourseGroup } from './requested-course-group';
import { Approval } from './approval';

export class PreApprovalForm {
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
  requestedCourseGroups: ArrayLike<RequestedCourseGroup>;
  exchangeCoordinatorApproval: Approval;
  facultyAdministrationBoardApproval: Approval;
}
