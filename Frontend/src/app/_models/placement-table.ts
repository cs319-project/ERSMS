import { GUID } from '../../utils/guid';
import { DepartmentInfo } from './department-info';

export class PlacementTable {
  id: GUID;
  department: DepartmentInfo;
  fileName: string;
  uploadTime: Date;
}
