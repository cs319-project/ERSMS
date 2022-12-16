import {Student} from "../../_models/student";
import {EquivalanceRequest} from "../../_models/equivalance-request";

export interface ViewEquivalanceRequest{
  student: Student;
  eqReq: EquivalanceRequest;
}
