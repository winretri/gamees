import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {GameButtonComponent} from './game-button.component';


@NgModule({
  declarations: [
    GameButtonComponent,
  ],
  imports: [
    CommonModule
  ],
  exports: [
    GameButtonComponent,
  ]
})
export class GameButtonModule {
}
