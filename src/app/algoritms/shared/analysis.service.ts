/*
 * analysis.service.ts
 * Service providing analysis result.
 *
 Copyright 2018 Grzegorz Mrukwa, Sebastian Pustelnik

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

import { Heatmap } from '../../heatmaps/shared/heatmap';
import { Service } from '../../app.service';
import { HeatmapUtil } from '../../heatmaps/shared/heatmap-util';
import { DivikConfig } from '../../divik-results/shared/divik-config';
import { DivikSummary } from '../../divik-results/shared/divik.summary';

@Injectable()
export class AnalysisService extends Service {

  constructor(private http: Http) {
    super();
  }

  private getHeaders() {
    const headers = new Headers();
    headers.append('Accept', 'application/json');
    return headers;
  }

  get(preparationId: number, analysisType: string, analysisName: string): Observable<Heatmap> {
    const queryUrl = `${this.getBaseAnalysisUrl()}/preparation/${preparationId}/analyses/${analysisType}/${analysisName}`;
    const response = this.http.get(queryUrl, {headers: this.getHeaders()});
    return response.map((res: Response) => HeatmapUtil.toHeatmap(res, '[DivikService]'))
      .catch(err => {
        return Observable.throw(err);
      });
  }

  getConfig(preparationId: number, analysisType: string, analysisName: string): Observable<DivikConfig> {
    const queryUrl = `${this.getBaseAnalysisUrl()}/preparation/${preparationId}/analyses/${analysisType}/${analysisName}/config`;
    const response = this.http.get(queryUrl, {headers: this.getHeaders()});
    return response.map(res => res.json() as DivikConfig);
  }

  getSummary(preparationId, analysisType: string, analysisName: string): Observable<DivikSummary> {
    const queryUrl = `${this.getBaseAnalysisUrl()}/preparation/${preparationId}/analyses/${analysisType}/${analysisName}/summary`;
    const response = this.http.get(queryUrl, {headers: this.getHeaders()});
    return response.map(res => res.json() as DivikSummary);
  }
}

