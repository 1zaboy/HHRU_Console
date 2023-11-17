import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class StorageService {
  public localStorageSetItem(key: string, data: any): void {
    localStorage.setItem(key, JSON.stringify(data));
  }

  public localStorageGetItem<T>(key: string): T {
    const stringData = localStorage.getItem(key);
    return stringData == null ? null : JSON.parse(stringData);
  }

  public localStorageClear(): void {
    localStorage.clear();
  }
}
