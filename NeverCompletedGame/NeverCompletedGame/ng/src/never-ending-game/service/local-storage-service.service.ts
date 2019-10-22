import { Inject, Injectable } from '@angular/core';
import { LOCAL_STORAGE, StorageService } from 'ngx-webstorage-service';

@Injectable()
export class LocalStorageService {
     STORAGE_KEY = 'open_game_id';
     anotherTodolist = [];
     constructor(@Inject(LOCAL_STORAGE) private storage: StorageService) { }

     public storeOpenGameId(gameId: string): void {
       console.log('store ' + gameId);
       // localStorage.setItem(this.STORAGE_KEY, gameId);
       this.storage.set(this.STORAGE_KEY, gameId);
     }

     public get containsOpenGameId(): boolean {
       // const contains  = localStorage.getItem(this.STORAGE_KEY) != null;
      // console.log('store contains ' + contains);
      //  return localStorage.getItem(this.STORAGE_KEY) != null;
       return  this.storage.has(this.STORAGE_KEY);
     }

     public getOpenGameId(): string {
       // return localStorage.getItem(this.STORAGE_KEY);
       return this.storage.get(this.STORAGE_KEY);
     }

     public resetOpenGame() {
       if (this.containsOpenGameId) {
         // localStorage.removeItem(this.STORAGE_KEY);
         this.storage.remove(this.STORAGE_KEY);
       }
    }
}
