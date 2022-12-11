import { Component} from '@angular/core';
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";
import { FormDialogComponent } from "./form-dialog/form-dialog.component"
import { NAMES, SCHOOLS } from "../logging/logging.component";

@Component({
    selector: 'app-formsandrequests',
    templateUrl: './formsandrequests.component.html',
    styleUrls: ['./formsandrequests.component.css']
  })

export class FormsAndRequestsComponent {
  constructor(private dialog: MatDialog, private _snackBar: MatSnackBar) {}

  openDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = createRandomDialogData();

    const dialogRef = this.dialog.open(FormDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        let message = result ? 'Form is successfully signed.' : 'Form is rejected.';
        this.openSnackBar(message, 'Close', 5);
      }
    });

  }

  openSnackBar(message: string, action: string, duration: number) {
    this._snackBar.open(message, action, {
      duration: duration * 1000
    });
  }
}

function createRandomDialogData() {
  const name = NAMES[Math.round(Math.random() * (NAMES.length - 1))]
  const surname = NAMES[Math.round(Math.random() * (NAMES.length - 1))].charAt(0)

  return {
    'studentName': name + ' ' + surname + '.',
    'studentEmail': name.toLowerCase() + '_' + surname.toLowerCase() + '@ug.bilkent.edu.tr',
    'studentId': Math.round(Math.random() * 100000000),
    'studentCgpa': Math.random() * 4,
    'studentEntranceYear': '2020',
    'studentDepartment': 'CS',
    'exchangeProgram': 'ERASMUS',
    'exchangeSchool': SCHOOLS[Math.round(Math.random() * (SCHOOLS.length - 1))],
    'exchangeTerm': '2022-2023 Spring',
    'formId': Math.round(Math.random() * 10000000),
    'formType': 'Pre-Approval Form',
    'formStatus': 'Waiting for Approval',
    'formAssignedPrivilegedUser': 'Can Alkan',
    'formAssignedPrivilegedUserRole': 'Exchange Coordinator',
    'formDate': null,
    'formSignature': null,
  }
}
