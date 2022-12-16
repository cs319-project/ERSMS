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
  constructor(private dialog: MatDialog) {
    this.dataSource = new MatTableDataSource(this.users);
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }
  ngOnInit(): void {}

  onCreteUser() {
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
}

export interface AppUser {
  id: GUID;
  bilkentId: string;
  name: string;
  type: string;
}
