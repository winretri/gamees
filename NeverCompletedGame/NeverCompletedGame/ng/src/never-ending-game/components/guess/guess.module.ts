import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GuessContainerModule } from './guess-container/guess-container.module';

@NgModule({
  declarations: [],
  imports: [
    GuessContainerModule,
  ],
  exports: [
    GuessContainerModule,
  ]
})
export class GuessModule { }
