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
import { EquivalenceRequestService } from '../_services/equivalencerequest.service';
import { CTEFormService } from '../_services/cteform.service';
import { UserService } from '../_services/user.service';
import { identity } from 'rxjs';
import { PreApprovalFormService } from '../_services/preapprovalform.service';

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
  currentUserId: string;

  preApprovalForm: PreApprovalForm;
  equivalanceRequest: EquivalenceRequest;
  cteForm: CteForm;
  cteForms: CteForm[] = [];
  preApprovalForms: PreApprovalForm[] = [];
  equivalenceRequests: EquivalenceRequest[] = [];

  constructor(
    private dialog: MatDialog,
    private _snackBar: MatSnackBar,
    private equivalenceRequestService: EquivalenceRequestService,
    private cteFormService: CTEFormService,
    private userService: UserService,
    private preApprovalFormService: PreApprovalFormService
  ) {
    const users: UserData[] = [];
    const preapprovalUsers: UserData[] = [];
    const cteUsers: UserData[] = [];
    const courseequivalenceUsers: UserData[] = [];
    const studentUser: UserData[] = [];
    this.currentUserId = JSON.parse(localStorage.getItem('user')).userName;

    // for (let i = 1; i <= 100; i++) {
    //   users.push(createNewUser(i, (status = 'Processing')));
    // }

    cteFormService
      .getNonArchivedCTEFormsByDepartment(this.currentUserId)
      .toPromise()
      .then(data => {
        data.forEach(element => {
          let temp: UserData = {
            formId: element.id,
            id: element.idNumber,
            student: element.firstName + ' ' + element.lastName,
            date: element.submissionTime.toString(),
            type: 'CTE Form',
            school: element.hostUniversityName,
            status: element.isRejected
              ? 'Rejected'
              : element.isApproved
              ? 'Approved'
              : 'Processing'
          };
          users.push(temp);
          cteUsers.push(temp);
          studentUser.push(temp);
          this.cteForms.push(element);
        });
      });

    preApprovalFormService
      .getNonArchivedPreApprovalFormsByDepartment(this.currentUserId)
      .toPromise()
      .then(data => {
        data.forEach(element => {
          let temp: UserData = {
            formId: element.id,
            id: element.idNumber,
            student: element.firstName + ' ' + element.lastName,
            date: element.submissionTime.toString(),
            type: 'PreApproval Form',
            school: element.hostUniversityName,
            status: element.isRejected
              ? 'Rejected'
              : element.isApproved
              ? 'Approved'
              : 'Processing'
          };
          users.push(temp);
          preapprovalUsers.push(temp);
          studentUser.push(temp);
          this.preApprovalForms.push(element);
        });
      });

    equivalenceRequestService
      .getNonArchivedEquivalenceRequestsByDepartment(this.currentUserId)
      .toPromise()
      .then(data => {
        // console.log(data);
        data.forEach(element => {
          let temp: UserData = {
            formId: element.id,
            id: element.studentId,
            student: element.firstName + ' ' + element.lastName,
            // date: element.submissionTime.toString(),
            type: 'Course Eq. Request',
            school: element.hostUniversityName,
            status: element.isRejected
              ? 'Rejected'
              : element.isApproved
              ? 'Approved'
              : 'Processing'
          };
          users.push(temp);
          courseequivalenceUsers.push(temp);
          studentUser.push(temp);
          this.equivalenceRequests.push(element);
        });
      });

    //console.log(this.preApprovalForms);
    //console.log(preapprovalUsers);

    // console.log(this.cteForms);
    // console.log(users);
    // for (let i = 1; i <= 10; i++) {
    //   studentUser.push(createNewUser(i));
    // }

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
      id: null,
      firstName: '',
      lastName: '',
      idNumber: '',
      department: '',
      hostUniversityName: '',
      submissionTime: null,
      approvalTime: null,
      transferredCourseGroups: null,
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
