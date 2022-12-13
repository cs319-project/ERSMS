import { GUID } from '../../utils/guid';

export class Approval {
  id: GUID;
  name: string;
  dateOfApproval: Date;
  isApproved: boolean;
}
