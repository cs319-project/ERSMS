import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { UserData } from '../../placement/placement.component';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { SelectionModel } from '@angular/cdk/collections';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CreateUserDialogComponent } from './create-user-dialog/create-user-dialog.component';
import { UserDialogComponent } from './user-dialog/user-dialog.component';
import { GUID } from '../../../utils/guid';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmationDialogComponent } from '../../appointments/confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  dataSource: MatTableDataSource<AppUser>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  displayedColumns = ['name', 'bilkentId', 'type'];

  selection = new SelectionModel<UserData>(true, []);

  users: AppUser[] = [
    { id: null, bilkentId: '21901636', name: 'Berk Çakar', type: 'Student' }
  ];
  activatedRow = null;
  constructor(private dialog: MatDialog, private _snackBar: MatSnackBar) {
    this.dataSource = new MatTableDataSource(this.users);
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }
  ngOnInit(): void {}

  onCreateUser() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;
    dialogConfig.data = null;
    const dialogRef = this.dialog.open(CreateUserDialogComponent, dialogConfig);
  }

  openUserDialog(row) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;
    dialogConfig.data = null;
    const dialogRef = this.dialog.open(UserDialogComponent, dialogConfig);
  }

  deleteUser(row) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = {
      text: 'Are you sure to delete the user ' + row.name + ' from the system?'
    };
    const dialogRef = this.dialog.open(
      ConfirmationDialogComponent,
      dialogConfig
    );
    dialogRef.afterClosed().subscribe(deleteItem => {
      if (deleteItem) {
        this.openSnackBar('Appointment deleted', 'Close', 5);
        //TODO
      }
    });
  }

  openSnackBar(message: string, action: string, duration: number) {
    this._snackBar.open(message, action, {
      duration: duration * 1000
    });
  }
}

export interface AppUser {
  id: GUID;
  bilkentId: string;
  name: string;
  type: string;
}
