import { GUID } from '../../utils/guid';
import { RequestedCourse } from './requested-course';
import { ExemptedCourse } from './exempted-course';

export class RequestedCourseGroup {
  id: GUID;
  requestedCourses: ArrayLike<RequestedCourse>;
  requestedExemptedCourse: ExemptedCourse;
}
