import { GUID } from '../../utils/guid';

export class ToDoItem {
  id: GUID;
  cascadeId: GUID;
  title: string;
  description: string;
  isComplete: boolean;
  isStarred: boolean;
}
