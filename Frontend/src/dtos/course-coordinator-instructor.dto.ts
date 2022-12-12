import {DepartmentInfoDto} from "./department-info.dto";
import {CourseDto} from "./course.dto";

export class CourseCoordinatorInstructorDto {
  department: DepartmentInfoDto;
  course: CourseDto;
  isCourseCoordinator: boolean;
}
