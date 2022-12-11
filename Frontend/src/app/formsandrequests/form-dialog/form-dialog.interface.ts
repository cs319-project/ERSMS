export interface FormDialogData {
  studentName: string;
  studentEmail: string;
  studentId: string;
  studentCgpa: number;
  studentEntranceYear: number;
  studentDepartment: string;
  exchangeProgram: string;
  exchangeSchool: string;
  exchangeTerm: string;
  formId: number;
  formType: string;
  formStatus: string;
  formAssignedPrivilegedUser: string;
  formAssignedPrivilegedUserRole: string;
  formDate: Date;
  formSignature: string; // TODO: change signature type
}
