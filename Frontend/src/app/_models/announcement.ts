import { GUID } from 'src/utils/guid';

export interface Announcement {
  id?: GUID;
  title?: string;
  sender: string;
  description: string;
  creationDate?: Date;
}
