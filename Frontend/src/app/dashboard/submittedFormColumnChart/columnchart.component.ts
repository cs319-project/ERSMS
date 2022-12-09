
import { Component, OnInit } from '@angular/core';
import { single } from './data';

@Component({
  selector: 'app-columnchart',
  templateUrl: './columnchart.component.html',
  styleUrls: ['./columnchart.component.css']
})
export class ColumnChartComponent{

    single: any[];
  multi: any[];

  view: any[] = [500, 200];

  // options
  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = false;
  showXAxisLabel = true;
  xAxisLabel = 'Days';
  showYAxisLabel = true;
  yAxisLabel = 'Forms';


  colorScheme = {
    domain: ['#5AA454', '#C7B42C']
  };

  constructor() {
    Object.assign(this, { single })
  }

  onSelect(event) {
    console.log(event);
  }


}
