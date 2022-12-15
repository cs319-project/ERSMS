import { GUID } from '../../utils/guid';

export interface TransferredCourse {
  id: GUID;
  courseCode: string;
  courseName: string;
  ECTS: number;
  grade: string;
}
