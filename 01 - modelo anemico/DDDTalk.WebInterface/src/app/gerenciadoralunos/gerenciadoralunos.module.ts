import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';

import { MaterialModule } from '.././shared/material.module';
import {FlexLayoutModule} from '@angular/flex-layout';
import { FormsModule } from '@angular/forms';

import { GerenciadoralunosAppComponent } from './gerenciadoralunos-app.component';
import { ToolbarComponent } from './components/toolbar/toolbar.component';
import { MainContentComponent } from './components/main-content/main-content.component';
import { SideNavComponent } from './components/side-nav/side-nav.component';
import { AlunoService } from './services/aluno.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';

const routes: Routes = [
  {path: '', component: GerenciadoralunosAppComponent,
    children: [
      {path:':id', component: MainContentComponent},
      {path:'', component: MainContentComponent}
    ]
},
  {path: '**', redirectTo: ''}
];

@NgModule({
  declarations: [GerenciadoralunosAppComponent, ToolbarComponent, MainContentComponent, SideNavComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    MaterialModule,
    FlexLayoutModule,
    FormsModule,
    RouterModule.forChild(routes)
  ],
  providers: [
    AlunoService
  ]
})
export class GerenciadoralunosModule { }
