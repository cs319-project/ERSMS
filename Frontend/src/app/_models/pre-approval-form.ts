import { GUID } from '../../utils/guid';
import { RequestedCourseGroup } from './requested-course-group';
import { Approval } from './approval';

export interface PreApprovalForm {
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
  requestedCourseGroups: RequestedCourseGroup[];
  exchangeCoordinatorApproval?: Approval;
  facultyAdministrationBoardApproval?: Approval;
  isCanceled: boolean;
  isApproved: boolean;
  isRejected: boolean;
  isArchived: boolean;
}
