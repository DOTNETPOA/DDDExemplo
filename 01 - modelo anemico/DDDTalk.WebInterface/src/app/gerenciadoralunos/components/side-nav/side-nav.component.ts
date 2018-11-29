import { Component, OnInit, NgZone, ViewChild } from '@angular/core';
import { Aluno } from '../../models/aluno';
import { Observable } from 'rxjs';
import { AlunoService } from '../../services/aluno.service';
import { Router } from '@angular/router';
import { MatSidenav } from '@angular/material';

const SMALL_WIDTH_BREAKPOINT = 720;

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.css']
})
export class SideNavComponent implements OnInit {

  alunos: Observable<Aluno[]>;

  constructor(private router: Router, private alunoService: AlunoService) {
    
  }

  @ViewChild(MatSidenav) sidenav: MatSidenav;
  
  ngOnInit() {
    this.alunos = this.alunoService.alunos;
    this.alunoService.loadAll();

    this.alunos.subscribe(data => {
      if(data.length > 0) this.router.navigate(['/alunos', data[0].id]);
    });

    this.router.events.subscribe(()=> {
        this.sidenav.close();
    })
  }
}
