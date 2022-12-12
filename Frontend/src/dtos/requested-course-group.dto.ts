import {GUID} from "../utils/guid";
import {RequestedCourseDto} from "./requested-course.dto";
import {ExemptedCourseDto} from "./exempted-course.dto";

export class RequestedCourseGroupDto {
  id: GUID;
  requestedCourses: ArrayLike<RequestedCourseDto>;
  requestedExemptedCourse: ExemptedCourseDto;
}
