import { GUID } from '../../utils/guid';

export class ExemptedCourse {
  id: GUID;
  credits: number;
  courseCode: string;
  courseName: string;
  courseType: string;
}