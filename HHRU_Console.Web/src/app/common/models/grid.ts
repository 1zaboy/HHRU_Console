import { AgDataGridHelper, IHelperStorage } from "../ag-grid/ag-data-grid/ag-data-grid.helper";
import { Action } from "./action";


export type GridDataType = number | string | [] | any;

export class ActionRendererParams {
  action: Action;
}

export class Grid {
  layout: Layout;
  data: GridDataType[][];
  name: string;
  timeStamp: number;
}

export class Layout {
  actions: Action[];
  columnHeaders: HeaderDescription[];
  columns: ColumnDefinition[];
  rows: RowDefinition[];

  gridViewSettings: GridViewSettings;
  gridHelper?: IHelperStorage;

  constructor() {
    this.columnHeaders = [];
    this.columns = [];
    this.rows = [];
  }
}

export class ColumnDefinition {
  index?: number;
  tag: number;
  columnType?: RowColumnType;

  id: number;
  key: string;
  required?: boolean;
  name: string;
  headerId: number;
  field: string;
}

export type RowTagType = number | any;

export class RowDefinition {
  id: number;
  key: string;
  rowType: RowColumnType;

  tag?: RowTagType;
  data?: GridDataType[];

  hierarchyKey?: number[];
  parentIndex?: number;

  rowIndex?: number;
}

export class HeaderDescription {
  id: number;
  parentId: number;
  name: string;

  colDef?: string;
  type?: HeaderDescriptionType;
  tag?: number;

  constructor(header: HeaderDescription = null) {
    if (header) this.prepareHeader(header);
    else this.prepareEmptyHeader();
  }

  private prepareEmptyHeader() {
    this.id = null;
    this.type = HeaderDescriptionType.Empty;
  }
  private prepareHeader(header: HeaderDescription) {
    this.id = header.id;
    this.parentId = header.parentId;
    this.name = header.name;
    this.type = HeaderDescriptionType.Actual;
  }
}

export enum HeaderDescriptionType {
  Empty = 0,
  Actual = 1
}

export class GridViewSettings {
  suppressAutosize: boolean;
  flexColumns: boolean;
  suppressColumnVirtualization: boolean;
  suppressRowVirtualization: boolean;
  sidePanel: boolean;
  statusBar: boolean;
  rowHeight: number;
  lockPosition: boolean;
  suppressSelectAll: boolean;
  gradient: boolean;
  suppressPaging: boolean;
  suppressSorting: boolean;
  menuTag?: string;
  crossSelect: boolean;
  actionNotPin: boolean;
  actionHide: boolean;
  suppressCellSelection?: boolean;
  stopEditingWhenCellsLoseFocus: boolean;
  // tslint:disable-next-line:ban-types
  cssRules: { [cssClassName: string]: (Function | string) };
}


export class CellRendereParams {
  cellColumn: ColumnDefinition;
  helper: AgDataGridHelper;
  onChanged: CallableFunction;
  values?: string[];
  getWidget?: CallableFunction;
}

export enum RowColumnType {
  Normal = 0,
  Header = 1,
}

