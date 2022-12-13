import { GUID } from '../../utils/guid';

export class TransferredCourse {
  id: GUID;
  courseCode: string;
  courseName: string;
  credits: number;
  grade: string;
}
