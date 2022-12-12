import {GUID} from "../utils/guid";


export class ToDoItemDto {
  id: GUID;
  cascadeId: GUID;
  title: string;
  description: string;
  isComplete: boolean;
  isStarred: boolean;
}
