import { GUID } from '../../utils/guid';
import { ExemptedCourse } from './exempted-course';
import { Approval } from './approval';

export interface EquivalanceRequest {
  id: GUID;
  studentId: string;
  hostCourseName: string;
  hostCourseCode: string;
  hostCourseECTS: number;
  fileName: string;
  exemptedCourse: ExemptedCourse;
  instructorApproval: Approval;
  additionalNotes: string;
  isCanceled: boolean;
  isApproved: boolean;
  isRejected: boolean;
  isArchived: boolean;
}
