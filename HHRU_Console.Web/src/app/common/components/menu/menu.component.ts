import { Component } from '@angular/core';
import { menuItems } from '../../models/menu';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent {

  get menuItems() {
    return menuItems;
  }
}

