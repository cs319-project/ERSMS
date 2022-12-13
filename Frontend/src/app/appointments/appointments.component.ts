import { Component, OnInit } from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {MatSnackBar} from "@angular/material/snack-bar";
import {
  AppointmentDialogData,
  CreateAppointmentDialogComponent
} from "./create-appointment-dialog/create-appointment-dialog.component";

@Component({
  selector: 'app-appointments',
  templateUrl: './appointments.component.html',
  styleUrls: ['./appointments.component.css']
})

export class AppointmentsComponent implements OnInit {

  appointment: AppointmentDialogData = {topic: "", person:"", time:{hour:"",minute:""}, date:""};

  appointmentsList: dayAppointments[] = [{
    date: new Date("12/05/2022").toLocaleDateString('tr-TR'),
    appointments: [{topic: "Kutay vs CSS", person: "Kutay Tire", time:"13:33"}, {topic: "Kutay vs CSS", person: "Kutay Tire", time:"13:33"},
      {topic: "Kutay vs CSS", person: "Kutay Tire", time:"13:33"},{topic: "Kutay vs CSS", person: "Kutay Tire", time:"13:33"},]},
    {
      date: new Date('09/05/2022').toLocaleDateString('tr-TR'),
      appointments: [{topic: "Kutay vs Flex", person: "Kutay Tire", time:"13:33"}, {topic: "Kutay vs Flex", person: "Kutay Tire", time:"13:33"},
        {topic: "Kutay vs Flex", person: "Kutay Tire", time:"13:33"},{topic: "Kutay vs Flex", person: "Kutay Tire", time:"13:33"},
      ]
    }];



  constructor(private dialog: MatDialog,private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
  }
  openDialog() {
    const dialogRef = this.dialog.open(CreateAppointmentDialogComponent, {data: this.appointment});

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.openSnackBar("Announcement sent", 'Close', 5);
        console.log(result);
      }
    });

  }

  openSnackBar(message: string, action: string, duration: number) {
    this._snackBar.open(message, action, {
      duration: duration * 1000
    });
  }

}


export interface appointment{
  topic: string;
  person: string;
  time: string;
};

export interface dayAppointments{
  date: string;
  appointments: appointment[];
};
