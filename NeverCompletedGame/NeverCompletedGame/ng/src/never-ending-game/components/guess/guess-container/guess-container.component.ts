import { Subject } from 'rxjs';
import { IGuess } from './../../../model/game.interface';
import { Component, OnInit, Input, OnDestroy } from '@angular/core';

@Component({
  selector: 'ncg-guess-container',
  templateUrl: './guess-container.component.html',
  styleUrls: ['./guess-container.component.scss']
})
export class GuessContainerComponent implements OnInit, OnDestroy {

  private destroy$ = new Subject<null>();

  @Input()
  public guesses: IGuess[];

  @Input()
  public level: number;

  constructor() { }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    this.destroy$.next(null);
  }

}
