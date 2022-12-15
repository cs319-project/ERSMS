import { GUID } from 'src/utils/guid';

export interface Announcement {
  guid: GUID;
  title: string;
  sender: string;
  description: string;
  creationDate: Date;
}
