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

import {Injectable} from '@angular/core';
import {Http, Response, Headers} from '@angular/http';
import {Observable} from 'rxjs/Rx';
import 'rxjs/Rx';

import {Heatmap} from '../../heatmaps/shared/heatmap';
import {Service} from '../../app.service';
import {DivikConfig} from "./divik-config";

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
    return response.map(toHeatmap);
  }

  getConfig(preparationId: number, divikId: number): Observable<DivikConfig> {
    const queryUrl = `${this.getBaseUrl()}/divikResult/${preparationId}?divikId=${divikId}`;
    const response = this.http.get(queryUrl, {headers: this.getHeaders()});
    return response.map(toDivikConfig);
  }
}

function toDivikConfig(response: Response): DivikConfig {
  const json = response.json();
  console.log(json);
  return <DivikConfig>({
    maxK: json.MaxK,
    level: json.Level,
    usingLevels: json.UsingLevels,
    amplitude: json.Amplitude,
    variance: json.Variance,
    percentSizeLimit: json.PercentSizeLimit,
    featurePreservationLimit: json.FeaturePreservationLimit,
    metric: json.Metric,
    plottingPartitions: json.PlottingPartitions,
    plottingPartitionsRecursively: json.PlottingPartitionsRecursively,
    plottingDecomposition: json.PlottingDecomposition,
    plottingDecompositionRecursively: json.PlottingDecompositionRecursively,
    maxDecompositionComponents: json.MaxDecompositionComponents,
  })
    ;
}

function toHeatmap(response: Response): Heatmap {
  console.log('[DivikService] parsing response:');
  console.log(response);
  const json = response.json();
  console.log(json);
  const max_column = Math.max.apply(null, json.X);
  const min_column = Math.min.apply(null, json.X);
  const max_row = Math.max.apply(null, json.Y);
  const min_row = Math.min.apply(null, json.Y);
  console.log('[DivikService] found bounds: (' + min_row + ', ' + max_row + '), (' + min_column + ', ' + max_column + ')');
  console.log('[DivikService] initializing heatmap');
  const data = [];
  for (let i = min_row; i < max_row + 1; i++) {
    data[i - min_row] = [];
    for (let j = min_column; j < max_column + 1; j++) {
      data[i - min_row][j - min_column] = 0;
    }
  }
  console.log('[DivikService] processing heatmap');
  for (let i = 0; i < json.X.length; i++) {
    const column = json.X[i] - min_column;
    const row = max_row - json.Y[i];
    const divikData = json.Data[i];
    console.log('[DivikService] processing (' + row + ', ' + column + ', ' + divikData + ')');
    data[row][column] = divikData;
  }
  console.log('[DivikService] heatmap processed');
  return <Heatmap>({
    data: data
  });
}
