import { MakeGuess } from './../../state/game/actions';
import { IGame } from './../../model/game.interface';
import { Component, OnInit, AfterViewInit, OnDestroy } from '@angular/core';
import { Store } from '@ngrx/store';

import { Subject } from 'rxjs';

import * as gameAction from '../../state/';
import { gameSelectors } from 'src/never-ending-game/state/game/selectors';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'ncg-game-container',
  templateUrl: './game-container.component.html',
  styleUrls: ['./game-container.component.scss']
})
export class GameContainerComponent implements OnInit, OnDestroy {

  private destroy$ = new Subject<null>();

  public game: IGame;

  public gameLoaded = false;

  public gameMenuLoaded = false;

  public solution: string;

  constructor(private store: Store<any>) { }

  ngOnInit() {
    console.log('game container');
    this.store.dispatch(new gameAction.InitGame());

    this.store.select(gameSelectors.getGame)
    .pipe(takeUntil(this.destroy$))
    .subscribe(game => {
        this.game = game;
      }
     );

    this.store.select(gameSelectors.getGameLoaded)
    .pipe(takeUntil(this.destroy$))
    .subscribe(gameLoaded => {
        this.gameLoaded = gameLoaded;
      }
     );
  }

  onKeyUpEnter(event: KeyboardEvent) {
    this.makeGuess();
  }

  makeGuess() {
    this.store.dispatch(new MakeGuess({
      gameId: this.game.id,
      solution: this.solution,
    }));
  }

  public onReset(): void {
    console.log('reset game container');
    this.store.dispatch(new gameAction.ResetGame());
  }

  ngOnDestroy(): void {
    this.destroy$.next(null);
  }

}
