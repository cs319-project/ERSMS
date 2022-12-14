import { GUID } from '../../utils/guid';
import { DepartmentInfo } from './department-info';

export interface PlacementTable {
  id: GUID;
  department: DepartmentInfo;
  fileName: string;
  uploadTime: Date;
}
