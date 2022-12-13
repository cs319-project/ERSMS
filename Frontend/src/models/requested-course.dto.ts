import {GUID} from "../utils/guid";

export class RequestedCourseDto {
  id: GUID;
  courseCode: string;
  courseName: string;
  credits: number;
}
