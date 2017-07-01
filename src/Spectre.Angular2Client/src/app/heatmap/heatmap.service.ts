import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/Rx';

import { Heatmap } from './heatmap';

@Injectable()
export class HeatmapService {

    private baseUrl = 'http://localhost/spectre_api/api';

    constructor(private http: Http) { }

    private getHeaders() {
      const headers = new Headers();
      headers.append('Accept', 'application/json');
      return headers;
    }

    get(preparationId: number, channelId: number): Observable<Heatmap> {
      console.log('[HeatmapService] preparationId: ' + preparationId + ' channelId ' + channelId);
      const queryUrl = `${this.baseUrl}/preparations/${preparationId}?channelId=${channelId}&flag=false`;
      const response = this.http.get(queryUrl, {headers: this.getHeaders()});
      const spectrum = response.map(toHeatmap);
      return spectrum;
    }
}

function toHeatmap(response: Response): Heatmap {
    console.log('[HeatmapService] parsing response:');
    console.log(response);
    const json = response.json();
    console.log(json);
    const max_column = Math.max.apply(null, json.X);
    const min_column = Math.min.apply(null, json.X);
    const max_row = Math.max.apply(null, json.Y);
    const min_row = Math.min.apply(null, json.Y);
    console.log('[HeatmapService] found bounds: (' + min_row + ', ' + max_row + '), (' + min_column + ', ' + max_column + ')');
    console.log('[HeatmapService] initializing heatmap');
    const data = [];
    for (let i = min_row; i < max_row + 1; i++) {
      data[i - min_row] = [];
      for (let j = min_column; j < max_column + 1; j++) {
        data[i - min_row][j - min_column] = 0;
      }
    }
    console.log('[HeatmapService] processing heatmap');
    for (let i = 0; i < json.X.length; i++) {
        const column = json.X[i] - min_column;
        const row = max_row - json.Y[i];
        const intensity = json.Intensities[i];
        console.log('[HeatmapService] processing (' + row + ', ' + column + ', ' + intensity + ')');
        data[row][column] = intensity;
    }
    console.log('[HeatmapService] heatmap processed');
    return <Heatmap>({
        data: data
    });
}
