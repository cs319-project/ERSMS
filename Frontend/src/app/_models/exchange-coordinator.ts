import { DepartmentInfo } from './department-info';
import { ToDoItem } from './to-do-item';
import { DomainUser } from './domain-user';

export interface ExchangeCoordinator extends DomainUser {
  department: DepartmentInfo;
  toDoList: ArrayLike<ToDoItem>;
}
