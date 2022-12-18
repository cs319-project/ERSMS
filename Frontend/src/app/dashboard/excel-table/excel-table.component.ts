import { Component, Input, OnInit, OnChanges } from '@angular/core';
import { PlacementTable } from 'src/app/_models/placement-table';
import { PlacementService } from 'src/app/_services/placement.service';
import * as XLSX from 'xlsx';

type AOA = any[][];

@Component({
  selector: 'app-excel-table',
  templateUrl: './excel-table.component.html',
  styleUrls: ['./excel-table.component.css']
})
export class ExcelTableComponent implements OnInit, OnChanges {
  @Input() file: PlacementTable;
  data: AOA = [];
  wopts: XLSX.WritingOptions = { bookType: 'xlsx', type: 'array' };
  fileName: string = 'SheetJS.xlsx';

  constructor(private placementService: PlacementService) {}

  ngOnInit(): void {
    this.onFileChange(null);
  }

  ngOnChanges(): void {
    this.onFileChange(null);
  }
  onFileChange(evt: any) {
    // console.log('onFileChange');
    // /* read workbook */
    // const wb: XLSX.WorkBook = XLSX.read(this.excelBlob, { type: 'binary' });
    // // /* grab first sheet */
    // const wsname: string = wb.SheetNames[0];
    // const ws: XLSX.WorkSheet = wb.Sheets[wsname];
    // // /* save data */
    // this.data = <AOA>XLSX.utils.sheet_to_json(ws, { header: 1 });
    // console.log(this.data);
    this.placementService
      .downloadPlacementTable(this.file.id)
      .toPromise()
      .then(res => {
        //construct blob
        this.blobToArrayBuffer(res).then(res => {
          const wb: XLSX.WorkBook = XLSX.read(res, {
            type: 'buffer'
          });
          const wsname: string = wb.SheetNames[0];
          const ws: XLSX.WorkSheet = wb.Sheets[wsname];
          this.data = <AOA>XLSX.utils.sheet_to_json(ws, { header: 1 });
        });
      });
  }

  async blobToArrayBuffer(blob: Blob) {
    if ('arrayBuffer' in blob) return await blob.arrayBuffer();

    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.onload = () => resolve(reader.result);
      reader.onerror = () => reject();
      reader.readAsArrayBuffer(blob);
    });
  }
}
