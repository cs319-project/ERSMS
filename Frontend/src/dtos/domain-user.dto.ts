import {GUID} from "../utils/guid";
import {AppUserDto} from "./app-user.dto";

export class DomainUserDto {
  id: GUID;
  actorType: string;
  firstName: string;
  lastName: string;
  identityUser: AppUserDto;
}
