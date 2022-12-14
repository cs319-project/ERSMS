import { GUID } from '../../utils/guid';
import { TransferredCourse } from './transferred-course';

export interface TransferredCourseGroup {
  id: GUID;
  transferredCourses: ArrayLike<TransferredCourse>;
}
