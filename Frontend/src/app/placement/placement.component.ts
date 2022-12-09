import {Component, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import {MatSort, Sort} from '@angular/material/sort';


@Component({
  selector: 'app-placement',
  styleUrls: ['./placement.component.css'],
  templateUrl: './placement.component.html',
})
export class PlacementComponent{
  displayedColumns = ['studentname', 'email', 'preferences', 'score'];
  dataSource: MatTableDataSource<UserData>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor() {
    // Create 100 users
    const users: UserData[] = [];
    for (let i = 1; i <= 100; i++) { users.push(createNewUser(i)); }

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
}

/** Builds and returns a new User. */
function createNewUser(id: number): UserData {
  const name =
      NAMES[Math.round(Math.random() * (NAMES.length - 1))] + ' ' +
      NAMES[Math.round(Math.random() * (NAMES.length - 1))].charAt(0) + '.';

  return {
    name: name,
    email: name + "@ug.bilkent.edu.tr",
    preferences: PREFERENCES[Math.round(Math.random() * (PREFERENCES.length - 1))] + ", " + PREFERENCES[Math.round(Math.random() * (PREFERENCES.length - 1))],
    score: Math.round(Math.random() * 100)
  };
}

/** Constants used to fill up our data base. */
const NAMES = ['Maia', 'Asher', 'Olivia', 'Atticus', 'Amelia', 'Jack',
  'Charlotte', 'Theodore', 'Isla', 'Oliver', 'Isabella', 'Jasper',
  'Cora', 'Levi', 'Violet', 'Arthur', 'Mia', 'Thomas', 'Elizabeth'];

const PREFERENCES = ['EPFL' , 'Saarland', 'AGH', 'Vrije', 'Roskilde', 'TU Dortmund', 'TU Berlin']

export interface UserData {
  email: string;
  name: string;
  preferences: string;
  score: number;
}
