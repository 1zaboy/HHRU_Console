export enum MenuItemCode {
  Responses = 1,
  ResumeUp = 2,
}

export interface MenuItem {
  title: string;
  code: MenuItemCode;
  path: string;
}

export const menuItems: MenuItem[] = [
  {
    title: 'Responses',
    code: MenuItemCode.Responses,
    path: 'overview',
  },
  {
    title: 'Resume Up',
    code: MenuItemCode.ResumeUp,
    path: 'resumeup',
  },
];
