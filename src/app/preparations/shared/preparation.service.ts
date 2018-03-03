/*
 * preparation.service.ts
 * Service providing preparations list.
 *
   Copyright 2017 Sebastian Pustelnik, Grzegorz Mrukwa

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

import { Preparation } from './preparation';
import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/Rx';
import { Service } from '../../app.service';
import { apiUrl } from '../../../environments/apiUrl';

@Injectable()
export class PreparationService extends Service {

  constructor(private http: Http) { super(); }

  getAll(): Observable<Preparation[]> {
    return this.http
      .get(this.getBasePreparationUrl() + apiUrl.preparationsUrl, {headers: this.getHeaders()})
      .map(mapPreparations);
  }

  getPreparationById(preparationId: number): Observable<Preparation> {
    return this.http
      .get(this.getBasePreparationUrl() +  apiUrl.preparationUrl.format(preparationId), {headers: this.getHeaders()})
      .map(r => toPreparation(r.json()));
  }

  private getHeaders() {
    const headers = new Headers();
    headers.append('Accept', 'application/json');
    return headers;
  }
}

 function mapPreparations(response: Response): Preparation[] {
  return response.json().map(toPreparation);
}

function toPreparation(r: any): Preparation {
  return <Preparation>({
    id: r.Id,
    name: r.Name,
    spectraNumber: r.SpectraNumber
  });
}
