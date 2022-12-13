import {GUID} from "../utils/guid";

export class ApprovalDto {
  id: GUID;
  name: string;
  dateOfApproval: Date;
  isApproved: boolean;
}
