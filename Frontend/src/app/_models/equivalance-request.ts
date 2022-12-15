import { GUID } from '../../utils/guid';
import { ExemptedCourse } from './exempted-course';
import { Approval } from './approval';

export interface EquivalanceRequest {
  id: GUID;
  studentId: string;
  hostCourseName: string;
  fileName: string;
  exemptedCourse: ExemptedCourse;
  instructorApproval: Approval;
  additionalNotes: string;

}
