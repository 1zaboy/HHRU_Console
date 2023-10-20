import { GridDataType, Layout } from 'src/app/common/models/grid';

export class DataManager {
  private _data: GridDataType[][];

  constructor(
    private _layout: Layout,
    data: GridDataType[][],
  ) {
    this._data = data;
  }

  setAll(data: GridDataType[][]): void {
    this._data = data;
  }

  get(row: string, column: string): unknown {
    if (!this._data[row] || typeof this._data[row][column] === 'undefined') {
      return null;
    }
    return this._data[row][column];
  }

  getRow(index: number): any[] {
    return this._data[index];
  }

  set(row: string, column: string, value: GridDataType): void {
    if (!this._data[row] || typeof this._data[row][column] === 'undefined') {
      this._data[row][column] = [];
    }
    this._data[row][column] = value;
  }

  setValueToLayout(rowKey: string, columnKey: string, value: GridDataType): void {
    const col = this._layout.columns.find(k => k.key === columnKey);
    const row = this._layout.rows.find(e => e.key === rowKey);
    if (row && col) {
      row.data[col.index] = value;
    }
  }
}
