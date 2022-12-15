import { Component, Input, OnInit, OnChanges } from '@angular/core';
import { PlacementTable } from 'src/app/_models/placement-table';
import * as XLSX from 'xlsx';

type AOA = any[][];

@Component({
  selector: 'app-excel-table',
  templateUrl: './excel-table.component.html',
  styleUrls: ['./excel-table.component.css']
})
export class ExcelTableComponent implements OnInit, OnChanges {
  @Input() file: PlacementTable;
  data: AOA = [
    [1, 2],
    [3, 4]
  ];
  wopts: XLSX.WritingOptions = { bookType: 'xlsx', type: 'array' };
  fileName: string = 'SheetJS.xlsx';

  constructor() {}

  ngOnInit(): void {
    this.onFileChange(null);
  }

  ngOnChanges(): void {
    console.log('ngOnChanges');
  }
  onFileChange(evt: any) {
    console.log('onFileChange');
    /* read workbook */
    // const wb: XLSX.WorkBook = XLSX.read(this.excelBlob, { type: 'binary' });

    // /* grab first sheet */
    // const wsname: string = wb.SheetNames[0];
    // const ws: XLSX.WorkSheet = wb.Sheets[wsname];

    // /* save data */
    // this.data = <AOA>XLSX.utils.sheet_to_json(ws, { header: 1 });
    // console.log(this.data);
    console.log(this.file);
  }

  export(): void {
    /* generate worksheet */
    const ws: XLSX.WorkSheet = XLSX.utils.aoa_to_sheet(this.data);

    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

    /* save to file */
    XLSX.writeFile(wb, this.fileName);
  }
}
