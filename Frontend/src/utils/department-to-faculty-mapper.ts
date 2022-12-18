import { DepartmentsEnum } from 'src/app/_models/enum/departments-enum';
import { FacultiesEnum } from 'src/app/_models/enum/faculties-enum';

export class DepartmentToFacultyMapper {
  public static map(department: string): string {
    if (
      department == DepartmentsEnum.CS ||
      department == DepartmentsEnum.EEE ||
      department == DepartmentsEnum.ME ||
      department == DepartmentsEnum.IE
    ) {
      return FacultiesEnum.Engineering;
    } else if (
      department == DepartmentsEnum.ARCH ||
      department == DepartmentsEnum.COMD ||
      department == DepartmentsEnum.FA ||
      department == DepartmentsEnum.GRA ||
      department == DepartmentsEnum.IAED ||
      department == DepartmentsEnum.LAUD
    ) {
      return FacultiesEnum.ArtsDesignArchitecture;
    } else if (department === DepartmentsEnum.MAN) {
      return FacultiesEnum.BusinessAdministration;
    } else if (
      department == DepartmentsEnum.ECON ||
      department == DepartmentsEnum.HIST ||
      department == DepartmentsEnum.POLS ||
      department == DepartmentsEnum.IR ||
      department == DepartmentsEnum.PSYC
    ) {
      return FacultiesEnum.EconomicsAdministrativeSocialSciences;
    } else if (
      department == DepartmentsEnum.AMER ||
      department == DepartmentsEnum.ARCHAE ||
      department == DepartmentsEnum.ELIT ||
      department == DepartmentsEnum.PHIL ||
      department == DepartmentsEnum.TRIN ||
      department == DepartmentsEnum.TURK
    ) {
      return FacultiesEnum.HumanitiesLetters;
    } else if (department === DepartmentsEnum.LAW) {
      return FacultiesEnum.Law;
    } else if (
      department == DepartmentsEnum.CHEM ||
      department == DepartmentsEnum.MATH ||
      department == DepartmentsEnum.PHYS ||
      department == DepartmentsEnum.MBG
    ) {
      return FacultiesEnum.Science;
    } else if (
      department == DepartmentsEnum.MSC ||
      department == DepartmentsEnum.THR
    ) {
      return FacultiesEnum.MusicPerformingArts;
    } else if (
      department == DepartmentsEnum.CTIS ||
      department == DepartmentsEnum.THM
    ) {
      return FacultiesEnum.AppliedSciences;
    } else {
      return FacultiesEnum.NotSpecified;
    }
  }
}
