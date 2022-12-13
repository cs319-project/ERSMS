import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import {MatSort, Sort} from '@angular/material/sort';
import { SelectionModel } from '@angular/cdk/collections';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { FormDialogComponent } from '../formsandrequests/form-dialog/form-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormsAndRequestsComponent } from '../formsandrequests/formsandrequests.component';


@Component({
    selector: 'app-logging',
    templateUrl: './logging.component.html',
    styleUrls: ['./logging.component.css']
  })

export class LoggingComponent{

  displayedColumns = ['id', 'student', 'date', 'type','school' ,'status'];
  displayedColumns2 = ['id', 'student', 'date','school' ,'status'];
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

  selection = new SelectionModel<UserData>(true, []);

  activatedRow = null;

  constructor(private dialog: MatDialog, private _snackBar: MatSnackBar) {
    // Create 100 users
    const users: UserData[] = [];
    const preapprovalUsers: UserData[] = [];
    const cteUsers: UserData[] = [];
    const courseequivalenceUsers: UserData[] = [];

    for (let i = 1; i <= 100; i++) { users.push(createNewUser(i))};

    for(let k = 0; k < users.length; k++) {


      if(users[k].type == 'PreApproval Form') {

        preapprovalUsers.push(users[k]);
      }
      else if(users[k].type == 'CTE Form') {

        cteUsers.push(users[k]);
      }

      else if(users[k].type == 'Course Eq. Request') {

        courseequivalenceUsers.push(users[k]);
      }
    }

    // Assign the data to the data source for the table to render
    this.dataSource = new MatTableDataSource(users);
    this.preapprovalDataSource = new MatTableDataSource(preapprovalUsers);
    this.cteDataSource = new MatTableDataSource(cteUsers);
    this.courseEquivalenceDataSource = new MatTableDataSource(courseequivalenceUsers);

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
          !this.dataSource.paginator ? this.dataSource.paginator = this.paginator : null;
          break;
        case 1:
          !this.preapprovalDataSource.paginator ? this.preapprovalDataSource.paginator = this.paginator2 : null;
          break;
        case 2:
          !this.cteDataSource.paginator ? this.cteDataSource.paginator = this.paginator3 : null;
          break;
        case 3:
          !this.courseEquivalenceDataSource.paginator ? this.courseEquivalenceDataSource.paginator = this.paginator4 : null;
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
  }

  openSnackBar(message: string, action: string, duration: number) {
    this._snackBar.open(message, action, {
      duration: duration * 1000
    });
  }

}

export function createRandomDialogData(row) {
  return {
    'studentName': row.student,
    'studentEmail': row.student + '@ug.bilkent.edu.tr',
    'studentId': Math.round(Math.random() * 100000000),
    'studentCgpa': Math.random() * 4,
    'studentEntranceYear': '2020',
    'studentDepartment': 'CS',
    'exchangeProgram': 'ERASMUS',
    'exchangeSchool': row.school,
    'exchangeTerm': '2022-2023 Spring',
    'formId': Math.round(Math.random() * 10000000),
    'formType': row.type,
    'formStatus': row.status,
    'formAssignedPrivilegedUser': 'Can Alkan',
    'formAssignedPrivilegedUserRole': 'Exchange Coordinator',
    'formDate': null,
    'formSignature': null,
  }
}



/** Builds and returns a new User. */
export function createNewUser(id: number, status: string = null, student: string = null): UserData {
  const name =
      NAMES[Math.round(Math.random() * (NAMES.length - 1))] + ' ' +
      NAMES[Math.round(Math.random() * (NAMES.length - 1))].charAt(0) + '.';

  return {
    id: id,
    student: student || name,
    date: new Date("12/05/2022").toLocaleDateString('en-US'),
    type: TYPE[Math.round(Math.random() * (TYPE.length - 1))],
    school: SCHOOLS[Math.round(Math.random() * (SCHOOLS.length - 1))],
    status: status || STATUS[Math.round(Math.random() * (STATUS.length - 1))]
  };
}

/** Constants used to fill up our data base. */
export const NAMES = ['Maia', 'Asher', 'Olivia', 'Atticus', 'Amelia', 'Jack',
  'Charlotte', 'Theodore', 'Isla', 'Oliver', 'Isabella', 'Jasper',
  'Cora', 'Levi', 'Violet', 'Arthur', 'Mia', 'Thomas', 'Elizabeth'];

export const SCHOOLS = ['EPFL' , 'Saarland', 'AGH', 'Vrije', 'Roskilde', 'TU Dortmund', 'TU Berlin', 'ETH'];
export const TYPE = ['CTE Form', 'PreApproval Form', 'Course Eq. Request'];
export const STATUS = ['Rejected', 'Approved'];

export interface UserData {
  id: number;
  student: string,
  date: string;
  type: string;
  school: string;
  status: string;
}
