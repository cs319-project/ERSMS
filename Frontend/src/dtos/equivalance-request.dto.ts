import {GUID} from "../utils/guid";
import {ExemptedCourseDto} from "./exempted-course.dto";
import {ApprovalDto} from "./approval.dto";

export class EquivalanceRequestDto {
  id: GUID;
  studentId: string;
  fileName: string;
  exemptedCourse: ExemptedCourseDto;
  instructorApproval: ApprovalDto;
  additionalNotes: string;
}
