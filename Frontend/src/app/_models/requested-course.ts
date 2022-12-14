import { GUID } from '../../utils/guid';

export interface RequestedCourse {
  id: GUID;
  courseCode: string;
  courseName: string;
  credits: number;
}
