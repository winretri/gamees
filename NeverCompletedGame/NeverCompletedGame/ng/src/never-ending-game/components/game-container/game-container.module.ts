import { RxGuessService } from './../../service/rx.guess.service';
import { GameButtonModule } from './../game-button/game-button.module';
import { FormsModule } from '@angular/forms';
import { GameContainerComponent } from './game-container.component';
import { LocalStorageService } from './../../service/local-storage-service.service';
import { RxNeverCompletedGameService } from 'src/never-ending-game/service/rx.never-completed-game.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [GameContainerComponent],
  imports: [
    CommonModule,
    FormsModule,
    GameButtonModule
  ],
  providers: [
    RxNeverCompletedGameService,
    LocalStorageService,
    RxGuessService,
  ],
  exports: [GameContainerComponent]
})
export class GameContainerModule { }
