/*
 * analysis-list.service.ts
 * Service providing divik result.
 *
 Copyright 2018 Sebastian Pustelnik

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

import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/Rx';

import { Service } from '../../app.service';
import { AnalysisName } from './analysis-name';


@Injectable()
export class AnalysisListService extends Service {

  constructor(private http: Http) {
    super();
  }

  private getHeaders() {
    const headers = new Headers();
    headers.append('Accept', 'application/json');
    return headers;
  }

  getAnalysisList(preparationId: number, analysis_type: string): Observable<AnalysisName[]> {
    const queryUrl = `${this.getBaseUrl()}/preparation/${preparationId}/analyses/${analysis_type}`;
    return this.http.get(queryUrl, {headers: this.getHeaders()})
      .map(res => res.json() as AnalysisName[])
      .catch(err => {
        return Observable.throw(err);
      });
  }

  newAnalysis(preparationId: number, analysis_type: string) {
    const queryUrl = `${this.getBaseUrl()}/preparation/${preparationId}/analyses/${analysis_type}`;
    this.http.post(queryUrl, {headers: this.getHeaders()});
  }
}
