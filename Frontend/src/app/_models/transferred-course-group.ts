import { GUID } from '../../utils/guid';
import { TransferredCourse } from './transferred-course';
import { ExemptedCourse } from './exempted-course';

export interface TransferredCourseGroup {
  id: GUID;
  transferredCourses: TransferredCourse[];
  exemptedCourse: ExemptedCourse;
}
