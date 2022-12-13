import { GUID } from '../../utils/guid';

export class RequestedCourse {
  id: GUID;
  courseCode: string;
  courseName: string;
  credits: number;
}
