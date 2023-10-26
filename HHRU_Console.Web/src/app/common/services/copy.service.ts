import { DOCUMENT } from '@angular/common';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CopyService {
  private _textarea: HTMLTextAreaElement | undefined;

  constructor(@Inject(DOCUMENT) private readonly _document: Document) {
  }

  copy(text: string) {
    this.init(text);
    const result = this._copy();
    this.destroy();
    return result;
  }

  private init(text: string) {
    const textarea = (this._textarea = this._document.createElement('textarea'));
    const styles = textarea.style;

    styles.position = 'fixed';
    styles.top = styles.opacity = '0';
    styles.left = '-999em';
    textarea.setAttribute('aria-hidden', 'true');
    textarea.value = text;
    textarea.readOnly = true;
    (this._document.fullscreenElement || this._document.body).appendChild(textarea);
  }

  private _copy(): boolean {
    const textarea = this._textarea;
    let successful = false;

    try {
      if (textarea) {
        const currentFocus = this._document.activeElement as any as HTMLOrSVGElement | null;

        textarea.select();
        textarea.setSelectionRange(0, textarea.value.length);
        successful = this._document.execCommand('copy');

        if (currentFocus) {
          currentFocus.focus();
        }
      }
    } catch {
      //
    }

    return successful;
  }

  private destroy() {
    const textarea = this._textarea;

    if (textarea) {
      textarea.remove();
      this._textarea = undefined;
    }
  }
}
