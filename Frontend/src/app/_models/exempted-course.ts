import { GUID } from '../../utils/guid';

export interface ExemptedCourse {
  id: GUID;
  credits: number;
  courseCode: string;
  courseName: string;
  courseType: string;
}
