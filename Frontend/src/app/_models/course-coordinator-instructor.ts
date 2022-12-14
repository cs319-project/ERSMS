import { DepartmentInfo } from './department-info';
import { Course } from './course';
import { DomainUser } from './domain-user';

export interface CourseCoordinatorInstructor extends DomainUser {
  department: DepartmentInfo;
  course: Course;
  isCourseCoordinator: boolean;
}
