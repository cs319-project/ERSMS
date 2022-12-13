import { Component, ViewChild} from '@angular/core';
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";
import { FormDialogComponent } from "./form-dialog/form-dialog.component"
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import {MatPaginatorModule} from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { SelectionModel } from '@angular/cdk/collections';


@Component({
    selector: 'app-formsandrequests',
    templateUrl: './formsandrequests.component.html',
    styleUrls: ['./formsandrequests.component.css']
  })

export class FormsAndRequestsComponent {
  constructor(private dialog: MatDialog, private _snackBar: MatSnackBar) {
    const users: UserData[] = [];
  
    for (let i = 1; i <= 100; i++) { users.push(createNewUser(i))};
    this.dataSource = new MatTableDataSource(users);

  }



  displayedColumns = ['id', 'student', 'date', 'type','school' ,'status'];
  selection = new SelectionModel<UserData>(true, []);
  dataSource: MatTableDataSource<UserData>;
  @ViewChild('paginator') paginator: MatPaginator;
  @ViewChild('sorter1') sorter1: MatSort;

  openDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = createRandomDialogData();

    const dialogRef = this.dialog.open(FormDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        let message = result ? 'Form is successfully signed.' : 'Form is rejected.';
        this.openSnackBar(message, 'Close', 5);
      }
    });

  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    
    this.dataSource.sort = this.sorter1;
  }

  openSnackBar(message: string, action: string, duration: number) {
    this._snackBar.open(message, action, {
      duration: duration * 1000
    });
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;
    
  }
}

function createRandomDialogData() {
  const name = NAMES[Math.round(Math.random() * (NAMES.length - 1))]
  const surname = NAMES[Math.round(Math.random() * (NAMES.length - 1))].charAt(0)

  return {
    'studentName': name + ' ' + surname + '.',
    'studentEmail': name.toLowerCase() + '_' + surname.toLowerCase() + '@ug.bilkent.edu.tr',
    'studentId': Math.round(Math.random() * 100000000),
    'studentCgpa': Math.random() * 4,
    'studentEntranceYear': '2020',
    'studentDepartment': 'CS',
    'exchangeProgram': 'ERASMUS',
    'exchangeSchool': SCHOOLS[Math.round(Math.random() * (SCHOOLS.length - 1))],
    'exchangeTerm': '2022-2023 Spring',
    'formId': Math.round(Math.random() * 10000000),
    'formType': 'Pre-Approval Form',
    'formStatus': 'Waiting for Approval',
    'formAssignedPrivilegedUser': 'Can Alkan',
    'formAssignedPrivilegedUserRole': 'Exchange Coordinator',
    'formDate': null,
    'formSignature': null,
  }
}
  

  /**
   * Set the paginator and sort after the view init since this component will
   * be able to query its view for the initialized paginator and sort.
   */


/** Builds and returns a new User. */
function createNewUser(id: number): UserData {
  const name =
      NAMES[Math.round(Math.random() * (NAMES.length - 1))] + ' ' +
      NAMES[Math.round(Math.random() * (NAMES.length - 1))].charAt(0) + '.';

  return {
    id: id,
    student: name,
    date: new Date("12/05/2022").toLocaleDateString('en-US'),
    type: TYPE[Math.round(Math.random() * (TYPE.length - 1))],
    school: SCHOOLS[Math.round(Math.random() * (SCHOOLS.length - 1))],
    status: STATUS[Math.round(Math.random() * (STATUS.length - 1))]
  };
}

/** Constants used to fill up our data base. */
export const NAMES = ['Maia', 'Asher', 'Olivia', 'Atticus', 'Amelia', 'Jack',
  'Charlotte', 'Theodore', 'Isla', 'Oliver', 'Isabella', 'Jasper',
  'Cora', 'Levi', 'Violet', 'Arthur', 'Mia', 'Thomas', 'Elizabeth'];

export const SCHOOLS = ['EPFL' , 'Saarland', 'AGH', 'Vrije', 'Roskilde', 'TU Dortmund', 'TU Berlin', 'ETH']
export const TYPE = ['CTE Form', 'PreApproval Form', 'Course Eq. Request']
export const STATUS = ['Rejected', 'Approved']

export interface UserData {
  id: number;
  student: string,
  date: string;
  type: string;
  school: string;
  status: string;
}
