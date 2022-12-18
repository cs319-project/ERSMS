import { GUID } from 'src/utils/guid';

export interface NotificationERSMS {
  id: GUID;
  read: boolean;
  content: string;
  userId: GUID;
}
