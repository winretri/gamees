import { StartPage } from './game/pages/start-page/start.page';
import { GamePage } from './game/pages/game-page/game.page';
import { NgModule } from '@angular/core';
import { RouterModule, Routes, PreloadAllModules } from '@angular/router';



const routes: Routes = [
  { path: 'ncg', component: StartPage },
  { path: 'ncg/:id', component: GamePage },
  { path: '', pathMatch: 'full', redirectTo: 'ncg' },
  { path: '**', redirectTo: 'ncg' },

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
