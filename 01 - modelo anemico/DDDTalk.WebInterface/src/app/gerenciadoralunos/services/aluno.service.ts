import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Aluno } from '../models/aluno';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AlunoService {
  

  private _alunos: BehaviorSubject<Aluno[]>;
  
  private dataStore: {
    alunos: Aluno[]
  }

  constructor(private http: HttpClient) { 
    this.dataStore  = {alunos: []};
    this._alunos = new BehaviorSubject<Aluno[]>([]);
  }

  get alunos(): Observable<Aluno[]> {
    return this._alunos.asObservable();
  }

  alunoPorId(id: string): Aluno {
    return this.dataStore.alunos.find(a=> a.id == id);
  }

  loadAll(){
    const alunosUrl = "http://localhost:5000/api/Alunos";
    return this.http.get<Aluno[]>(alunosUrl)
            .subscribe(data => {
              this.dataStore.alunos = data;
              this._alunos.next(Object.assign({}, this.dataStore).alunos);
            }, error => {
              console.log("Falha ao carregar alunos");
            });            
  }
}
