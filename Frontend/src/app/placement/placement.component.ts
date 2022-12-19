import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { SelectionModel } from '@angular/cdk/collections';
import { UserService } from '../_services/user.service';
import { PlacementService } from '../_services/placement.service';
import { GUID } from '../../utils/guid';
import { ToastrService } from 'ngx-toastr';
import { MatButton } from '@angular/material/button';
import { PlacementTable } from '../_models/placement-table';
import { DepartmentToFacultyMapper } from 'src/utils/department-to-faculty-mapper';

@Component({
  selector: 'app-placement',
  styleUrls: ['./placement.component.css'],
  templateUrl: './placement.component.html'
})
export class PlacementComponent implements OnInit {
  displayedColumns = ['name', 'preferences', 'score'];
  dataSource: MatTableDataSource<UserData>;
  dataSourceWaiting: MatTableDataSource<UserData>;
  page_index = 0;

  placementTables: PlacementTable[] = [];
  placementTable: PlacementTable;

  currentUserDepartment: string;
  currentUserFaculty: string;

  @ViewChild('paginator') paginator: MatPaginator;
  @ViewChild('sort') sort: MatSort;
  @ViewChild('paginatorWaiting') paginatorWaiting: MatPaginator;
  @ViewChild('sortWaiting') sortWaiting: MatSort;

  @ViewChild('placeStudentsButton') placeStudentsButton: MatButton;
  @ViewChild('downloadTableButton') downloadTableButton: MatButton;

  @ViewChild(MatTable) PlacementTable!: MatTable<UserData>;
  @ViewChild(MatTable) WaitingTable!: MatTable<UserData>;

  users: UserData[] = [];
  users2: UserData[] = [];
  tuplesArr: any[] = [];

  sample: UserData = {
    name: 'Student Name',
    department: 'Student Department',
    departmentFull: 'Student Department',
    cpga: 0.0,
    email: '',
    id: 22200000,
    preferences: 'Student Preferences',
    score: 89,
    prefTerm: 'Preferred Term'
  };
  selection = new SelectionModel<UserData>(false, [this.sample]);
  constructor(
    private userService: UserService,
    private placementService: PlacementService,
    private toastr: ToastrService
  ) {
    this.currentUserDepartment = JSON.parse(
      localStorage.getItem('user')
    ).userDetails.department.departmentName;
    this.currentUserFaculty = JSON.parse(
      localStorage.getItem('user')
    ).userDetails.department.facultyName;

    // this.placementService
    //   .getPlacementTables(this.currentUserDepartment, this.currentUserFaculty)
    //   .subscribe(data => {
    //     // if (data.length === 0) {
    //     //   this.placeStudentsButton.disabled = true;
    //     //   this.downloadTableButton.disabled = true;
    //     // }
    //   });

    this.userService
      .getUserTuples()
      .toPromise()
      .then(tuples => {
        tuples.forEach(element => {
          let temp = {
            email: element.email,
            userName: element.userName
          };

          this.tuplesArr.push(temp);
        });
      });

    this.userService
      .getStudents()
      .toPromise()
      .then(data => {
        data.forEach(element => {
          if (
            element.department.departmentName === this.currentUserDepartment
          ) {
            let temp: UserData = {
              departmentFull: element.department.departmentName,
              department: element.department.departmentName,
              cpga: element.cgpa,
              id: element.userName,
              email: '',
              name: `${element.firstName} ${element.lastName}`,
              preferences: element.preferredSchools.join(', '),
              score: Math.round(element.exchangeScore * 100) / 100,
              prefTerm: `${element.preferredSemester.academicYear} ${element.preferredSemester.semester}`,
              hostUniversity: element.exchangeSchool
            };

            this.tuplesArr.forEach(element2 => {
              if (element2.userName === element.userName) {
                temp.email = element2.email;
              }
            });
            if (element.isPlaced) {
              this.users.push(temp);
            } else {
              this.users2.push(temp);
            }
            // if (this.users.length !== 0) {
            //   this.placeStudentsButton.disabled = true;
            // }
          }
        });

        // Assign the data to the data source for the table to render
        this.dataSource = new MatTableDataSource(this.users);
        this.dataSourceWaiting = new MatTableDataSource(this.users2);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;

        this.dataSourceWaiting.paginator = this.paginatorWaiting;
        this.dataSourceWaiting.sort = this.sortWaiting;
        this.PlacementTable.renderRows();
        this.WaitingTable.renderRows();
      });
  }

  /**
   * Set the paginator and sort after the view init since this component will
   * be able to query its view for the initialized paginator and sort.
   */
  ngAfterViewInit() {
    this.placeStudentsButton.disabled = true;
    this.downloadTableButton.disabled = true;
  }

  // ngOnInit(): void {
  //   this.userService.getUserTuples().subscribe(tuples => {
  //     console.log(tuples);
  //     this.userService
  //       .getStudents()
  //       .toPromise()
  //       .then(data => {
  //         data.forEach(element => {
  //           if (
  //             element.department.departmentName === this.currentUserDepartment
  //           ) {
  //             let temp: UserData = {
  //               departmentFull: element.department.departmentName,
  //               department: element.department.departmentName,
  //               cpga: element.cgpa,
  //               id: element.userName,
  //               name: `${element.firstName} ${element.lastName}`,
  //               preferences: element.preferredSchools.join(', '),
  //               score: Math.round(element.exchangeScore * 100) / 100,
  //               prefTerm: `${element.preferredSemester.academicYear} ${element.preferredSemester.semester}`,
  //               hostUniversity: element.exchangeSchool
  //             };
  //             tuples.forEach(tuple => {
  //               if (tuple.userName === element.userName) {
  //                 temp.email = tuple.email;
  //               }
  //             });
  //             if (element.isPlaced) {
  //               this.users.push(temp);
  //             } else {
  //               this.users2.push(temp);
  //             }
  //             if (this.users.length !== 0) {
  //               this.placeStudentsButton.disabled = true;
  //             }
  //           }
  //         });
  //       });
  //   });

  //   console.log(this.users);
  // }

  ngOnInit() {
    this.updatePlacementTables();
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

  updatePlacementTables() {
    this.placementTables = [];
    this.placementService
      .getPlacementTables(
        this.currentUserDepartment,
        DepartmentToFacultyMapper.map(this.currentUserDepartment).toString()
      )
      .subscribe(res => {
        this.placementTables = res;
      });
  }

  buttonReArrange() {
    this.downloadTableButton.disabled = this.placementTable === undefined;
    this.placeStudentsButton.disabled = this.placementTable === undefined;
  }

  placeStudents() {
    this.toastr.info('Placing students...', '', { timeOut: 0 });
    if (this.placementTable === undefined) {
      this.toastr.error('Please select a placement table');
      return;
    }
    this.placeStudentsButton.disabled = false;
    this.placementService
      .placeStudents(this.placementTable.id)
      .subscribe(res => {
        if (res) {
          this.users = [];
          this.users2 = [];
          this.userService
            .getStudents()
            .toPromise()
            .then(
              data => {
                data.forEach(element => {
                  if (
                    element.department.departmentName ===
                    this.currentUserDepartment
                  ) {
                    let temp: UserData = {
                      departmentFull: element.department.departmentName,
                      department: element.department.departmentName,
                      cpga: element.cgpa,
                      id: element.userName,
                      email: '',
                      name: `${element.firstName} ${element.lastName}`,
                      preferences: element.preferredSchools.join(', '),
                      score: Math.round(element.exchangeScore * 100) / 100,
                      prefTerm: `${element.preferredSemester.academicYear} ${element.preferredSemester.semester}`,
                      hostUniversity: element.exchangeSchool
                    };
                    if (element.isPlaced) {
                      this.users.push(temp);
                    } else {
                      this.users2.push(temp);
                    }
                  }
                });
                // if (this.users.length !== 0) {
                //   this.placeStudentsButton.disabled = true;
                // }

                // Assign the data to the data source for the table to render
                this.dataSource = new MatTableDataSource(this.users);
                this.dataSourceWaiting = new MatTableDataSource(this.users2);
                this.dataSource.paginator = this.paginator;
                this.dataSource.sort = this.sort;

                this.dataSourceWaiting.paginator = this.paginatorWaiting;
                this.dataSourceWaiting.sort = this.sortWaiting;

                this.PlacementTable.renderRows();
                this.WaitingTable.renderRows();
                this.toastr.clear();
                this.toastr.success('Students are successfully placed.');
              },
              err => {
                this.toastr.clear();
                this.toastr.error('Error occured while placing students.');
              }
            );
        }
      });
    // this.placementService
    //   .getPlacementTables(this.currentUserDepartment, this.currentUserFaculty)
    //   .subscribe(
    //     data => {
    //       console.log(data);
    //       if (data.length === 0) {
    //         this.toastr.clear();
    //         this.toastr.error('No tables found.');
    //         this.downloadTableButton.disabled = true;
    //       } else {
    //         this.placementService
    //           .placeStudents(new GUID(data[data.length - 1].id))
    //           .subscribe(
    //             res => {
    //               if (res) {
    //                 this.users = [];
    //                 this.users2 = [];
    //                 this.userService
    //                   .getStudents()
    //                   .toPromise()
    //                   .then(data => {
    //                     data.forEach(element => {
    //                       if (
    //                         element.department.departmentName ===
    //                         this.currentUserDepartment
    //                       ) {
    //                         let temp: UserData = {
    //                           departmentFull: element.department.departmentName,
    //                           department: element.department.departmentName,
    //                           cpga: element.cgpa,
    //                           id: element.userName,
    //                           email: '',
    //                           name: `${element.firstName} ${element.lastName}`,
    //                           preferences: element.preferredSchools.join(', '),
    //                           score:
    //                             Math.round(element.exchangeScore * 100) / 100,
    //                           prefTerm: `${element.preferredSemester.academicYear} ${element.preferredSemester.semester}`,
    //                           hostUniversity: element.exchangeSchool
    //                         };
    //                         if (element.isPlaced) {
    //                           this.users.push(temp);
    //                         } else {
    //                           this.users2.push(temp);
    //                         }
    //                       }
    //                     });
    //                     if (this.users.length !== 0) {
    //                       this.placeStudentsButton.disabled = true;
    //                     }

    //                     // Assign the data to the data source for the table to render
    //                     this.dataSource = new MatTableDataSource(this.users);
    //                     this.dataSourceWaiting = new MatTableDataSource(
    //                       this.users2
    //                     );
    //                     this.dataSource.paginator = this.paginator;
    //                     this.dataSource.sort = this.sort;

    //                     this.dataSourceWaiting.paginator =
    //                       this.paginatorWaiting;
    //                     this.dataSourceWaiting.sort = this.sortWaiting;

    //                     this.PlacementTable.renderRows();
    //                     this.WaitingTable.renderRows();
    //                     this.toastr.clear();
    //                     this.toastr.success(
    //                       'Students are successfully placed.'
    //                     );
    //                   });
    //               }
    //             },
    //             error => {
    //               this.toastr.error('Request related error.');
    //             }
    //           );
    //       }
    //     },
    //     error => {
    //       this.toastr.error('Request related error.');
    //     }
    //   );
  }

  downloadCurrentTable() {
    this.placementService
      .downloadPlacementTable(this.placementTable.id)
      .subscribe(
        res => {
          const blob = new Blob([res], {
            type: this.placementTable.fileName.endsWith('.xlsx')
              ? 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
              : 'application/vnd.ms-excel'
          });
          const url = window.URL.createObjectURL(blob);
          const link = document.createElement('a');
          link.href = url;
          link.download = this.placementTable.fileName;
          link.click();
        },
        err => {
          this.toastr.error('Error when downloading the score table');
        },
        () => {
          this.toastr.success('Score table downloaded successfully');
        }
      ),
      error => {
        this.toastr.error('Request related error.');
      };
  }
}

/** Builds and returns a new User. */
export function createNewUser(
  placed: boolean = false,
  dep: string = null
): UserData {
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
  const pref =
    PREFERENCES[Math.round(Math.random() * (PREFERENCES.length - 1))];
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
  email?: string;
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
