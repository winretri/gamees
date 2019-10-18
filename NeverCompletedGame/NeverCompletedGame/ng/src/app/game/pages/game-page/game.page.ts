import { GameContainerComponent } from './../../../../never-ending-game/components/game-container/game-container.component';
import { Component, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'ncg-game-page',
  templateUrl: './game.page.html',
  styleUrls: ['./game.page.scss'],
  providers: []
})
export class GamePage implements OnInit {

  @ViewChild('ncgContainer', {static: false})
  public ncgContainer: GameContainerComponent;

  constructor() { }

  ngOnInit() {
  }

  public onDoReset(event: MouseEvent) {
    console.log(this.ncgContainer);
    this.ncgContainer.onReset();
  }

}
