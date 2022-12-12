import {DepartmentInfoDto} from "./department-info.dto";
import {ToDoItemDto} from "./to-do-item.dto";

export class ExchangeCoordinatorDto {
  department: DepartmentInfoDto;
  toDoList: ArrayLike<ToDoItemDto>;
}
