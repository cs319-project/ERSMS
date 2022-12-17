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
import { DomainUser } from 'src/app/_models/domain-user';
import { UserService } from 'src/app/_services/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  dataSource: MatTableDataSource<DomainUser>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  users: DomainUser[];
  activatedRow = null;
  displayedColumns = ['firstName', 'lastName', 'userName', 'role'];
  selection = new SelectionModel<UserData>(true, []);

  constructor(
    private dialog: MatDialog,
    private userService: UserService,
    private toastr: ToastrService
  ) {
    this.userService
      .getUsers()
      .toPromise()
      .then((users: DomainUser[]) => {
        this.users = users;
        this.dataSource = new MatTableDataSource(this.users);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      });
  }

  ngOnInit() {
    this.userService.getUsers().subscribe((users: DomainUser[]) => {
      this.users = users;
      this.dataSource.data = this.users;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  onCreateUser() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = false;
    dialogConfig.autoFocus = false;
    dialogConfig.data = null;
    const dialogRef = this.dialog.open(CreateUserDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe((res: any) => {
      this.userService.getUsers().subscribe((users: DomainUser[]) => {
        this.users = users;
        this.dataSource.data = this.users;
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      });
    });
  }

  openUserDialog(row) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;
    dialogConfig.data = row;
    const dialogRef = this.dialog.open(UserDialogComponent, dialogConfig);
  }

  deleteUser(row) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = {
      text:
        'Are you sure to delete the user ' +
        row.firstName +
        ' ' +
        row.lastName +
        ' from the system?'
    };
    const dialogRef = this.dialog.open(
      ConfirmationDialogComponent,
      dialogConfig
    );
    dialogRef.afterClosed().subscribe(deleteItem => {
      if (deleteItem) {
        this.userService.deleteUser(row.identityUser.userName).subscribe(
          (res: any) => {
            this.dataSource.data = this.dataSource.data.filter((value, key) => {
              return value.identityUser.userName != row.identityUser.userName;
            });
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;

            this.toastr.success('User deleted successfully');
          },
          error => {
            const errorMsg = error.error ? error.error : error;
            this.toastr.error('User update failed: ' + errorMsg);
          }
        );
      }
    });
  }
}

export interface AppUser {
  id: GUID;
  bilkentId: string;
  name: string;
  type: string;
}
