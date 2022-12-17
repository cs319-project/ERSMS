import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatSort, Sort } from '@angular/material/sort';
import { SelectionModel } from '@angular/cdk/collections';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { FormDialogComponent } from '../formsandrequests/form-dialog/form-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormsAndRequestsComponent } from '../formsandrequests/formsandrequests.component';
import { EquivalenceRequestService } from '../_services/equivalencerequest.service';
import { CTEFormService } from '../_services/cteform.service';
import { UserService } from '../_services/user.service';
import { PreApprovalFormService } from '../_services/preapprovalform.service';
import { PreApprovalForm } from '../_models/pre-approval-form';
import { EquivalenceRequest } from '../_models/equivalence-request';
import { CteForm } from '../_models/cte-form';
import { Student } from '../_models/student';
import { ViewCTEForm } from '../formsandrequests/view-cte-form-dialog/viewCTEForm';
import { ViewCteFormDialogComponent } from '../formsandrequests/view-cte-form-dialog/view-cte-form-dialog.component';
import { ViewPreApprovalForm } from '../formsandrequests/view-preapproval-form-dialog/viewPreApprovalForm';
import { ViewPreapprovalFormDialogComponent } from '../formsandrequests/view-preapproval-form-dialog/view-preapproval-form-dialog.component';
import { ViewEquivalenceRequest } from '../formsandrequests/view-equivalence-request-dialog/viewEquivalenceRequest';
import { ViewEquivalenceRequestDialogComponent } from '../formsandrequests/view-equivalence-request-dialog/view-equivalence-request-dialog.component';
import { ActorsEnum } from '../_models/enum/actors-enum';

@Component({
  selector: 'app-logging',
  templateUrl: './logging.component.html',
  styleUrls: ['./logging.component.css']
})
export class LoggingComponent {
  displayedColumns = ['id', 'student', 'date', 'type', 'school', 'status'];
  displayedColumns2 = ['id', 'student', 'date', 'school', 'status'];
  dataSource: MatTableDataSource<UserData>;
  preapprovalDataSource: MatTableDataSource<UserData>;
  cteDataSource: MatTableDataSource<UserData>;
  courseEquivalenceDataSource: MatTableDataSource<UserData>;

  @ViewChild('paginator') paginator: MatPaginator;
  @ViewChild('paginator2') paginator2: MatPaginator;
  @ViewChild('paginator3') paginator3: MatPaginator;
  @ViewChild('paginator4') paginator4: MatPaginator;

  @ViewChild('sorter1') sorter1: MatSort;
  @ViewChild('sorter2') sorter2: MatSort;
  @ViewChild('sorter3') sorter3: MatSort;
  @ViewChild('sorter4') sorter4: MatSort;

  @ViewChild(MatTable) AllFormsTable!: MatTable<UserData>;

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
    this.currentUserId = JSON.parse(localStorage.getItem('user')).userName;

    // for (let i = 1; i <= 100; i++) {
    //   users.push(createNewUser(i, (status = 'Processing')));
    // }
    this.dataSource = new MatTableDataSource<UserData>();
    this.cteDataSource = new MatTableDataSource<UserData>();
    this.preapprovalDataSource = new MatTableDataSource<UserData>();
    this.courseEquivalenceDataSource = new MatTableDataSource<UserData>();

    cteFormService
      .getArchivedCTEFormsByDepartment(this.currentUserId)
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
      .getArchivedEquivalenceRequestsByDepartment(this.currentUserId)
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
          this.courseEquivalenceDataSource.data.push(element);
          this.dataSource.data.push(temp);
        });
        this.courseEquivalenceDataSource.paginator = this.paginator4;
        this.dataSource.sort = this.sorter1;
        this.dataSource.paginator = this.paginator;
        this.courseEquivalenceDataSource.sort = this.sorter4;
        this.AllFormsTable.renderRows();
      });
  }

  /**
   * Set the paginator and sort after the view init since this component will
   * be able to query its view for the initialized paginator and sort.
   */
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.preapprovalDataSource.paginator = this.paginator2;
    this.cteDataSource.paginator = this.paginator3;
    this.courseEquivalenceDataSource.paginator = this.paginator4;

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
}

export function createRandomDialogData(row) {
  return {
    studentName: row.student,
    studentEmail: row.student + '@ug.bilkent.edu.tr',
    studentId: Math.round(Math.random() * 100000000),
    studentCgpa: Math.random() * 4,
    studentEntranceYear: '2020',
    studentDepartment: 'CS',
    exchangeProgram: 'ERASMUS',
    exchangeSchool: row.school,
    exchangeTerm: '2022-2023 Spring',
    formId: Math.round(Math.random() * 10000000),
    formType: row.type,
    formStatus: row.status,
    formAssignedPrivilegedUser: 'Can Alkan',
    formAssignedPrivilegedUserRole: 'Exchange Coordinator',
    formDate: null,
    formSignature: null
  };
}

/** Builds and returns a new User. */
export function createNewUser(
  id: number,
  status: string = null,
  student: string = null
): UserData {
  const name =
    NAMES[Math.round(Math.random() * (NAMES.length - 1))] +
    ' ' +
    NAMES[Math.round(Math.random() * (NAMES.length - 1))].charAt(0) +
    '.';

  return {
    id: id,
    student: student || name,
    date: new Date('12/05/2022').toLocaleDateString('en-US'),
    type: TYPE[Math.round(Math.random() * (TYPE.length - 1))],
    school: SCHOOLS[Math.round(Math.random() * (SCHOOLS.length - 1))],
    status: status || STATUS[Math.round(Math.random() * (STATUS.length - 1))]
  };
}

/** Constants used to fill up our data base. */
export const NAMES = [
  'Maia',
  'Asher',
  'Olivia',
  'Atticus',
  'Amelia',
  'Jack',
  'Charlotte',
  'Theodore',
  'Isla',
  'Oliver',
  'Isabella',
  'Jasper',
  'Cora',
  'Levi',
  'Violet',
  'Arthur',
  'Mia',
  'Thomas',
  'Elizabeth'
];

export const SCHOOLS = [
  'EPFL',
  'Saarland',
  'AGH',
  'Vrije',
  'Roskilde',
  'TU Dortmund',
  'TU Berlin',
  'ETH'
];
export const TYPE = ['CTE Form', 'PreApproval Form', 'Course Eq. Request'];
export const STATUS = ['Rejected', 'Approved'];

export interface UserData {
  id: number;
  student: string;
  date?: string;
  type: string;
  school: string;
  status: string;
  formId?: number;
}
