import { GUID } from '../../utils/guid';

export interface Approval {
  id?: GUID;
  name: string;
  dateOfApproval: Date;
  isApproved: boolean;
  comment: string;
}
