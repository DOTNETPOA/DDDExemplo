import { Component, OnInit } from '@angular/core';
import { Aluno } from '../../models/aluno';
import { ActivatedRoute } from '@angular/router';
import { AlunoService } from '../../services/aluno.service';

@Component({
  selector: 'app-main-content',
  templateUrl: './main-content.component.html',
  styleUrls: ['./main-content.component.css']
})
export class MainContentComponent implements OnInit {

  aluno: Aluno;
  
  constructor(private route: ActivatedRoute, private service: AlunoService) {

   }

  ngOnInit() {
    this.route.params.subscribe(params=> {
      let id = params['id'];
      if(!id) id = "";
      this.aluno = null;

      this.service.alunos.subscribe(alunos => {
        if(alunos.length == 0) return;
        setTimeout(() => {
          this.aluno = this.service.alunoPorId(id);
        }, 500);
      });
    });
  }

}
