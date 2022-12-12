import {GUID} from "../utils/guid";


export class ExemptedCourseDto {
  id: GUID;
  credits: number;
  courseCode: string;
  courseName: string;
  courseType: string;
}
