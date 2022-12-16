import { GUID } from 'src/utils/guid';

export interface Message {
  id: GUID;
  senderUsername: string;
  recipientUsername: string;
  content: string;
  messageSent: Date;
}
