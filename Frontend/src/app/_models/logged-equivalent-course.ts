import { GUID } from 'src/utils/guid';
import { ExemptedCourse } from './exempted-course';

export interface LoggedEquivalentCourse {
  id: GUID;
  exemptedCourse: ExemptedCourse;
  hostCourseCode: string;
  hostCourseName: string;
  logTime?: Date;
  hostCourseEcts: number;
  hostSchool: string;
}
