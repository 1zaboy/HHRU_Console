import { Component, ElementRef, ViewChild } from '@angular/core';
import { ICellEditorAngularComp } from '@ag-grid-community/angular';
import { ICellEditorParams } from '@ag-grid-community/core';

export enum KEY_CODE {
  UP_ARROW = 38,
  DOWN_ARROW = 40
}

@Component({
  selector: 'app-def-cell-editor',
  templateUrl: './def-cell-editor.component.html',
  styleUrls: ['./def-cell-editor.component.scss']
})
export class DefCellEditorComponent implements ICellEditorAngularComp {
  @ViewChild('editCellInput', {static: false}) input: ElementRef;

  value: string;
  locale: string;

  constructor() {
    this.locale = '';
  }

  agInit(params: ICellEditorParams<any, any, any>): void {
    this.value = params.value;
  }

  getValue() {
    return this.value;
  }

  afterGuiAttached(): void {
    this.input.nativeElement.focus();
    this.input.nativeElement.addEventListener('keydown', this.onKeyPress.bind(this));
  }

  onKeyPress(event): void {
    if (event.which === KEY_CODE.UP_ARROW || event.which === KEY_CODE.DOWN_ARROW) {
      event.preventDefault();
    }
  }

  get inputType(): string {
    return 'text';
  }
}
