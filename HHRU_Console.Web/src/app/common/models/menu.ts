export enum MenuItemCode {
  Responses = 1,
}

export interface MenuItem {
  title: string;
  code: MenuItemCode;
}

export const menuItems: MenuItem[] = [
  { title: 'Responses', code: MenuItemCode.Responses }
];
