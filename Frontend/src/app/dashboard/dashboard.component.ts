import {Component, ViewChild} from '@angular/core';
import {
  ApexNonAxisChartSeries,
  ApexResponsive,
  ApexChart,
  ChartComponent,
  ApexDataLabels,
  ApexPlotOptions,
  ApexYAxis,
  ApexXAxis,
  ApexAxisChartSeries,
  ApexGrid,
  ApexLegend
} from "ng-apexcharts";

export type PieChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  responsive: ApexResponsive[];
  labels: any;
};

export type BarChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  yaxis: ApexYAxis;
  xaxis: ApexXAxis;
  grid: ApexGrid;
  colors: string[];
  legend: ApexLegend;
};

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css']
  })

export class DashboardComponent{
  @ViewChild("chart") chart: ChartComponent;
  public pieChartOptions: Partial<PieChartOptions>;
  public barChartOptions: Partial<BarChartOptions>;

  editing = true;
  value = "Todo Item";
  add = true;
  constructor(){
    this.pieChartOptions = {
      series: [44, 55, 13],
      chart: {
        type: "donut",
        toolbar: {
          show: true
        }
      },
      labels: ["Processing", "Accepting", "Rejected"],
      responsive: [
        {
          breakpoint: 480,
          options: {
            chart: {
              width: 200
            },
            legend: {
              position: "bottom"
            }
          }
        }
      ]
    };

    this.barChartOptions = {
      series: [
        {
          name: "submission",
          data: [21, 22, 10, 28, 16, 21, 13]
        }
      ],
      chart: {
        height: 350,
        type: "bar",
        events: {
          click: function(chart, w, e) {
            // console.log(chart, w, e)
          }
        }
      },
      colors: [
        "#008FFB",
        "#00E396",
      ],
      plotOptions: {
        bar: {
          columnWidth: "45%",
          distributed: true
        }
      },
      dataLabels: {
        enabled: true
      },
      legend: {
        show: false
      },
      grid: {
        show: true
      },
      xaxis: {
        categories: [
          "Mon",
          "Tue",
          "Wed",
          "Thur",
          "Fri",
          "Sat",
          "Sun"
        ],
        labels: {
          style: {
            colors: [
            ],
            fontSize: "12px"
          }
        }
      }
    };
  }

  toggleEditing() {
    this.editing = !this.editing;
  }

  OnTabChange(index) {
    console.log(index);
  }

  toggleAdd() {
    this.add = !this.add;
  }
}

