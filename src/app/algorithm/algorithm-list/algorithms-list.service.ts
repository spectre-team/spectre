import {Injectable} from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import { Service } from '../../app.service';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class AlgorithmsListService extends Service {
  private listUrl = 'http://localhost:2003/algorithms/';

  constructor(private http: HttpClient) {
    super();
  }

  getAlgorithms() {
    return this.http.get(this.listUrl);
  }
}
