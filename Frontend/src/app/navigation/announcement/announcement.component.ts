import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-announcement',
  templateUrl: './announcement.component.html',
  styleUrls: ['./announcement.component.css']
})
export class AnnouncementComponent{

  constructor(    public dialogRef: MatDialogRef<AnnouncementComponent>,
                  @Inject(MAT_DIALOG_DATA) public data: DialogData) { }
}
export interface DialogData{
  description: string;
}

