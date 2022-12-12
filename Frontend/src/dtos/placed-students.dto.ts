import {DepartmentInfoDto} from "./department-info.dto";
import {SemesterInfoDto} from "./semester-info.dto";


export class PlacedStudentsDto {
  firstName: string;
  lastName: string;
  userName: string;
  department: DepartmentInfoDto;
  cgpa: number;
  exchangeScore: number;
  preferredSemester: SemesterInfoDto;
  preferredSchools: ArrayLike<string>;
  exchangeSchool: string;
  isPlaced: boolean;
}
