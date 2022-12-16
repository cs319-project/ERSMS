import { GUID } from 'src/utils/guid';

export interface Notification {
  id: GUID;
  read: boolean;
  content: string;
  userId: GUID;
}
