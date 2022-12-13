import {Component, ViewChild, OnInit, AfterViewInit} from '@angular/core';
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import {MatSort, Sort} from '@angular/material/sort';
import { FormDialogComponent } from "./form-dialog/form-dialog.component"
import { SelectionModel } from '@angular/cdk/collections';
import {createNewUser, createRandomDialogData, NAMES, SCHOOLS, UserData} from "../logging/logging.component";


@Component({
    selector: 'app-formsandrequests',
    templateUrl: './formsandrequests.component.html',
    styleUrls: ['./formsandrequests.component.css']
  })

export class FormsAndRequestsComponent {
  /* Student View
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
   */

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

  selection = new SelectionModel<UserData>(true, []);

  activatedRow = null;

  constructor(private dialog: MatDialog, private _snackBar: MatSnackBar) {
    const users: UserData[] = [];
    const preapprovalUsers: UserData[] = [];
    const cteUsers: UserData[] = [];
    const courseequivalenceUsers: UserData[] = [];

    for (let i = 1; i <= 100; i++) {users.push(createNewUser(i, status='Processing'))}

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

  /* Student View functions
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sorter1;
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }
   */
}
