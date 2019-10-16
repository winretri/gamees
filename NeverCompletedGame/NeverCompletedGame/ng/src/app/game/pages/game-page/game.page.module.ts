import { GamePage } from './game.page';
import { NeverEndingGameModule } from '../../../../never-ending-game/never-ending-game.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [GamePage],
  imports: [
    CommonModule,
    NeverEndingGameModule,
  ]
})
export class GamePageModule { }
