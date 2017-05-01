import { Preparation } from './preparation';
import {Injectable} from '@angular/core';
import { Http, Response, Headers} from '@angular/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/Rx';

@Injectable()
export class PreparationsService {
  private baseUrl = 'http://localhost/spectre_api/api';

  constructor(private http: Http) {
  }

  getAll(): Observable<Preparation[]> {
    return this.http
      .get(`${this.baseUrl}/preparations`, {headers: this.getHeaders()})
      .map(mapPreparations);
  }

  private getHeaders() {
    const headers = new Headers();
    headers.append('Accept', 'application/json');
    headers.append('Access-Control-Allow-Headers', 'Content-Type');
    headers.append('Access-Control-Allow-Methods', 'GET');
    headers.append('Access-Control-Allow-Origin', '*'); //it is optional I think
    return headers;
  }
}
 function mapPreparations(response: Response): Preparation[] {
  return response.json().results.map(toPreparation);
}

function toPreparation(r: any): Preparation {
  return <Preparation>({
    id: r.id,
    name: r.name
  });
}




