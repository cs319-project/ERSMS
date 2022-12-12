import {GUID} from "../utils/guid";
import {TransferredCourseDto} from "./transferred-course.dto";

export class TransferredCourseGroupDto {
  id: GUID;
  transferredCourses: ArrayLike<TransferredCourseDto>;
}
