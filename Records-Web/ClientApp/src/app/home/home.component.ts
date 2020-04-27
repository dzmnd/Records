import { Component, OnInit } from '@angular/core';
import { RecordService } from "../Services/record.service";
import { SignalRService } from "../Services/signal-r.service";
import { Table } from '../Models/Table/Table';
import { TableColumn } from '../Models/Table/TableColumn';
import { Record } from '../Models/Record';
import { HttpClient } from '@angular/common/http';
var Tabulator = require('tabulator-tables');

declare var $: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor(private recordService: RecordService, private signalRService: SignalRService, private http: HttpClient) { }

  tabulator = document.createElement('div');
  tabulatorTable: any;
  tableOptions: Table = new Table();

  recordsForUpdate: Record[] = [];

  ngOnInit(): void {
    let _this = this;
    this.tableOptions.columns = <Array<TableColumn>>[
      { title: "Text", field: "text", editor: false },
      { title: "Type", field: "type", editor: false },
      { title: "Status", field: "status", editor: false },
      {
        title: "Index Number",
        field: "indexNumber",
        editor: false,
        formatter: function (cell: any, formatterParams: any) {
          return cell.getData().indexNumber == 0 ? '' : cell.getData().indexNumber;
        }
      },
      {
        title: "",
        field: "Update",
        editor: false,
        align: "center",
        formatter: function (cell: any, formatterParams: any) {
          let positionRow = cell.getRow().getPosition();
          return `<input class="update-record" type="checkbox" data-position-row="${positionRow}" />`;
        }
      },
    ];

    this.tableOptions.paginationSize = 25;
    this.tableOptions.ajaxFiltering = false;
    this.tableOptions.ajaxSorting = false;

    this.tableOptions.renderComplete = function () {
      $('#tabulator-table').off('change', '.update-record').on('change', '.update-record', function () {
        let isChecked = this.checked;
        let positionRow = $(this).attr("data-position-row");

        let row = _this.tabulatorTable.getRowFromPosition(positionRow, true);

        let record = row._row.data;
        if (isChecked) {
          _this.recordsForUpdate.push(record);
        }
        else {
          _this.recordsForUpdate = _this.recordsForUpdate.filter(item => item !== record);
        }
      });
    };

    this.tableOptions.data = [];

    this.drawTable();
    this.signalRService.startConnection();
    this.signalRService.addTransferChartDataListener();
    this.signalRService.startHttpRequest().subscribe();

    this.getRecords();
    this.signalRService.updateRecordEnded().subscribe(recods => {
      this.tabulatorTable.replaceData(recods);
    });
  }

  private getRecords() {
    this.recordService.getRecords().subscribe(recods => {
      this.tabulatorTable.replaceData(recods);
    });
  }

  private updateRecord() {
    this.recordService.updateRecords(this.recordsForUpdate).subscribe(recods => {
      this.recordsForUpdate = [];
      this.tabulatorTable.replaceData(recods);
    });
  }

  private drawTable(): void {
    this.tabulatorTable = new Tabulator(this.tabulator, this.tableOptions);
    document.getElementById('tabulator-table').appendChild(this.tabulator);
  }
}
