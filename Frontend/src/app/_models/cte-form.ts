import { GUID } from '../../utils/guid';
import { Approval } from './approval';
import { TransferredCourseGroup } from './transferred-course-group';

export interface CteForm {
  id: GUID;
  firstName: string;
  lastName: string;
  idNumber: string;
  department: string;
  hostUniversityName: string;
  transferredCourseGroup: TransferredCourseGroup[];
  submissionTime: Date;
  approvalTime: Date;
  chairApproval: Approval;
  deanApproval: Approval;
  exchangeCoordinatorApproval: Approval;
  facultyOfAdministrationBoardApproval: Approval;
}
