import { GameEvent } from './../../model/game.event.interface';
import { RxEventListenerService } from './../../service/rx.event-listener.service';
import { MakeGuess, LoadGame } from './../../state/game/actions';
import { IGame } from './../../model/game.interface';
import { Component, OnInit, AfterViewInit, OnDestroy } from '@angular/core';
import { Store } from '@ngrx/store';

import { Subject } from 'rxjs';

import * as gameAction from '../../state/';
import { gameSelectors } from 'src/never-ending-game/state/game/selectors';
import { takeUntil, throttleTime, delay } from 'rxjs/operators';

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

  public event: GameEvent;

  public answerClass: string;

  public completed = false;

  constructor(private store: Store<any>, private eventListener: RxEventListenerService) { }

  ngOnInit() {
    this.store.dispatch(new gameAction.InitGame());

    this.store.select(gameSelectors.getGame)
    .pipe(takeUntil(this.destroy$))
    .subscribe(game => {
        this.reset();
        this.game = game;
        this.completed = game != null ? this.game.completed : false;
      }
     );



    this.store.select(gameSelectors.getGameLoaded)
    .pipe(takeUntil(this.destroy$))
    .subscribe(gameLoaded => {
        this.gameLoaded = gameLoaded;
      }
     );

     this.store.select(gameSelectors.getLastGameEvent)
    .pipe(takeUntil(this.destroy$), delay(1000))
    .subscribe(event => {
        console.log('LAST STORE EVENT: ' + event);
      }
     );

     this.eventListener.getMessage()
     .pipe(takeUntil(this.destroy$))
     .subscribe(event => {
       this.event = event;
       console.log('STREAM EVENT: ' + event);
       if (event.DomainEventName === 'LevelSucceeded') {
         this.answerClass = 'correct';
         this.store.dispatch(new LoadGame(this.game.id));
       } else if (event.DomainEventName === 'LevelFailed') {
         this.answerClass = 'wrong';
       } else if (event.DomainEventName === 'GameCompleted') {
         this.eventListener.stopListeningForGameEvents(this.game.id);
       }
      }
     );
  }

  onKeyUpEnter(event: KeyboardEvent) {
    this.makeGuess();
  }

  onKeyUp(event: KeyboardEvent) {
    this.answerClass = '';
  }

  makeGuess() {
    this.answerClass = '';
    this.store.dispatch(new MakeGuess({
      gameId: this.game.id,
      solution: this.solution,
    }));
  }

  public reset(): void {
    this.answerClass = '';
    this.solution = '';
  }

  public onReset(): void {
    this.reset();
    this.store.dispatch(new gameAction.ResetGame(this.game.id));
  }

  public onDoReset(event: MouseEvent) {
    this.onReset();
  }

  public onReload(event: MouseEvent) {
    this.store.dispatch(new gameAction.LoadGame(this.game.id));
  }

  ngOnDestroy(): void {
    this.destroy$.next(null);
  }

}
