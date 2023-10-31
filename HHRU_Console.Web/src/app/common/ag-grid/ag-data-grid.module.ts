import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AgDataGridComponent } from './ag-data-grid/ag-data-grid.component';
import { DefCellRendererComponent } from './ag-grid-components/def-cell-renderer/def-cell-renderer.component';
import { DefCellEditorComponent } from './ag-grid-components/def-cell-editor/def-cell-editor.component';
import { AgGridModule } from '@ag-grid-community/angular';
import { FormsModule } from '@angular/forms';
import { ModuleRegistry } from '@ag-grid-community/core';
import { ClientSideRowModelModule } from '@ag-grid-community/client-side-row-model';
import { ActionCellRendererComponent } from './ag-grid-components/action-cell-renderer/action-cell-renderer.component';
import { TuiScrollbarModule, TuiSvgModule } from '@taiga-ui/core';
import { TuiFadeModule } from '@taiga-ui/experimental';

ModuleRegistry.registerModules([ClientSideRowModelModule]);

@NgModule({
  declarations: [
    AgDataGridComponent,
    DefCellRendererComponent,
    DefCellEditorComponent,
    ActionCellRendererComponent,
  ],
  exports: [
    AgDataGridComponent
  ],
  imports: [
    CommonModule,
    AgGridModule,
    FormsModule,
    TuiSvgModule,
    TuiFadeModule,
    TuiScrollbarModule,
  ],
})
export class AgDataGridModule {
}
