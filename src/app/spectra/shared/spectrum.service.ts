/*
 * spectrum.service.ts
 * Service providing spectrum.
 *
   Copyright 2017 Grzegorz Mrukwa, Sebastian Pustelnik, Daniel Babiak

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

import { Spectrum } from './spectrum';
import { apiUrl } from '../../../environments/apiUrl';

@Injectable()
export class SpectrumService {

  constructor(private http: Http) { }

  get(preparationId: number, spectrumId: number): Observable<Spectrum> {
    const queryUrl = apiUrl.spectrumByIdUrl(preparationId, spectrumId);
    const response = this.http.get(queryUrl, {headers: this.getHeaders()});
    const spectrum = response.map(toSpectrum);
    return spectrum;
  }

  getByCoordinates(preparationId: number, x: number, y: number): Observable<Spectrum> {
    const queryUrl = apiUrl.spectrumByCoordUrl(preparationId, x, y);
    const response = this.http.get(queryUrl, {headers: this.getHeaders()});
    const spectrum = response.map(toSpectrum);
    return spectrum;
  }

  private getHeaders() {
    const headers = new Headers();
    headers.append('Accept', 'application/json');
    return headers;
  }
}

function toSpectrum(response: Response): Spectrum {
  const json = response.json();
  return <Spectrum>({
    id: json.Id,
    mz: json.Mz,
    intensities: json.Intensities,
    x: json.X,
    y: json.Y
  });
}
