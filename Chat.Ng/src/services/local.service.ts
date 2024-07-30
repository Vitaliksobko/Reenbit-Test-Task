import { Injectable } from "@angular/core";


@Injectable({ providedIn: "root" })
export class LocalService {
  
  public static AuthId = "authId";

  
  
  private isLocalStorageAvailable(): boolean {
    const isWindowDefined = typeof window !== 'undefined';
    const isLocalStorageDefined = isWindowDefined && window.localStorage !== undefined;
    return isLocalStorageDefined;
  }

  isAuthenticated(name: string): boolean {
    if (this.isLocalStorageAvailable()) {
      return !!localStorage.getItem(name);
    }
 
    return false;
  }


  put(name: string, value: string) {
    localStorage.setItem(name, value);
  }

  get(name: string): any {
    if (this.isLocalStorageAvailable())
      return localStorage.getItem(name);

    return null;
  }

  remove(name: string) {
    localStorage.removeItem(name)
  }
}