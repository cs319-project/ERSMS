import { DepartmentInfo } from './department-info';
import { SemesterInfo } from './semester-info';

export interface PlacedStudent {
  firstName: string;
  lastName: string;
  userName: string;
  department: DepartmentInfo;
  cgpa: number;
  exchangeScore: number;
  preferredSemester: SemesterInfo;
  preferredSchools: ArrayLike<string>;
  exchangeSchool: string;
  isPlaced: boolean;
}
