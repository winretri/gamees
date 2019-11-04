import { Subject } from 'rxjs';
import { IGuess, IGame } from './../../../model/game.interface';
import { Component, OnInit, OnDestroy, Input, OnChanges } from '@angular/core';

@Component({
  selector: 'ncg-guess-list',
  templateUrl: './guess-list.component.html',
  styleUrls: ['./guess-list.component.scss']
})
export class GuessListComponent implements OnInit, OnDestroy, OnChanges {


  @Input()
  public guesses: IGuess[];
  @Input()
  public level: number;
  public message = '';
  private destroy$ = new Subject<null>();
  constructor() { }

  ngOnInit() {
    this.calculateMessage();
  }

  calculateMessage() {
    if (!this.hasGuesses()) {
      this.message = 'You haven\'t made any guesses yet.';
    } else {
      this.message = '';
    }
  }

  hasGuesses() {
    return this.guesses && this.guesses.length > 0;
  }

  ngOnDestroy(): void {
    this.destroy$.next(null);
  }

  ngOnChanges(changes: import('@angular/core').SimpleChanges): void {
    this.calculateMessage();
  }

}
