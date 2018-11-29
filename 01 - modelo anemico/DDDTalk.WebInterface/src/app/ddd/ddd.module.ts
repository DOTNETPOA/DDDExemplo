import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DddAppComponent } from './ddd-app.component';
import { Routes } from '@angular/router';
import { TopoComponent } from './componentes/topo/topo.component';
import { MenuComponent } from './componentes/menu/menu.component';

const routes: Routes = [
  {path: 'alunos', component: DddAppComponent,
    children: [
      {path:':id', component: MainContentComponent},
      {path:'', component: MainContentComponent}
    ]
},
  {path: '**', redirectTo: ''}
];

@NgModule({
  declarations: [DddAppComponent, TopoComponent, MenuComponent],
  imports: [
    CommonModule
  ]
})
export class DddModule { }
