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
import { EquivalanceRequest } from '../_models/equivalance-request';
import { EquivalanceRequestDialogComponent } from './equivalance-request-dialog/equivalance-request-dialog.component';
import { CteForm } from '../_models/cte-form';
import { CteFormDialogComponent } from './cte-form-dialog/cte-form-dialog.component';

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
  equivalanceRequest: EquivalanceRequest;
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
    this.activatedRow = row;
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = createRandomDialogData(this.activatedRow);

    const dialogRef = this.dialog.open(FormDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        let message = result
          ? 'Form is successfully signed.'
          : 'Form is rejected.';
        this.openSnackBar(message, 'Close', 5);
      }
    });
  }

  openSnackBar(message: string, action: string, duration: number) {
    this._snackBar.open(message, action, {
      duration: duration * 1000
    });
  }

  openCreatePreapprovalFormDialog() {
    const dialogConfig = new MatDialogConfig();
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
      isCanceled: false,
      isArchived: false,
      isApproved: false,
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
    this.equivalanceRequest = {
      id: null,
      studentId: null,
      fileName: null,
      exemptedCourse: {
        id: null,
        courseName: '',
        courseCode: '',
        courseType: null,
        bilkentCredits: null,
        ECTS: null
      },
      instructorApproval: null,
      additionalNotes: null,
      hostCourseName: '',
      hostCourseCode: '',
      hostCourseECTS: null,
      isApproved: false,
      isRejected: false,
      isArchived: false,
      isCanceled: false
    };
    dialogConfig.data = this.equivalanceRequest;

    const dialogRef = this.dialog.open(
      EquivalanceRequestDialogComponent,
      dialogConfig
    );
  }

  openCreateCTEFormDialog() {
    const dialogConfig = new MatDialogConfig();
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
      isCanceled: false,
      isArchived: false,
      isApproved: false,
      isRejected: false
    };
    dialogConfig.data = this.cteForm;

    const dialogRef = this.dialog.open(CteFormDialogComponent, dialogConfig);
  }
}
