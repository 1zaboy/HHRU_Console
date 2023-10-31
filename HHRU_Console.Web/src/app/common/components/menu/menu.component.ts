import { Component } from '@angular/core';
import { MenuItem, menuItems } from '../../models/menu';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent {

  constructor(
    private readonly _router: Router
  ) {}

  get menuItems() {
    return menuItems;
  }

  onClick(menuItem: MenuItem) {
    this._router.navigate([`/${menuItem.path}`])
  }
}

