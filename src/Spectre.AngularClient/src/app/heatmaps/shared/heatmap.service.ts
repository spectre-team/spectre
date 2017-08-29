/*
 * heatmap.service.ts
 * Service providing heatmap.
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

import { Heatmap } from './heatmap';
import {Service} from '../../app.service';

@Injectable()
export class HeatmapService extends Service {

    constructor(private http: Http) { super(); }

    private getHeaders() {
      const headers = new Headers();
      headers.append('Accept', 'application/json');
      return headers;
    }

    get(preparationId: number, channelId: number): Observable<Heatmap> {
      console.log('[HeatmapService] preparationId: ' + preparationId + ' channelId ' + channelId);
      const queryUrl = `${this.getBaseUrl()}/heatmap/${preparationId}?channelId=${channelId}&flag=false`;
      const response = this.http.get(queryUrl, {headers: this.getHeaders()});
      const spectrum = response.map((res: Response) => HeatmapUtil.toHeatmap(res));
      return spectrum;
    }
}

export class HeatmapUtil {
  static toHeatmap(response: Response, className = '[HeatmapService]'): Heatmap {
    console.log(className + ' parsing response:');
    console.log(response);
    const json = response.json();
    console.log(json);
    const max_column = Math.max.apply(null, json.X);
    const min_column = Math.min.apply(null, json.X);
    const max_row = Math.max.apply(null, json.Y);
    const min_row = Math.min.apply(null, json.Y);
    console.log(className + ' found bounds: (' + min_row + ', ' + max_row + '), (' + min_column + ', ' + max_column + ')');
    console.log(className + ' initializing heatmap');
    const data = [];
    for (let i = min_row; i < max_row + 1; i++) {
      data[i - min_row] = [];
      for (let j = min_column; j < max_column + 1; j++) {
        data[i - min_row][j - min_column] = 0;
      }
    }
    console.log(className + ' processing heatmap');
    for (let i = 0; i < json.X.length; i++) {
      const column = json.X[i] - min_column;
      const row = max_row - json.Y[i];
      if (className === '[HeatmapService]') {
        const intensity = json.Intensities[i];
        console.log(className + ' processing (' + row + ', ' + column + ', ' + intensity + ')');
        data[row][column] = intensity;
      } else {
        const jsonData = json.Data[i];
        console.log(className + ' processing (' + row + ', ' + column + ', ' + jsonData + ')');
        data[row][column] = jsonData;
      }
    }
    console.log(className + ' heatmap processed');
    return <Heatmap>({
      data: data
    });
  }
}

