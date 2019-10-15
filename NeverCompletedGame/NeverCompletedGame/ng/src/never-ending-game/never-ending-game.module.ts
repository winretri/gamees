import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RxNeverCompletedGameService } from './service/rx.never-completed-game.service';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
  ],
  providers: [
    RxNeverCompletedGameService,
    LocalStorageService,
  ]
})
export class NeverEndingGameModule { }
