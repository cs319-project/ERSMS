import { Component, ViewChild, OnInit, AfterViewInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatPaginator } from '@angular/material/paginator';
import { MatTable, MatTableDataSource } from '@angular/material/table';
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
import { EquivalenceRequestService } from '../_services/equivalencerequest.service';
import { CTEFormService } from '../_services/cteform.service';
import { UserService } from '../_services/user.service';
import { PreApprovalFormService } from '../_services/preapprovalform.service';
import { formatDate } from '@angular/common';
import { ConfirmationDialogComponent } from '../appointments/confirmation-dialog/confirmation-dialog.component';
import { ToastrService } from 'ngx-toastr';
import { ActorsEnum } from '../_models/enum/actors-enum';

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

  @ViewChild(MatTable) AllFormsTable!: MatTable<UserData>;
  @ViewChild(MatTable) StudentTable!: MatTable<UserData>;
  @ViewChild(MatTable) PreApprovalTable!: MatTable<UserData>;
  @ViewChild(MatTable) CTETable!: MatTable<UserData>;
  @ViewChild(MatTable) CourseEqTable!: MatTable<UserData>;

  activatedRow = null;
  currentUserId: string;
  currentUserRole: string;
  isDean: boolean;

  preApprovalForm: PreApprovalForm;
  equivalanceRequest: EquivalenceRequest;
  cteForm: CteForm;
  cteForms: CteForm[] = [];
  preApprovalForms: PreApprovalForm[] = [];
  equivalenceRequests: EquivalenceRequest[] = [];

  format = 'dd/MM/yyyy h:mm';
  locale = 'en-TR';
  constructor(
    private toastr: ToastrService,
    private dialog: MatDialog,
    private _snackBar: MatSnackBar,
    private equivalenceRequestService: EquivalenceRequestService,
    private cteFormService: CTEFormService,
    private userService: UserService,
    private preApprovalFormService: PreApprovalFormService
  ) {
    this.currentUserId = JSON.parse(localStorage.getItem('user')).userName;
    this.currentUserRole = JSON.parse(localStorage.getItem('user')).roles[0];
    this.isDean = JSON.parse(localStorage.getItem('user')).userDetails.isDean;

    // for (let i = 1; i <= 100; i++) {
    //   users.push(createNewUser(i, (status = 'Processing')));
    // }
    this.dataSource = new MatTableDataSource<UserData>();
    this.cteDataSource = new MatTableDataSource<UserData>();
    this.preapprovalDataSource = new MatTableDataSource<UserData>();
    this.courseEquivalenceDataSource = new MatTableDataSource<UserData>();
    this.studentDataSource = new MatTableDataSource<UserData>();

    if (this.currentUserRole === ActorsEnum.CourseCoordinatorInstructor) {
      const courseCode: string = JSON.parse(localStorage.getItem('user'))
        .userDetails.course.courseCode;

      equivalenceRequestService
        .getNonArchivedEquivalenceRequestsByCourseCode(courseCode)
        .toPromise()
        .then(data => {
          data.forEach(element => {
            const formattedDate = formatDate(
              element.submissionDate.toString(),
              this.format,
              this.locale
            );
            let temp: UserData = {
              formId: element.id,
              id: element.studentId,
              student: element.firstName + ' ' + element.lastName,
              date: formattedDate,
              type: 'Course Eq. Request',
              school: element.hostUniversityName,
              status: element.isRejected
                ? 'Rejected'
                : element.isApproved
                ? 'Approved'
                : 'Waiting'
            };
            this.equivalenceRequests.push(element);
            this.courseEquivalenceDataSource.data.push(temp);
            console.log(this.courseEquivalenceDataSource.data);
          });
          this.courseEquivalenceDataSource.paginator = this.paginator4;
          this.courseEquivalenceDataSource.sort = this.sorter4;
          this.CourseEqTable.renderRows();
        });
    }
    else if (this.currentUserRole === ActorsEnum.DeanDepartmentChair && this.isDean) {
      cteFormService
        .GetNonArchivedCTEFormsByFacultyForDean(this.currentUserId)
        .toPromise()
        .then(data => {
          console.log(data);
          data.forEach(element => {
            const formattedDate = formatDate(
              element.submissionTime.toString(),
              this.format,
              this.locale
            );
            let temp: UserData = {
              formId: element.id,
              id: element.idNumber,
              student: element.firstName + ' ' + element.lastName,
              date: formattedDate,
              type: 'CTE Form',
              school: element.hostUniversityName,
              status: element.isRejected
                ? 'Rejected'
                : element.isApproved
                ? 'Approved'
                : 'Waiting'
            };
            this.cteDataSource.data.push(temp);
            this.cteForms.push(element);
          });
          this.cteDataSource.sort = this.sorter3;
          this.cteDataSource.paginator = this.paginator3;
          this.CTETable.renderRows();
        });
    }

    else if(this.currentUserRole === ActorsEnum.DeanDepartmentChair && !this.isDean){
      cteFormService
        .getNonArchivedCTEFormsByDepartment(this.currentUserId)
        .toPromise()
        .then(data => {
          console.log(data);
          data.forEach(element => {
            const formattedDate = formatDate(
              element.submissionTime.toString(),
              this.format,
              this.locale
            );
            let temp: UserData = {
              formId: element.id,
              id: element.idNumber,
              student: element.firstName + ' ' + element.lastName,
              date: formattedDate,
              type: 'CTE Form',
              school: element.hostUniversityName,
              status: element.isRejected
                ? 'Rejected'
                : element.isApproved
                  ? 'Approved'
                  : 'Waiting'
            };
            this.cteDataSource.data.push(temp);
            this.cteForms.push(element);
          });
          this.cteDataSource.sort = this.sorter3;
          this.cteDataSource.paginator = this.paginator3;
          this.CTETable.renderRows();
        });
    }else if (this.currentUserRole !== ActorsEnum.Student) {
      cteFormService
        .getNonArchivedCTEFormsByDepartment(this.currentUserId)
        .toPromise()
        .then(data => {
          data.forEach(element => {
            const formattedDate = formatDate(
              element.submissionTime.toString(),
              this.format,
              this.locale
            );
            let temp: UserData = {
              formId: element.id,
              id: element.idNumber,
              student: element.firstName + ' ' + element.lastName,
              date: formattedDate,
              type: 'CTE Form',
              school: element.hostUniversityName,
              status: element.isRejected
                ? 'Rejected'
                : element.isApproved
                ? 'Approved'
                : 'Waiting'
            };
            this.dataSource.data.push(temp);
            this.cteDataSource.data.push(temp);
            this.cteForms.push(element);
          });
          this.cteDataSource.sort = this.sorter3;
          this.cteDataSource.paginator = this.paginator3;
          this.dataSource.sort = this.sorter1;
          this.dataSource.paginator = this.paginator;
          this.AllFormsTable.renderRows();
        });

      preApprovalFormService
        .getNonArchivedPreApprovalFormsByDepartment(this.currentUserId)
        .toPromise()
        .then(data => {
          data.forEach(element => {
            const formattedDate = formatDate(
              element.submissionTime.toString(),
              this.format,
              this.locale
            );
            let temp: UserData = {
              formId: element.id,
              id: element.idNumber,
              student: element.firstName + ' ' + element.lastName,
              date: formattedDate,
              type: 'PreApproval Form',
              school: element.hostUniversityName,
              status: element.isRejected
                ? 'Rejected'
                : element.isApproved
                ? 'Approved'
                : 'Waiting'
            };
            this.preApprovalForms.push(element);
            this.dataSource.data.push(temp);
            this.preapprovalDataSource.data.push(temp);
          });
          this.preapprovalDataSource.paginator = this.paginator2;
          this.dataSource.sort = this.sorter1;
          this.dataSource.paginator = this.paginator;
          this.preapprovalDataSource.sort = this.sorter2;
          this.AllFormsTable.renderRows();
        });

      equivalenceRequestService
        .getNonArchivedEquivalenceRequestsByDepartment(this.currentUserId)
        .toPromise()
        .then(data => {
          data.forEach(element => {
            const formattedDate = formatDate(
              element.submissionDate.toString(),
              this.format,
              this.locale
            );
            let temp: UserData = {
              formId: element.id,
              id: element.studentId,
              student: element.firstName + ' ' + element.lastName,
              date: formattedDate,
              type: 'Course Eq. Request',
              school: element.hostUniversityName,
              status: element.isRejected
                ? 'Rejected'
                : element.isApproved
                ? 'Approved'
                : 'Waiting'
            };
            this.equivalenceRequests.push(element);
            this.courseEquivalenceDataSource.data.push(temp);
            console.log(this.courseEquivalenceDataSource.data);
            this.dataSource.data.push(temp);
          });
          this.courseEquivalenceDataSource.paginator = this.paginator4;
          this.dataSource.sort = this.sorter1;
          this.dataSource.paginator = this.paginator;
          this.courseEquivalenceDataSource.sort = this.sorter4;
          this.AllFormsTable.renderRows();
        });
    } else if (this.currentUserRole === ActorsEnum.Student) {
      cteFormService
        .getCTEFormOfStudent(this.currentUserId)
        .toPromise()
        .then(data => {
          data.forEach(element => {
            const formattedDate = formatDate(
              element.submissionTime.toString(),
              this.format,
              this.locale
            );
            let temp: UserData = {
              formId: element.id,
              id: element.idNumber,
              student: element.firstName + ' ' + element.lastName,
              date: formattedDate,
              type: 'CTE Form',
              school: element.hostUniversityName,
              status: element.isRejected
                ? 'Rejected'
                : element.isApproved
                ? 'Approved'
                : 'Waiting'
            };
            this.cteForms.push(element);
            this.studentDataSource.data.push(temp);
          });
          this.studentDataSource.sort = this.sorterS;
          this.studentDataSource.paginator = this.paginatorS;
          this.StudentTable.renderRows();
        });

      preApprovalFormService
        .getPreApprovalFormsOfStudent(this.currentUserId)
        .toPromise()
        .then(data => {
          data.forEach(element => {
            const formattedDate = formatDate(
              element.submissionTime.toString(),
              this.format,
              this.locale
            );
            let temp: UserData = {
              formId: element.id,
              id: element.idNumber,
              student: element.firstName + ' ' + element.lastName,
              date: formattedDate,
              type: 'PreApproval Form',
              school: element.hostUniversityName,
              status: element.isRejected
                ? 'Rejected'
                : element.isApproved
                ? 'Approved'
                : 'Waiting'
            };
            this.preApprovalForms.push(element);
            this.studentDataSource.data.push(temp);
          });
          this.studentDataSource.sort = this.sorterS;
          this.studentDataSource.paginator = this.paginatorS;
          this.StudentTable.renderRows();
        });

      equivalenceRequestService
        .getEquivalenceRequestsOfStudent(this.currentUserId)
        .toPromise()
        .then(data => {
          // console.log(data);
          data.forEach(element => {
            const formattedDate = formatDate(
              element.submissionDate.toString(),
              this.format,
              this.locale
            );
            let temp: UserData = {
              formId: element.id,
              id: element.studentId,
              student: element.firstName + ' ' + element.lastName,
              date: formattedDate,
              type: 'Course Eq. Request',
              school: element.hostUniversityName,
              status: element.isRejected
                ? 'Rejected'
                : element.isApproved
                ? 'Approved'
                : 'Waiting'
            };
            this.equivalenceRequests.push(element);
            this.studentDataSource.data.push(temp);
          });
          this.studentDataSource.sort = this.sorterS;
          this.studentDataSource.paginator = this.paginatorS;
          this.StudentTable.renderRows();
        });
    }
  }

  ngAfterViewInit() {
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
    console.log(row);
    if (row.type == 'CTE Form') {
      this.userService
        .getUserDetails(row.id)
        .toPromise()
        .then((data: Student) => {
          let studentData: Student = data;
          let viewCTEForm: ViewCTEForm = {
            student: studentData,
            cteForm: this.cteForms.find(x => x.id == row.formId)
          };
          const dialogConfig = new MatDialogConfig();
          dialogConfig.disableClose = true;
          dialogConfig.autoFocus = false;
          dialogConfig.data = viewCTEForm;
          this.dialog.open(ViewCteFormDialogComponent, dialogConfig);
        });
    } else if (row.type == 'PreApproval Form') {
      this.userService
        .getUserDetails(row.id)
        .toPromise()
        .then((data: Student) => {
          let studentData: Student = data;
          let viewPreApprovalForm: ViewPreApprovalForm = {
            student: studentData,
            preApprovalForm: this.preApprovalForms.find(x => x.id == row.formId)
          };
          const dialogConfig = new MatDialogConfig();
          dialogConfig.disableClose = true;
          dialogConfig.autoFocus = false;
          dialogConfig.data = viewPreApprovalForm;
          this.dialog.open(ViewPreapprovalFormDialogComponent, dialogConfig);
          console.log(viewPreApprovalForm);
        });
    } else if (row.type == 'Course Eq. Request') {
      this.userService
        .getUserDetails(row.id)
        .toPromise()
        .then((data: Student) => {
          let studentData: Student = data;
          let viewCourseEquivalenceRequest: ViewEquivalenceRequest = {
            student: studentData,
            eqReq: this.equivalenceRequests.find(x => x.id == row.formId)
          };
          const dialogConfig = new MatDialogConfig();
          dialogConfig.disableClose = true;
          dialogConfig.autoFocus = false;
          dialogConfig.data = viewCourseEquivalenceRequest;
          this.dialog.open(ViewEquivalenceRequestDialogComponent, dialogConfig);
        });
    }
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
      firstName: '',
      lastName: '',
      idNumber: '',
      department: '',
      hostUniversityName: '',
      academicYear: '',
      semester: '',
      requestedCourseGroups: null,
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

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        //TODO update table after new form
      }
    });
  }

  openCreateEquivalanceRequestDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;
    this.equivalanceRequest = {
      studentId: null,
      fileName: null,
      exemptedCourse: {
        courseName: '',
        courseCode: '',
        ects: null,
        bilkentCredits: null
      },
      firstName: '',
      lastName: '',
      hostUniversityName: '',
      additionalNotes: null,
      hostCourseName: '',
      hostCourseCode: null,
      hostCourseECTS: null,
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
      firstName: '',
      lastName: '',
      idNumber: '',
      department: '',
      hostUniversityName: '',
      transferredCourseGroups: null,
      isApproved: false,
      isArchived: false,
      isCanceled: false,
      isRejected: false
    };
    dialogConfig.data = this.cteForm;

    const dialogRef = this.dialog.open(CteFormDialogComponent, dialogConfig);
  }

  onCancelButton(e, type, formId) {
    e.stopPropagation();
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = { text: 'Are you sure to cancel this ' + type + '?' };
    dialogConfig.autoFocus = false;
    const dialogRef = this.dialog.open(
      ConfirmationDialogComponent,
      dialogConfig
    );

    dialogRef.afterClosed().subscribe(res => {
      if (res) {
        console.log(formId);
        this.preApprovalFormService.cancelPreApprovalForm(formId).subscribe(
          result => {
            if (result) {
              this.toastr.success('Form is succesfully cancelled');
            } else {
              this.toastr.error('An error occured while canceling');
            }
          },
          error => {
            this.toastr.error('An error occured while canceling');
          }
        );
      }
    });
  }
}
