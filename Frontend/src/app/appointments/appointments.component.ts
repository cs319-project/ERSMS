import { Component, OnInit } from '@angular/core';
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {MatSnackBar} from "@angular/material/snack-bar";
import {
  AppointmentDialogData,
  CreateAppointmentDialogComponent
} from "./create-appointment-dialog/create-appointment-dialog.component";
import {ConfirmationDialogComponent} from "./confirmation-dialog/confirmation-dialog.component";

@Component({
  selector: 'app-appointments',
  templateUrl: './appointments.component.html',
  styleUrls: ['./appointments.component.css']
})

export class AppointmentsComponent implements OnInit {

  appointment: AppointmentDialogData = {topic: "", person:"", time:{hour:"",minute:""}, date:""};

  appointmentsList: dayAppointments[] = [{
    date: new Date("12/05/2022").toLocaleDateString('tr-TR'),
    appointments: [{topic: "Kutay vs CSS", person: "Kutay Tire", time:"13:33"}, {topic: "Kutay vs HTML", person: "Kutay Tire", time:"13:33"},
      {topic: "Kutay vs CSS", person: "Kutay Tire", time:"13:33"},{topic: "Kutay vs CSS", person: "Kutay Tire", time:"13:33"},]},
    {
      date: new Date('09/05/2022').toLocaleDateString('tr-TR'),
      appointments: [{topic: "Kutay vs C#", person: "Kutay Tire", time:"13:33"}, {topic: "Kutay vs Angular", person: "Kutay Tire", time:"13:33"},
        {topic: "Kutay vs Flex", person: "Kutay Tire", time:"13:33"},{topic: "Kutay vs Flex", person: "Kutay Tire", time:"13:33"},
      ]
    }];

  appointmentsList2: dayAppointments[] = [{
    date: new Date("12/05/2022").toLocaleDateString('tr-TR'),
    appointments: [{topic: "Kutay vs CSS", person: "Kutay Tire", time:"13:33"}, {topic: "Kutay vs CSS", person: "Kutay Tire", time:"13:33"},
      {topic: "Kutay vs CSS", person: "Kutay Tire", time:"13:33"},{topic: "Kutay vs CSS", person: "Kutay Tire", time:"13:33"},]},
    {
      date: new Date('09/05/2022').toLocaleDateString('tr-TR'),
      appointments: [{topic: "Kutay vs Flex", person: "Kutay Tire", time:"13:33"}, {topic: "Kutay vs Flex", person: "Kutay Tire", time:"13:33"},
        {topic: "Kutay vs Flex", person: "Kutay Tire", time:"13:33"},{topic: "Kutay vs Flex", person: "Kutay Tire", time:"13:33"},
      ]
    }]

  appointmentsList3: dayAppointments[] = [{
    date: new Date("12/05/2022").toLocaleDateString('tr-TR'),
    appointments: [{topic: "Kutay vs CSS", person: "Kutay Tire", time:"13:33"}, {topic: "Kutay vs HTML", person: "Kutay Tire", time:"13:33"},
      {topic: "Kutay vs CSS", person: "Kutay Tire", time:"13:33"},{topic: "Kutay vs CSS", person: "Kutay Tire", time:"13:33"},]},
    {
      date: new Date('09/05/2022').toLocaleDateString('tr-TR'),
      appointments: [{topic: "Kutay vs C#", person: "Kutay Tire", time:"13:33"}, {topic: "Kutay vs Angular", person: "Kutay Tire", time:"13:33"},
        {topic: "Kutay vs Flex", person: "Kutay Tire", time:"13:33"},{topic: "Kutay vs Flex", person: "Kutay Tire", time:"13:33"},
      ]
    }];



  constructor(private dialog: MatDialog, private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
  }
  openDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = this.appointment;
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;
    dialogConfig.data = {'text': 'Are you sure to reject this appointment request?'}

    const dialogRef = this.dialog.open(CreateAppointmentDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.openSnackBar("Announcement sent", 'Close', 5);
      }
    });

  }

  deleteAppointment(dayAppointmentsIndex, appointmentIndex) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = {'text': 'Are you sure to delete this appointment?'}
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(deleteItem => {
      if (deleteItem) {
        this.appointmentsList[dayAppointmentsIndex]['appointments'].splice(appointmentIndex, 1);
        if (this.appointmentsList[dayAppointmentsIndex]['appointments'].length == 0) {
          this.appointmentsList.splice(appointmentIndex, 1);
        }
        this.openSnackBar("Appointment deleted", 'Close', 5);
      }
    });
  }

  deleteReceivedAppointmentRequest(dayAppointmentsIndex, appointmentIndex) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = {'text': 'Are you sure to delete this appointment request?'}
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(deleteItem => {
      if (deleteItem) {
        this.appointmentsList2[dayAppointmentsIndex]['appointments'].splice(appointmentIndex, 1);
        if (this.appointmentsList2[dayAppointmentsIndex]['appointments'].length == 0) {
          this.appointmentsList2.splice(appointmentIndex, 1);
        }
        this.openSnackBar("Appointment deleted", 'Close', 5);
      }
    });
  }

  deleteSentAppointmentRequest(dayAppointmentsIndex, appointmentIndex) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = {'text': 'Are you sure to unsend this appointment request?'}
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(deleteItem => {
      if (deleteItem) {
        this.appointmentsList3[dayAppointmentsIndex]['appointments'].splice(appointmentIndex, 1);
        if (this.appointmentsList3[dayAppointmentsIndex]['appointments'].length == 0) {
          this.appointmentsList3.splice(appointmentIndex, 1);
        }
        this.openSnackBar("Appointment deleted", 'Close', 5);
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
