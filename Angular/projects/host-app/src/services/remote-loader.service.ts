import { Injectable } from '@angular/core';
import { loadRemoteModule } from '@angular-architects/module-federation';

@Injectable({
  providedIn: 'root'
})
export class RemoteLoaderService {
  loadRemoteModule<T = any>(remoteName: string, exposedModule: string, remoteEntry: string): Promise<T> {
    return loadRemoteModule<T>({ remoteName, exposedModule, remoteEntry });
  }
}
