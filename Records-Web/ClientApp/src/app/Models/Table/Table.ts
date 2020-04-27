import { TableColumn } from "./TableColumn";

export class Table {
  placeholder: string = "No Data Available";
  height: string = "100%";
  data: any[] = [];
  layout: string = "fitColumns";
  pagination: any = "local";
  paginationSize: number = 25;
  columnMinWidth: number = 20;
  columns: Array<TableColumn> = <Array<TableColumn>>[];
  ajaxURL: any = "";
  ajaxFiltering: boolean = true;
  ajaxSorting: boolean = true;
  renderComplete: () => void;
  ajaxURLGenerator: (url: any, config: any, params: any) => void;
  footerElement: string = "";
  validationFailed: (cell: any, value: any, validators: any) => void;
  ajaxResponse: (url: string, params: any, response: any) => void;
}
