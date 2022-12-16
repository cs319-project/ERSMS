import {Student} from "../../_models/student";
import {PreApprovalForm} from "../../_models/pre-approval-form";

export interface ViewPreApprovalForm {
  student: Student;
  preApprovalForm: PreApprovalForm;
}
