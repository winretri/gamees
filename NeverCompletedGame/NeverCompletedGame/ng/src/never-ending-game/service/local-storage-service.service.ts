import { Inject, Injectable } from '@angular/core';
import { LOCAL_STORAGE, StorageService } from 'ngx-webstorage-service';

@Injectable()
export class LocalStorageService {
     STORAGE_KEY = 'open_game_id';
     anotherTodolist = [];
     constructor(@Inject(LOCAL_STORAGE) private storage: StorageService) { }

     public storeOpenGameId(gameId: string): void {
       this.storage.set(this.STORAGE_KEY, gameId);
     }

     public get containsOpenGameId(): boolean {
       return  this.storage.has(this.STORAGE_KEY);
     }

     public getOpenGameId(): string {
       return this.storage.get(this.STORAGE_KEY);
     }

     public resetOpenGame() {
       if (this.containsOpenGameId) {
         this.storage.remove(this.STORAGE_KEY);
       }
    }
}
