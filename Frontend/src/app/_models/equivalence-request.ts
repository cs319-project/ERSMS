import { GUID } from '../../utils/guid';
import { ExemptedCourse } from './exempted-course';
import { Approval } from './approval';

export interface EquivalenceRequest {
  id: GUID;
  studentId: string;
  firstName?: string;
  lastName?: string;
  hostUniversityName?: string;
  hostCourseName: string;
  hostCourseCode: string;
  hostCourseEcts: number;
  fileName: string;
  exemptedCourse: ExemptedCourse;
  instructorApproval?: Approval;
  additionalNotes: string;
  isCanceled: boolean;
  isApproved: boolean;
  isRejected: boolean;
  isArchived: boolean;
}
