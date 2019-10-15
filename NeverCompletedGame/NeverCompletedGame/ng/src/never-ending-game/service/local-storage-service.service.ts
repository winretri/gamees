import { Inject, Injectable } from '@angular/core';
import { SESSION_STORAGE, StorageService } from 'ngx-webstorage-service';
import * as uuid from 'uuid';


@Injectable()
export class LocalStorageService {
     STORAGE_KEY = 'local_todolist';
     anotherTodolist = [];
     constructor(@Inject(SESSION_STORAGE) private storage: StorageService) { }
     public storeOnLocalStorage(gameId: uuid): void {
          // insert updated array to local storage
          this.storage.set(this.STORAGE_KEY, gameId);
          console.log(this.storage.get(this.STORAGE_KEY) || 'LocaL storage is empty');
     }
}
