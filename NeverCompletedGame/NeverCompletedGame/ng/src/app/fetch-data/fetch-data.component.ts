import { RxNeverCompletedGameService } from './../../never-ending-game/service/rx.never-completed-game.service';
import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];
  public data: Observable<string[]>;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private gameService: RxNeverCompletedGameService) {
    http.get<WeatherForecast[]>(baseUrl + 'weatherforecast').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
    this.data = gameService.fetchData();
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
