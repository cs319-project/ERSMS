import { DepartmentInfo } from './department-info';
import { ToDoItem } from './to-do-item';

export class ExchangeCoordinator {
  department: DepartmentInfo;
  toDoList: ArrayLike<ToDoItem>;
}
