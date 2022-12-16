import { Component, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-placement',
  styleUrls: ['./placement.component.css'],
  templateUrl: './placement.component.html'
})
export class PlacementComponent {
  displayedColumns = ['name', 'email', 'preferences', 'score'];
  dataSource: MatTableDataSource<UserData>;
  dataSourceWaiting: MatTableDataSource<UserData>;
  page_index = 0;

  @ViewChild('paginator') paginator: MatPaginator;
  @ViewChild('sort') sort: MatSort;
  @ViewChild('paginatorWaiting') paginatorWaiting: MatPaginator;
  @ViewChild('sortWaiting') sortWaiting: MatSort;

  sample: UserData = {
    name: 'Student Name',
    department: 'Student Department',
    departmentFull: 'Student Department',
    cpga: 0.0,
    id: 22200000,
    email: 'Student Email',
    preferences: 'Student Preferences',
    score: 89,
    prefTerm: 'Preferred Term'
  };
  selection = new SelectionModel<UserData>(false, [this.sample]);
  constructor() {
    // Create 100 users
    const users: UserData[] = [];
    for (let i = 1; i <= 100; i++) {
      users.push(createNewUser(true));
    }

    const users2: UserData[] = [];
    for (let i = 1; i <= 100; i++) {
      users2.push(createNewUser());
    }

    // Assign the data to the data source for the table to render
    this.dataSource = new MatTableDataSource(users);
    this.dataSourceWaiting = new MatTableDataSource(users2);
  }

  /**
   * Set the paginator and sort after the view init since this component will
   * be able to query its view for the initialized paginator and sort.
   */
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;

    this.dataSourceWaiting.paginator = this.paginatorWaiting;
    this.dataSourceWaiting.sort = this.sortWaiting;
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;
    this.dataSourceWaiting.filter = filterValue;
  }

  handlePageEvent(e: PageEvent) {
    this.page_index = e.pageIndex;
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
          !this.dataSourceWaiting.paginator
            ? (this.dataSourceWaiting.paginator = this.paginatorWaiting)
            : null;
          break;
      }
    });
  }
}

/** Builds and returns a new User. */
export function createNewUser(placed: boolean = false, dep: string = null): UserData {
  const name =
    NAMES[Math.round(Math.random() * (NAMES.length - 1))] +
    ' ' +
    NAMES[Math.round(Math.random() * (NAMES.length - 1))].charAt(0) +
    '.';

  const department =
    DEPARTMENTS[Math.round(Math.random() * (DEPARTMENTS.length - 1))];
  const departmentFull =
    DEPARTMENTS_FULL[Math.round(Math.random() * (DEPARTMENTS_FULL.length - 1))];
  const id = IDS[Math.round(Math.random() * (IDS.length - 1))];
  const pref = PREFERENCES[Math.round(Math.random() * (PREFERENCES.length - 1))]
  let obj = {
    name: name,
    department: dep || department,
    departmentFull: departmentFull,
    id: id,
    cpga: Math.round(Math.random() * 4 * 100) / 100,
    email: name + '@ug.bilkent.edu.tr',
    preferences:
      pref +
      ', ' +
      PREFERENCES[Math.round(Math.random() * (PREFERENCES.length - 1))],
    score: Math.round(Math.random() * 100 * 100) / 100,
    prefTerm: '2022-2023 Spring'
  };
  if (placed) {
    obj['schoolPlaced'] = pref;
  }
  return obj;
}

/** Constants used to fill up our data base. */
const NAMES = [
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

const DEPARTMENTS = ['CS', 'EEE', 'IE', 'ME'];
const DEPARTMENTS_FULL = [
  'Computer Engineering',
  'Electrical Engineering',
  'Industrial Engineering',
  'Mechanical Engineering'
];

const PREFERENCES = [
  'EPFL',
  'Saarland',
  'AGH',
  'Vrije',
  'Roskilde',
  'TU Dortmund',
  'TU Berlin'
];

const IDS = [21902534, 22074268, 21956239, 21877324];

export interface UserData {
  email: string;
  departmentFull: string;
  department: string;
  cpga: number;
  id: number;
  name: string;
  preferences: string;
  score: number;
  prefTerm: string;
  hostUniversity?: string;
}
