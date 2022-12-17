import { GUID } from '../../utils/guid';

export interface ToDoItem {
  id?: GUID;
  cascadeId?: GUID;
  title: string;
  description: string;
  isComplete: boolean;
  isStarred: boolean;
}
