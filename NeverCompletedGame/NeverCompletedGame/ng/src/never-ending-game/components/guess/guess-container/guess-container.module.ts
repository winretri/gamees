import { GuessContainerComponent } from './guess-container.component';
import { GuessListModule } from './../guess-list/guess-list.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    GuessContainerComponent,
  ],
  imports: [
    CommonModule,
    GuessListModule,
  ],
  exports: [
    GuessContainerComponent,
  ]
})
export class GuessContainerModule { }
