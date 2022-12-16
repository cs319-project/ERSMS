import { Component, ViewChild, OnInit, AfterViewInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort, Sort } from '@angular/material/sort';
import { FormDialogComponent } from './form-dialog/form-dialog.component';
import { SelectionModel } from '@angular/cdk/collections';
import {
  createNewUser,
  createRandomDialogData,
  NAMES,
  SCHOOLS,
  UserData
} from '../logging/logging.component';
import { PreApprovalForm } from '../_models/pre-approval-form';
import { PreapprovalFormDialogComponent } from './preapproval-form-dialog/preapproval-form-dialog.component';
import { GUID } from '../../utils/guid';
import { EquivalenceRequest } from '../_models/equivalence-request';
import { EquivalenceRequestDialogComponent } from './equivalence-request-dialog/equivalence-request-dialog.component';
import { CteForm } from '../_models/cte-form';
import { CteFormDialogComponent } from './cte-form-dialog/cte-form-dialog.component';
import { Student } from '../_models/student';
import { DepartmentsEnum } from '../_models/enum/departments-enum';
import { FacultiesEnum } from '../_models/enum/faculties-enum';
import { ViewCTEForm } from './view-cte-form-dialog/viewCTEForm';
import { ViewCteFormDialogComponent } from './view-cte-form-dialog/view-cte-form-dialog.component';
import { ViewPreApprovalForm } from './view-preapproval-form-dialog/viewPreApprovalForm';
import { ViewPreapprovalFormDialogComponent } from './view-preapproval-form-dialog/view-preapproval-form-dialog.component';
import { ViewEquivalenceRequest } from './view-equivalence-request-dialog/viewEquivalenceRequest';
import { ViewEquivalenceRequestDialogComponent } from './view-equivalence-request-dialog/view-equivalence-request-dialog.component';

@Component({
  selector: 'app-formsandrequests',
  templateUrl: './formsandrequests.component.html',
  styleUrls: ['./formsandrequests.component.css']
})
export class FormsAndRequestsComponent {
  displayedColumns = ['id', 'student', 'date', 'type', 'school', 'status'];
  displayedColumns2 = ['id', 'student', 'date', 'school', 'status'];
  displayedColumns3 = ['id', 'date', 'type', 'status'];

  dataSource: MatTableDataSource<UserData>;
  preapprovalDataSource: MatTableDataSource<UserData>;
  cteDataSource: MatTableDataSource<UserData>;
  courseEquivalenceDataSource: MatTableDataSource<UserData>;
  studentDataSource: MatTableDataSource<UserData>;

  @ViewChild('paginatorS') paginatorS: MatPaginator;
  @ViewChild('sorterS') sorterS: MatSort;

  @ViewChild('paginator') paginator: MatPaginator;
  @ViewChild('paginator2') paginator2: MatPaginator;
  @ViewChild('paginator3') paginator3: MatPaginator;
  @ViewChild('paginator4') paginator4: MatPaginator;

  @ViewChild('sorter1') sorter1: MatSort;
  @ViewChild('sorter2') sorter2: MatSort;
  @ViewChild('sorter3') sorter3: MatSort;
  @ViewChild('sorter4') sorter4: MatSort;

  selection = new SelectionModel<UserData>(true, []);

  activatedRow = null;

  preApprovalForm: PreApprovalForm;
  equivalanceRequest: EquivalenceRequest;
  cteForm: CteForm;

  constructor(private dialog: MatDialog, private _snackBar: MatSnackBar) {
    const users: UserData[] = [];
    const preapprovalUsers: UserData[] = [];
    const cteUsers: UserData[] = [];
    const courseequivalenceUsers: UserData[] = [];
    const studentUser: UserData[] = [];

    for (let i = 1; i <= 100; i++) {
      users.push(createNewUser(i, (status = 'Processing')));
    }
    for (let i = 1; i <= 10; i++) {
      studentUser.push(createNewUser(i));
    }

    for (let k = 0; k < users.length; k++) {
      if (users[k].type == 'PreApproval Form') {
        preapprovalUsers.push(users[k]);
      } else if (users[k].type == 'CTE Form') {
        cteUsers.push(users[k]);
      } else if (users[k].type == 'Course Eq. Request') {
        courseequivalenceUsers.push(users[k]);
      }
    }

    // Assign the data to the data source for the table to render
    this.dataSource = new MatTableDataSource(users);
    this.preapprovalDataSource = new MatTableDataSource(preapprovalUsers);
    this.cteDataSource = new MatTableDataSource(cteUsers);
    this.courseEquivalenceDataSource = new MatTableDataSource(
      courseequivalenceUsers
    );
    this.studentDataSource = new MatTableDataSource(studentUser);
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.preapprovalDataSource.paginator = this.paginator2;
    this.cteDataSource.paginator = this.paginator3;
    this.courseEquivalenceDataSource.paginator = this.paginator4;

    this.studentDataSource.paginator = this.paginatorS;
    this.studentDataSource.sort = this.sorterS;

    this.dataSource.sort = this.sorter1;
    this.preapprovalDataSource.sort = this.sorter2;
    this.cteDataSource.sort = this.sorter3;
    this.courseEquivalenceDataSource.sort = this.sorter4;
  }

  _setDataSource(indexNumber) {
    setTimeout(() => {
      switch (indexNumber) {
        case 0:
          !this.dataSource.paginator
            ? (this.dataSource.paginator = this.paginator)
            : null;
          break;
        case 1:
          !this.preapprovalDataSource.paginator
            ? (this.preapprovalDataSource.paginator = this.paginator2)
            : null;
          break;
        case 2:
          !this.cteDataSource.paginator
            ? (this.cteDataSource.paginator = this.paginator3)
            : null;
          break;
        case 3:
          !this.courseEquivalenceDataSource.paginator
            ? (this.courseEquivalenceDataSource.paginator = this.paginator4)
            : null;
          break;
      }
    });
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;
    this.preapprovalDataSource.filter = filterValue;
    this.cteDataSource.filter = filterValue;
    this.courseEquivalenceDataSource.filter = filterValue;
    this.studentDataSource.filter = filterValue;
  }

  openDialog(row) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;
    let student: Student = {
      firstName: 'Atak Talay',
      lastName: 'Yücel',
      preferredSemester: { academicYear: '2022-2023', semester: 'Spring' },
      exchangeSchool: 'EPFL',
      preferredSchools: null,
      minors: null,
      major: {
        departmentName: DepartmentsEnum.CS[0],
        facultyName: FacultiesEnum.Engineering
      },
      cgpa: 3.88,
      cteForms: null,
      preApprovalForms: null,
      equivalenceRequestForms: null,
      id: null,
      identityUser: {
        email: 'talay.yucel@ug.bilkent.edu.tr',
        userName: '21901636'
      },
      actorType: null,
      entranceYear: 2019,
      exchangeScore: 100
    };
    let preApprovalForm: PreApprovalForm = {
      id: null,
      firstName: student.firstName,
      lastName: student.lastName,
      idNumber: student.identityUser.userName,
      department: student.major.departmentName,
      isApproved: false,
      isRejected: false,
      isCanceled: false,
      isArchived: false,
      approvalTime: null,
      submissionTime: new Date(),
      hostUniversityName: student.exchangeSchool,
      semester: student.preferredSemester.semester,
      academicYear: student.preferredSemester.academicYear,
      exchangeCoordinatorApproval: {
        id: null,
        name: 'Borga Haktan Bilen',
        dateOfApproval: null,
        isApproved: false
      },
      facultyAdministrationBoardApproval: {
        id: null,
        name: 'Kutay Tire',
        dateOfApproval: null,
        isApproved: false
      },
      requestedCourseGroups: [
        {
          id: null,
          requestedExemptedCourse: {
            id: null,
            courseCode: 'MATH 313',
            courseName: 'Real Analysis 1',
            bilkentCredits: 3,
            ects: 5,
            courseType: 'Mandatory Course'
          },
          requestedCourses: [
            {
              id: null,
              courseCode: 'MATH 354',
              courseName: 'Real Analysis',
              ects: 6.5
            }
          ]
        }
      ]
    };
    let viewPreApprovalForm: ViewPreApprovalForm = {
      student: student,
      preApprovalForm: preApprovalForm
    };
    let cteForm: CteForm = {
      id: new GUID(),
      firstName: student.firstName,
      lastName: student.lastName,
      department: student.major.departmentName,
      idNumber: student.identityUser.userName,
      hostUniversityName: student.exchangeSchool,
      chairApproval: {
        id: null,
        name: 'Borga Haktan Bilen',
        dateOfApproval: null,
        isApproved: false
      },
      deanApproval: {
        id: null,
        name: 'Kutay Tire',
        dateOfApproval: null,
        isApproved: false
      },
      exchangeCoordinatorApproval: {
        id: null,
        name: 'Yiğit Yalın',
        dateOfApproval: new Date(),
        isApproved: true
      },
      approvalTime: null,
      transferredCourseGroup: [
        {
          id: null,
          transferredCourses: [
            {
              id: null,
              courseCode: 'MATH 354',
              courseName: 'Real Analysis',
              grade: 'A',
              ects: 6.5
            }
          ],
          exemptedCourse: {
            id: null,
            courseCode: 'MATH 313',
            courseName: 'Real Analysis 1',
            bilkentCredits: 3,
            ects: 5,
            courseType: 'Mandatory Course'
          }
        },
        {
          id: null,
          transferredCourses: [
            {
              id: null,
              courseCode: 'MATH 354',
              courseName: 'Real Analysis',
              grade: 'A',
              ects: 6.5
            }
          ],
          exemptedCourse: {
            id: null,
            courseCode: 'MATH 313',
            courseName: 'Real Analysis 1',
            bilkentCredits: 3,
            ects: 5,
            courseType: 'Mandatory Course'
          }
        }
      ],
      submissionTime: new Date(),
      facultyOfAdministrationBoardApproval: {
        id: null,
        name: 'Berk Çakar',
        dateOfApproval: new Date(),
        isApproved: false
      },
      isRejected: false,
      isApproved: false,
      isCanceled: false,
      isArchived: false
    };
    let viewCTEForm: ViewCTEForm = { student: student, cteForm: cteForm };
    let eqReq: EquivalenceRequest = {
      id: null,
      studentId: student.identityUser.userName,
      additionalNotes: '',
      hostCourseCode: 'MATH 354',
      hostCourseEcts: 4.5,
      isApproved: false,
      isRejected: false,
      isArchived: false,
      isCanceled: false,
      hostCourseName: 'Real Analysis',
      fileName: 'Syllabus',
      exemptedCourse: {
        id: null,
        courseCode: 'MATH 313',
        courseName: 'Real Analysis 1',
        bilkentCredits: 3,
        ects: 5,
        courseType: 'Mandatory Course'
      },
      instructorApproval: {
        id: null,
        name: 'Borga Haktan Bilen',
        dateOfApproval: null,
        isApproved: false
      }
    };
    let viewEqReq: ViewEquivalenceRequest = { student: student, eqReq: eqReq };

    if (row.type == 'PreApproval Form') {
      dialogConfig.data = viewPreApprovalForm;
      const dialogRef = this.dialog.open(
        ViewPreapprovalFormDialogComponent,
        dialogConfig
      );
    } else if (row.type == 'CTE Form') {
      dialogConfig.data = viewCTEForm;
      const dialogRef = this.dialog.open(
        ViewCteFormDialogComponent,
        dialogConfig
      );
    } else if (row.type == 'Course Eq. Request') {
      dialogConfig.data = viewEqReq;
      const dialogRef = this.dialog.open(
        ViewEquivalenceRequestDialogComponent,
        dialogConfig
      );
    }

    /*
    this.activatedRow = row;
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = createRandomDialogData(this.activatedRow);

    const dialogRef = this.dialog.open(FormDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        let message = result ? 'Form is successfully signed.' : 'Form is rejected.';
        this.openSnackBar(message, 'Close', 5);
      }
    });
     */
  }

  openSnackBar(message: string, action: string, duration: number) {
    this._snackBar.open(message, action, {
      duration: duration * 1000
    });
  }

  openCreatePreapprovalFormDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;
    this.preApprovalForm = {
      id: null,
      firstName: '',
      lastName: '',
      idNumber: '',
      department: '',
      hostUniversityName: '',
      academicYear: '',
      semester: '',
      submissionTime: null,
      approvalTime: null,
      requestedCourseGroups: null,
      exchangeCoordinatorApproval: null,
      facultyAdministrationBoardApproval: null,
      isApproved: false,
      isArchived: false,
      isCanceled: false,
      isRejected: false
    };
    dialogConfig.data = this.preApprovalForm;

    const dialogRef = this.dialog.open(
      PreapprovalFormDialogComponent,
      dialogConfig
    );
  }

  openCreateEquivalanceRequestDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;
    this.equivalanceRequest = {
      id: null,
      studentId: null,
      fileName: null,
      exemptedCourse: {
        id: null,
        courseName: '',
        courseCode: '',
        courseType: null,
        ects: null,
        bilkentCredits: null
      },
      instructorApproval: null,
      additionalNotes: null,
      hostCourseName: '',
      hostCourseCode: null,
      hostCourseEcts: null,
      isApproved: false,
      isArchived: false,
      isCanceled: false,
      isRejected: false
    };
    dialogConfig.data = this.equivalanceRequest;

    const dialogRef = this.dialog.open(
      EquivalenceRequestDialogComponent,
      dialogConfig
    );
  }

  openCreateCTEFormDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;
    this.cteForm = {
      id: null,
      firstName: '',
      lastName: '',
      idNumber: '',
      department: '',
      hostUniversityName: '',
      submissionTime: null,
      approvalTime: null,
      transferredCourseGroup: null,
      exchangeCoordinatorApproval: null,
      facultyOfAdministrationBoardApproval: null,
      deanApproval: null,
      chairApproval: null,
      isApproved: false,
      isArchived: false,
      isCanceled: false,
      isRejected: false
    };
    dialogConfig.data = this.cteForm;

    const dialogRef = this.dialog.open(CteFormDialogComponent, dialogConfig);
  }
}
