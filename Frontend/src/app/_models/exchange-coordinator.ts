import { DepartmentInfo } from './department-info';
import { ToDoItem } from './to-do-item';
import { DomainUser } from './domain-user';

export class ExchangeCoordinator extends DomainUser {
  department: DepartmentInfo;
  toDoList: ArrayLike<ToDoItem>;
}
