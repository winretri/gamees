import { Store } from '@ngrx/store';
import { GameId } from './../../../../never-ending-game/model/game.interface';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import * as fromRoot from '../../../reducers/index.reducer';
import { InitGame } from '../../actions/start.page.action';

@Component({
  selector: 'ncg-start-page',
  templateUrl: './start.page.html',
  styleUrls: ['./start.page.scss']
})
export class StartPage implements OnInit {

  public gameId: GameId = null;

  constructor(public readonly store: Store<fromRoot.State>, private readonly router: Router) { }

  ngOnInit() {
    this.store.dispatch(new InitGame());
    this.store.select(fromRoot.getOpenGameId).subscribe(gameId => {
      console.log('START PAGE' + gameId);
      this.gameId = gameId;
    });
  }

  public onDoContinue(event: MouseEvent) {
    this.router.navigate(['ncg', this.gameId]);
  }

  public onDoReset(event: MouseEvent) {

  }

}
