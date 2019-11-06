import { GameId } from './../../../../never-ending-game/model/game.interface';
import { Store } from '@ngrx/store';
import { GameContainerComponent } from './../../../../never-ending-game/components/game-container/game-container.component';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'ncg-game-page',
  templateUrl: './game.page.html',
  styleUrls: ['./game.page.scss'],
  providers: []
})
export class GamePage implements OnInit {

  public gameId: GameId;

  @ViewChild('ncgContainer', {static: false})
  public ncgContainer: GameContainerComponent;

  constructor(private readonly store: Store<any>,
    private readonly route: ActivatedRoute) {
  }

  ngOnInit() {
    this.gameId = this.route.snapshot.params.id;
    console.log('GID:' + this.gameId);
  }

  public onDoReset(event: MouseEvent) {
    this.ncgContainer.onReset();
  }

}
