import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';


const routes: Routes = [
  {path: 'ddd', loadChildren: './ddd/ddd.module#DddModule'},
  {path: 'alunos', loadChildren: './gerenciadoralunos/gerenciadoralunos.module#GerenciadoralunosModule'},
  {path: 'demo', loadChildren: './demo/demo.module#DemoModule'},
  {path: '**', redirectTo: 'alunos'}
];

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(routes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
