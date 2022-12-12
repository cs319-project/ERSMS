import {GUID} from "../utils/guid";
import {DepartmentInfoDto} from "./department-info.dto";

export class PlacementTableDto {
  id: GUID;
  department: DepartmentInfoDto;
  fileName: string;
  uploadTime: Date;
}
