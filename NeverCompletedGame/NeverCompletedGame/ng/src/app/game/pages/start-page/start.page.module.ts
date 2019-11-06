import { NeverEndingGameModule } from './../../../../never-ending-game/never-ending-game.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StartPage } from './start.page';

@NgModule({
  declarations: [StartPage],
  imports: [
    CommonModule,
    NeverEndingGameModule
  ],
  exports: [
    StartPage
  ]
})
export class StartPageModule { }
