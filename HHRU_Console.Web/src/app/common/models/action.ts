export enum ActionKey {
  VIEW_VACANCY = 1,
}

export enum ActionType {
  action = 0,
}

export const ActionIconMap: { [type: number]: string } = {
  [ActionKey.VIEW_VACANCY]: 'tuiIconPlay',
}

export class Action<T = any> {
  actionKey: ActionKey;
  title: string;
  type: ActionType;
  timeStamp?: number;
  data: T;
  disabled: boolean;
  hidden: boolean;
  waiting?: boolean;


  constructor(
    type: ActionType,
    actionKey?: ActionKey,
    title?: string,
    timeStamp?: number,
    data?: T,
    disabled?: boolean,
    waiting?: boolean
  ) {
    this.actionKey = actionKey;
    this.title = title;
    this.type = type;
    this.timeStamp = timeStamp;
    this.data = data;
    this.disabled = disabled;
    this.hidden = false;
    this.waiting = waiting ?? false;
  }
}
