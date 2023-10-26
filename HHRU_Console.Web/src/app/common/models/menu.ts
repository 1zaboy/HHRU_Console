export enum MenuItemCode {
  Responses = 1,
  ResumeUp = 2,
}

export interface MenuItem {
  title: string;
  code: MenuItemCode;
  path: string;
  icon: string;
}

export const menuItems: MenuItem[] = [
  {
    title: 'Responses',
    code: MenuItemCode.Responses,
    path: 'overview',
    icon: 'tuiIconList',
  },
  {
    title: 'Resume Up',
    code: MenuItemCode.ResumeUp,
    path: 'resumeup',
    icon: 'tuiIconTrendingUp',
  },
];
