import { Admin } from '../_models/admin';
import { CourseCoordinatorInstructor } from '../_models/course-coordinator-instructor';
import { DeanDepartmentChair } from '../_models/dean-department-chair';
import { ExchangeCoordinator } from '../_models/exchange-coordinator';
import { OISEP } from '../_models/oisep';
import { Student } from '../_models/student';

export type ActorsType =
  | Admin
  | Student
  | OISEP
  | ExchangeCoordinator
  | DeanDepartmentChair
  | CourseCoordinatorInstructor;
