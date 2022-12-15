import { GUID } from '../../utils/guid';

export interface ExemptedCourse {
  id: GUID;
  bilkentCredits: number;
  ECTS: number;
  courseCode: string;
  courseName: string;
  courseType: string;
}
