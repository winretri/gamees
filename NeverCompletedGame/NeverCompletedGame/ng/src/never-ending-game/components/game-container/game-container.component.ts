import { Component, OnInit, AfterViewInit, OnDestroy } from '@angular/core';
import { Store } from '@ngrx/store';

import { Subject } from 'rxjs';

import * as gameAction from '../../state/';

@Component({
  selector: 'ncg-game-container',
  templateUrl: './game-container.component.html',
  styleUrls: ['./game-container.component.scss']
})
export class GameContainerComponent implements OnInit, OnDestroy {

  private destroy$ = new Subject<null>();

  constructor(private store: Store<any>) { }

  ngOnInit() {
    console.log('game container');
    this.store.dispatch(new gameAction.InitGame());
  }

  ngOnDestroy(): void {
    this.destroy$.next(null);
  }

}
