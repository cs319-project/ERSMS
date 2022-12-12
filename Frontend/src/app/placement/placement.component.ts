import {Component, ViewChild} from '@angular/core';
import {MatPaginator, PageEvent} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import {MatSort} from '@angular/material/sort';
import  {SelectionModel} from "@angular/cdk/collections";


@Component({
  selector: 'app-placement',
  styleUrls: ['./placement.component.css'],
  templateUrl: './placement.component.html',
})
export class PlacementComponent{
  displayedColumns = ['name', 'email', 'preferences', 'score'];
  dataSource: MatTableDataSource<UserData>;
  page_index = 0;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  sample : UserData ={name: "Student Name", department: "Student Department",departmentFull: "Student Department",
    cpga:0.00,id: 22200000, email: "Student Email", preferences: "Student Preferences", score:89,
    prefTerm: "Preferred Term"};
  selection = new SelectionModel<UserData>(false, [this.sample]);
  constructor() {
    // Create 100 users
    const users: UserData[] = [];
    for (let i = 1; i <= 100; i++) { users.push(createNewUser()); }

    // Assign the data to the data source for the table to render
    this.dataSource = new MatTableDataSource(users);
  }

  /**
   * Set the paginator and sort after the view init since this component will
   * be able to query its view for the initialized paginator and sort.
   */
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }

  handlePageEvent(e: PageEvent) {
    this.page_index = e.pageIndex;
  }
}

/** Builds and returns a new User. */
function createNewUser(): UserData {
  const name =
    NAMES[Math.round(Math.random() * (NAMES.length - 1))] + ' ' +
    NAMES[Math.round(Math.random() * (NAMES.length - 1))].charAt(0) + '.';

  const department = DEPARTMENTS[Math.round(Math.random() * (DEPARTMENTS.length - 1))];
  const departmentFull = DEPARTMENTS_FULL[Math.round(Math.random() * (DEPARTMENTS_FULL.length - 1))];
  const id = IDS[Math.round(Math.random() * (IDS.length - 1))];
  return {
    name: name,
    department: department,
    departmentFull: departmentFull,
    id: id,
    cpga: Math.round(Math.random() * 4 * 100) / 100,
    email: name + "@ug.bilkent.edu.tr",
    preferences: PREFERENCES[Math.round(Math.random() * (PREFERENCES.length - 1))] + ", " + PREFERENCES[Math.round(Math.random() * (PREFERENCES.length - 1))],
    score: Math.round(Math.random() * 100 * 100) / 100,
    prefTerm: "2022-2023 Spring"
  };
}

/** Constants used to fill up our data base. */
const NAMES = ['Maia', 'Asher', 'Olivia', 'Atticus', 'Amelia', 'Jack',
  'Charlotte', 'Theodore', 'Isla', 'Oliver', 'Isabella', 'Jasper',
  'Cora', 'Levi', 'Violet', 'Arthur', 'Mia', 'Thomas', 'Elizabeth'];

const DEPARTMENTS = ["CS", "EEE", "IE", "ME"];
const DEPARTMENTS_FULL = [
  "Computer Engineering",
  "Electrical Engineering",
  "Industrial Engineering",
  "Mechanical Engineering"];

const PREFERENCES = ['EPFL' , 'Saarland', 'AGH', 'Vrije', 'Roskilde', 'TU Dortmund', 'TU Berlin']

const IDS = [21902534, 22074268, 21956239, 21877324];

export interface UserData {
  email: string,
  departmentFull: string,
  department: string,
  cpga: number,
  id: number,
  name: string,
  preferences: string,
  score: number,
  prefTerm: string
}
