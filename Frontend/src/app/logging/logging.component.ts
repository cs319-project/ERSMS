import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
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
  displayedColumns2 = ['id', 'student', 'date','school' ,'status'];
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
 
  constructor() {
    // Create 100 users
    const users: UserData[] = [];
    const preapprovalUsers: UserData[] = [];
    const cteUsers: UserData[] = [];
    const courseequivalenceUsers: UserData[] = [];

    for (let i = 1; i <= 100; i++) { users.push(createNewUser(i))};
    
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

  /**
   * Set the paginator and sort after the view init since this component will
   * be able to query its view for the initialized paginator and sort.
   */
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