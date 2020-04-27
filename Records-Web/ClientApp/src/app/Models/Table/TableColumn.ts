export class TableColumn {
  title: string = "";
  field: string = "";
  editor: any = false;
  headerFilter: any = false;
  headerFilterParams: any = {};
  headerFilterPlaceholder: string = "filter...";
  formatter: any = "html";
  visible: boolean = true;
  headerSort: boolean = true;
  align: string = "center";
  validator: any = false;
  minWidth: number = 40;
  width: number = 40;
}
