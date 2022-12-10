import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import {MatSort, Sort} from '@angular/material/sort';

@Component({
    selector: 'app-logging',
    templateUrl: './logging.component.html',
    styleUrls: ['./logging.component.css']
  })

export class LoggingComponent{

  displayedColumns = ['id', 'student', 'date', 'type','school' ,'status'];
  dataSource: MatTableDataSource<UserData>;
  
  

  selectedTabChange(event:any){
    this.dataSource.paginator = this.paginator;
  }
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
    id: id,
    student: name,
    date: new Date("12/05/2022").toLocaleDateString('en-US'),
    type: TYPE[Math.round(Math.random() * (TYPE.length - 1))], 
    school: SCHOOLS[Math.round(Math.random() * (SCHOOLS.length - 1))], 
    status: STATUS[Math.round(Math.random() * (STATUS.length - 1))]
  };
}

/** Constants used to fill up our data base. */
const NAMES = ['Maia', 'Asher', 'Olivia', 'Atticus', 'Amelia', 'Jack',
  'Charlotte', 'Theodore', 'Isla', 'Oliver', 'Isabella', 'Jasper',
  'Cora', 'Levi', 'Violet', 'Arthur', 'Mia', 'Thomas', 'Elizabeth'];

const SCHOOLS = ['EPFL' , 'Saarland', 'AGH', 'Vrije', 'Roskilde', 'TU Dortmund', 'TU Berlin', 'ETH']
const TYPE = ['CTE Form', 'PreApproval Form', 'Course Eq. Request']
const STATUS = ['Processing', 'Rejected', 'Approved']

export interface UserData {
  id: number;
  student: string,
  date: string;
  type: string;
  school: string;
  status: string;
}
