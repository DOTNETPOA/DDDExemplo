import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-buttons',
  template: `
      <button mat-button>
      <mat-icon>
          face
      </mat-icon>Alunos
    </button> 
    <br/>
    <button mat-button>Inscrições</button><br/>
    <button mat-button>Turmas</button>
  `,
  styles: []
})
export class ButtonsComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
