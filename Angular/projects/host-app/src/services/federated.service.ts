import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FederatedComponentService {
  private showFederatedComponentSubject = new BehaviorSubject<boolean>(false);
  showFederatedComponent$ = this.showFederatedComponentSubject.asObservable();

  constructor() { }

  setShowFederatedComponent(show: boolean): void {
    this.showFederatedComponentSubject.next(show);
  }
}
