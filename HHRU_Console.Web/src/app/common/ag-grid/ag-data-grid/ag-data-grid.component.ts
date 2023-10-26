import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  NgZone,
  OnChanges,
  OnDestroy,
  OnInit,
  Output,
  ViewChild,
  ViewEncapsulation
} from '@angular/core';
import {
  ColDef,
  ColGroupDef,
  Column,
  ColumnApi,
  GridApi,
  GridOptions,
  ICellRendererParams,
  SideBarDef,
  ToolPanelDef
} from 'ag-grid-community';
import { AgDataGridHelper } from './ag-data-grid.helper';
import { DefCellRendererComponent } from '../ag-grid-components/def-cell-renderer/def-cell-renderer.component';
import { DefCellEditorComponent } from '../ag-grid-components/def-cell-editor/def-cell-editor.component';
import { Subscription } from 'rxjs';
import { CellEditorSelectorResult, CellRendererSelectorResult } from 'ag-grid-community/dist/lib/entities/colDef';
import { ActionRendererParams, CellRendereParams, ColumnDefinition, Grid, GridDataType, GridViewSettings, RowColumnType, RowDefinition } from '../../models/grid';
import { LocaleFormats } from '../../models/localeFormats';
import { DEFAULT_FORMATS } from '../../models/localeMap';
import { Action } from '../../models/action';
import { ActionCellRendererComponent } from '../ag-grid-components/action-cell-renderer/action-cell-renderer.component';
import { CopyService } from '../../services/copy.service';


@Component({
  selector: 'app-ag-data-grid',
  templateUrl: './ag-data-grid.component.html',
  styleUrls: [
    './ag-data-grid.component.scss',
  ],
  encapsulation: ViewEncapsulation.None,
})
export class AgDataGridComponent implements OnInit, OnChanges, OnDestroy {
  @ViewChild('agDataGrid', { static: false }) gridElement: ElementRef;

  @Input() grid: Grid;

  @Output() valueChanged = new EventEmitter<any>();
  @Output() cellSelected = new EventEmitter<any>();
  @Output() ready = new EventEmitter<void>();

  readonly PAGE_SIZING: number = 50;
  readonly MAX_COLUMNS_COUNT: number = 50;

  readonly GRID_ROW_HEIGHT: number = 34;
  readonly GRID_HEADER_HEIGHT: number = 34;
  readonly TWIN_GRID_HEADER_HEIGHT: number = 24;

  readonly MAX_COLUMN_WIDTH: number = 300;
  readonly ACTION_COLUMN_WIDTH: number = 30;

  readonly DEF_CELL_RENDERER = DefCellRendererComponent;
  readonly DEF_CELL_EDITOR = DefCellEditorComponent;

  readonly AG_COLUMN_HEADER = 'agColumnHeader';

  readonly ACTION_CELL_RENDERER = ActionCellRendererComponent;

  readonly GRID_SELECT_EMPTY = 'Empty';

  readonly precisionFormat: string = '1.0-';

  initialized = false;

  helper: AgDataGridHelper;

  gridOptions: GridOptions;
  rowDefs: RowDefinition[];
  columnDefs: (ColDef | ColGroupDef)[];
  locale: LocaleFormats;
  localeCode: string;

  private _subscriptions: Subscription[] = [];
  private _gridApi: GridApi;
  private _columnApi: ColumnApi;

  constructor(
    private readonly _zone: NgZone,
    private readonly _copyService: CopyService,
  ) {
    this.setLocale();
  }

  // static function

  ngOnChanges(): void {
    if (!this.grid.layout.gridViewSettings)
      this.grid.layout.gridViewSettings = new GridViewSettings();
  }

  ngOnInit(): void {
    this.helper = new AgDataGridHelper(this.grid.layout);
    this.helper.dataManager.setAll(this.grid.data);

    this.prepareColDefs();
    this.prepareRowDefs();
    this.addActionsCells();

    this.grid.layout.rows = this.rowDefs;

    this.gridOptions = ({
      columnDefs: this.columnDefs,
      rowHeight: this.grid.layout.gridViewSettings.rowHeight ? this.grid.layout.gridViewSettings.rowHeight : this.GRID_ROW_HEIGHT,
      headerHeight: this.grid.layout.columnHeaders.length > 0 ? this.TWIN_GRID_HEADER_HEIGHT : this.GRID_HEADER_HEIGHT,

      suppressColumnVirtualisation: !this.needColumnVirtualization,
      suppressRowVirtualisation: !this.needRowVirtualization,
      suppressDragLeaveHidesColumns: true,
      autoSizePadding: 1,

      rowSelection: 'single',

      suppressRowTransform: true,

      processDataFromClipboard: (params) => {
        return this.processPastData(params.data);
      },

      onCellDoubleClicked: (params) => {
        console.log("double click");
        return this.cellDoubleClicked(params);
      },

      onCellClicked: (params) => {
        console.log("click");
        this.cellClick(params);
      },

      stopEditingWhenCellsLoseFocus: this.grid.layout.gridViewSettings.stopEditingWhenCellsLoseFocus,
    } as GridOptions);

    this.gridOptions.rowData = this.rowDefs;

    this.initialized = true;
  }

  onGridReady(params): void {
    this._gridApi = params.api;
    this._columnApi = params.columnApi;

    this._columnApi.autoSizeAllColumns();

    this.ready.next();
  }

  ngOnDestroy(): void {
    for (const subscription of this._subscriptions) {
      subscription.unsubscribe();
    }
  }

  cellClick(event: any) {
    const e = event.event;
    console.log(event);

    if ('ctrlKey' in e) {
      if (e.ctrlKey == true) {
        const successful = this._copyService.copy(event.value);
        console.log(successful);
      }
    }
  }

  cellDoubleClicked($event): void {
    console.log($event);

    const column = $event.colDef.cellRendererParams.cellColumn as ColumnDefinition;
    const row = $event.data as RowDefinition;
    const rowIndexAboutSort = $event.node.rowIndex as number;
    if (row && column) {
      this._gridApi.startEditingCell({
        rowIndex: rowIndexAboutSort,
        colKey: column.id.toString()
      });
    }
  }

  get needPaginator(): boolean {
    return !this.grid.layout.gridViewSettings.suppressPaging && this.grid.layout.rows.length > this.PAGE_SIZING;
  }

  get needColumnVirtualization(): boolean {
    return !this.grid.layout.gridViewSettings.suppressColumnVirtualization && this.grid.layout.columns.length > this.MAX_COLUMNS_COUNT;
  }

  get needRowVirtualization(): boolean {
    return !(this.grid.layout.gridViewSettings?.suppressRowVirtualization ?? true);
  }

  get sidePanel(): false | SideBarDef {
    if (!this.grid.layout.gridViewSettings.sidePanel) return false;
    const sidePanelParameters = {
      toolPanels: [
        {
          id: 'columns',
          labelDefault: 'Columns',
          labelKey: 'columns',
          iconKey: 'columns',
          toolPanel: 'agColumnsToolPanel',
          toolPanelParams: {
            suppressRowGroups: true,
            suppressValues: true,
            suppressPivots: true,
            suppressPivotMode: true
          }
        } as ToolPanelDef
      ],
      defaultToolPanel: ''
    } as SideBarDef;
    return sidePanelParameters;
  }

  get statusPanel() {
    return {
      statusPanels: [
        {
          statusPanel: 'agTotalAndFilteredRowCountComponent',
          align: 'left'
        },
        { statusPanel: 'agFilteredRowCountComponent' },
        { statusPanel: 'agSelectedRowCountComponent' },
        { statusPanel: 'agAggregationComponent' }
      ]
    };
  }

  get isEmpty(): boolean {
    return this.rowDefs.length === 0;
  }

  private setLocale(): void {
    this.locale = DEFAULT_FORMATS;
    this.localeCode = '';
  }

  processPastData(data: string[][]): string[][] {
    if (data.length > 0) {
      if (data[data.length - 1].length === 1 && !data[data.length - 1][0]) {
        data.splice(data.length - 1, 1);
      }
    }
    return data;
  }

  onPasteEnd(): void {
    this.valueChanged.next(null);
  }

  private prepareColDefs(): void {
    this.columnDefs = this.getOrderedColumns().map((c) => this.defColDef(c));
  }

  private getOrderedColumns(): ColumnDefinition[] {
    let result = this.grid.layout.columns;
    result.forEach((c, i) => {
      c.field = i.toString();
      c.index = i;
    });
    return result;
  }

  private defColDef(column: ColumnDefinition): ColDef {
    const parent = this.grid.layout.columnHeaders.find(h => h.id === column.headerId);
    const result = {
      headerName: column.name,
      field: column.field,
      colId: column.id.toString(),
      maxWidth: this.MAX_COLUMN_WIDTH,

      cellRendererSelector: (params) => {
        return this.renderer(params.colDef, params.data);
      },
      cellEditorSelector: (params) => {
        return this.editor(params.colDef, params.data);
      },

      headerComponent: this.headerComponent(),

      flex: this.grid.layout.gridViewSettings.flexColumns ? 1 : null,

      sortable: !this.grid.layout.gridViewSettings.suppressSorting,
      lockPosition: this.grid.layout.gridViewSettings.lockPosition,

      filterValueGetter: (params) => {
        return this.getFilterValue(params.colDef, params.data);
      },
      valueGetter: (params) => {
        return this.getActualValue(params.colDef, params.data);
      },
      valueSetter: (params) => {
        return this.setValueToCell(params.colDef, params.data, params.newValue, params.oldValue);
      },

      cellEditorParams: (params) => {
        return {
          cellColumn: column,
          values: null,
          helper: this.helper
        } as CellRendereParams;
      },
      cellClassRules: this.grid.layout.gridViewSettings.cssRules
    } as ColDef;

    return result;
  }

  headerComponent() {
    return this.AG_COLUMN_HEADER;
  }

  private renderer(colDef: ColDef, row: RowDefinition): CellRendererSelectorResult {
    let renderer = this.DEF_CELL_RENDERER;
    var data = row.data[colDef.colId];
    return {
      component: renderer,
      params: {
        getValue: () => data,
      } as ICellRendererParams
    } as CellRendererSelectorResult;
  }

  private editor(colDef: ColDef, row: RowDefinition): CellEditorSelectorResult {
    let editor = this.DEF_CELL_EDITOR;
    return {
      component: editor
    } as CellEditorSelectorResult;
  }

  private getActualValue(colDef: ColDef, row: RowDefinition): any {
    const value = row.data[colDef.field];
    return value;
  }

  private getFilterValue(colDef: ColDef, row: RowDefinition): any {
    const value = row.data[colDef.field];
    return value;
  }

  private setValueToCell(colDef: ColDef, row: RowDefinition, newValue: GridDataType, oldValue: GridDataType) {
    const column = colDef.cellRendererParams.cellColumn as ColumnDefinition;
    if (newValue !== oldValue) {
      let value = newValue;

      this.helper.dataManager.setValueToLayout(row.key, column.key, value);
    }
  }

  private prepareRowDefs(): void {
    let value = this.grid.layout.rows;
    value = this.fillRows(value);
    value.forEach((item, index) => {
      item.rowType = item.rowType ? item.rowType : RowColumnType.Normal;
      item.data = this.helper.dataManager.getRow(index);
      item.rowIndex = index;
    });
    this.rowDefs = value;
  }

  private fillRows(value: RowDefinition[]): RowDefinition[] {
    if (!value || value.length === 0) {
      value = this.grid.data.map(_ => new RowDefinition());
    }
    return value;
  }

  private addActionsCells(): void {
    if (!this.columnDefs) this.columnDefs = [];
    const actions = this.grid.layout.actions;
    if (actions && actions.length > 0) {
      actions.forEach((a) => {
        this.columnDefs.push(this.actionColDef(a));
      });
    }
  }

  private actionColDef(action: Action): ColDef {
    return {
      cellRenderer: this.ACTION_CELL_RENDERER,

      minWidth: this.ACTION_COLUMN_WIDTH,
      maxWidth: this.ACTION_COLUMN_WIDTH,

      pinned: this.grid.layout.gridViewSettings.actionNotPin ? '' : 'right',
      suppressMenu: true,

      lockVisible: true,
      toolPanelClass: ['hidden-column'],
      hide: this.grid.layout.gridViewSettings.actionHide,

      cellRendererParams: {
        action,
      } as ActionRendererParams
    } as ColDef;
  }
}


export class HeaderNode {
  isCellDef: boolean;
  parent: number;
  headerId: number;
  data: ColDef | ColGroupDef;
  children: HeaderNode[];

  constructor(
    isCellDef: boolean,
    parent: number,
    data: ColDef | ColGroupDef,
    headerId?: number
  ) {
    this.isCellDef = isCellDef;
    this.parent = parent;
    this.data = data;
    this.headerId = headerId;
    this.children = [];
  }
}
