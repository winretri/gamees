import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RxNeverCompletedGameService } from './service/rx.never-completed-game.service';
import { GameContainerComponent } from './components/game-container/game-container.component';

@NgModule({
  declarations: [GameContainerComponent],
  imports: [
    CommonModule,
  ],
  providers: [
    RxNeverCompletedGameService,
    LocalStorageService,
  ]
})
export class NeverEndingGameModule { }
