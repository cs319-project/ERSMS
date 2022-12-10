import { Component} from '@angular/core';
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";
import { FormDialogComponent } from "./form-dialog/form-dialog.component"

@Component({
    selector: 'app-formsandrequests',
    templateUrl: './formsandrequests.component.html',
    styleUrls: ['./formsandrequests.component.css']
  })

export class FormsAndRequestsComponent {
  constructor(private dialog: MatDialog, private _snackBar: MatSnackBar) {}

  openDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;

    const dialogRef = this.dialog.open(FormDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      let message = result ? 'Form is successfully signed.' : 'Form is rejected.';
      this.openSnackBar(message, 'Close', 5);
    });

  }

  openSnackBar(message: string, action: string, duration: number) {
    this._snackBar.open(message, action, {
      duration: duration * 1000
    });
  }
}
