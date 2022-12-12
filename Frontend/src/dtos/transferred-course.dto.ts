import {GUID} from "../utils/guid";

export class TransferredCourseDto {
  id: GUID;
  courseCode: string;
  courseName: string;
  credits: number;
  grade: string;
}
