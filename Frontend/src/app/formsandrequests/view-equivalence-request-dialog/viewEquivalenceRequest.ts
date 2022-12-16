import {Student} from "../../_models/student";
import {EquivalenceRequest} from "../../_models/equivalence-request";

export interface ViewEquivalenceRequest{
  student: Student;
  eqReq: EquivalenceRequest;
}
