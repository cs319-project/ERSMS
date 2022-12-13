import { GUID } from '../../utils/guid';
import { TransferredCourse } from './transferred-course';

export class TransferredCourseGroup {
  id: GUID;
  transferredCourses: ArrayLike<TransferredCourse>;
}
