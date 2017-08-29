/*
 * divik.service.ts
 * Service providing divik result.
 *
 Copyright 2017 Grzegorz Mrukwa, Sebastian Pustelnik

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
import { DivikConfig } from './divik-config';
import { HeatmapUtil } from '../../heatmaps/shared/heatmap.service';

@Injectable()
export class DivikService extends Service {

  constructor(private http: Http) {
    super();
  }

  private getHeaders() {
    const headers = new Headers();
    headers.append('Accept', 'application/json');
    return headers;
  }

  get (preparationId: number, divikId: number, level: number): Observable<Heatmap> {
    const queryUrl = `${this.getBaseUrl()}/divikResult/${preparationId}?divikId=${divikId}&level=${level}`;
    const response = this.http.get(queryUrl, {headers: this.getHeaders()});
    return response.map((res: Response) => HeatmapUtil.toHeatmap(res, '[DivikService]'));
  }

  getConfig(preparationId: number, divikId: number): Observable<DivikConfig> {
    const queryUrl = `${this.getBaseUrl()}/divikResult/${preparationId}?divikId=${divikId}`;
    const response = this.http.get(queryUrl, {headers: this.getHeaders()});
    return response.map(toDivikConfig);
  }
}

function toDivikConfig(response: Response): DivikConfig {
  const json = response.json();
  const properties = {};
  properties['Max K'] = json.MaxK;
  properties['Level'] = json.Level;
  properties['Using levels'] = json.UsingLevels;
  properties['Amplitude'] = json.UsingAmplitudeFiltration;
  properties['Variance'] = json.UsingVarianceFiltration;
  properties['Percent size limit'] = json.PercentSizeLimit;
  properties['Feature preservation limit'] = json.FeaturePreservationLimit;
  properties['Metric'] = json.Metric;
  properties['Plotting partitions'] = json.PlottingPartitions;
  properties['Plotting recursively'] = json.PlottingRecursively;
  properties['Plotting decomposition'] = json.PlottingDecomposition;
  properties['Plotting decomposition recursively'] = json.PlottingDecompositionRecursively;
  properties['Max decomposition components'] = json.MaxComponentsForDecomposition;
  return <DivikConfig>({
    properties: properties
  });
}
