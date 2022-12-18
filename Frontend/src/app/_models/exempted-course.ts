import { GUID } from '../../utils/guid';

export interface ExemptedCourse {
  id?: GUID;
  bilkentCredits: number;
  ects: number;
  courseCode: string;
  courseName: string;
  courseType?: string;
}
