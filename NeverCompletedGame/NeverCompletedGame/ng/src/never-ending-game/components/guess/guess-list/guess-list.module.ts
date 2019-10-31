import { GuessItemModule } from './../guess-item/guess-item.module';
import { GuessListComponent } from './guess-list.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    GuessListComponent,
  ],
  imports: [
    CommonModule,
    GuessItemModule
  ],
  exports: [
    GuessListComponent,
  ]
})
export class GuessListModule { }
