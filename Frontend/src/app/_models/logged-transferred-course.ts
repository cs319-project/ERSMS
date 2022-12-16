import { GUID } from 'src/utils/guid';
import { TransferredCourseGroup } from './transferred-course-group';

export interface LoggedTransferredCourse {
  id: GUID;
  transferredCourseGroups: TransferredCourseGroup[];
}
