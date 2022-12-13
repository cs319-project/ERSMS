import { GUID } from '../../utils/guid';
import { ExemptedCourse } from './exempted-course';
import { Approval } from './approval';

export class EquivalanceRequest {
  id: GUID;
  studentId: string;
  fileName: string;
  exemptedCourse: ExemptedCourse;
  instructorApproval: Approval;
  additionalNotes: string;
}
