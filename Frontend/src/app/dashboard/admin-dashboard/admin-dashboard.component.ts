import {Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {UserData} from "../../placement/placement.component";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import {SelectionModel} from "@angular/cdk/collections";

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {

  dataSource: MatTableDataSource<AppUser>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  displayedColumns = ['name', 'type'];

  selection = new SelectionModel<UserData>(true, []);

  users: AppUser[]= [{name: "Berk Çakar", type:"Student"}, {name:"Atak Talay Yücel", type:"Student"}, {name:"Can Alkan",
  type: "Exchange Coordinator"}]
  activatedRow = null;
  constructor() {
    this.dataSource = new MatTableDataSource(this.users);


  }

  ngAfterViewInit(){
    this.dataSource.sort = this.sort;
  }
  ngOnInit(): void {
  }

}

export interface AppUser{
  name: string;
  type: string;
}
