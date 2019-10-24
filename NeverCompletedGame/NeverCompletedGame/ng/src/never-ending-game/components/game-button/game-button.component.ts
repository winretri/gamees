import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'ncg-game-button',
  templateUrl: './game-button.component.html',
  styleUrls: ['./game-button.component.scss']
})
export class GameButtonComponent implements OnInit {

  @Output() gameClick = new EventEmitter<MouseEvent>();

  constructor() { }

  ngOnInit() {
  }

  public onClick(event: MouseEvent) {
    this.gameClick.emit(event);
  }
}
