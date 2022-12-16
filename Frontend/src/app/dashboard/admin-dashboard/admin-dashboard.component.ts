import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { UserData } from '../../placement/placement.component';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { SelectionModel } from '@angular/cdk/collections';
import { DomainUser } from '../../_models/domain-user';
import { UserService } from '../../_services/user.service';

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
  displayedColumns = ['firstName', 'lastName', 'role'];
  selection = new SelectionModel<UserData>(true, []);

  constructor(private userService: UserService) {
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
}
