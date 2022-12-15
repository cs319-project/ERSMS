import { GUID } from '../../utils/guid';
import { RequestedCourse } from './requested-course';
import { ExemptedCourse } from './exempted-course';

export interface RequestedCourseGroup {
  id: GUID;
  requestedCourses: RequestedCourse[];
  requestedExemptedCourse: ExemptedCourse;
}
