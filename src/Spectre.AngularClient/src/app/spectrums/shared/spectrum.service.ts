/*
 * spectrum.service.ts
 * Service providing spectrum.
 *
   Copyright 2017 Grzegorz Mrukwa

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
import {Service} from '../../app.service';

@Injectable()
export class SpectrumService extends Service {

  constructor(private http: Http) {
    super();
  }

  get(preparationId: number, spectrumId: number): Observable<Spectrum> {
    const queryUrl = `${this.getBaseUrl()}/spectrum/${preparationId}?spectrumId=${spectrumId}`;
    const response = this.http.get(queryUrl, {headers: this.getHeaders()});
    const spectrum = response.map(toSpectrum);
    return spectrum;
  }

  getAll(preparationId: number): Observable<Array<Spectrum>> {
    const queryUrl = `${this.getBaseUrl()}/spectrum/${preparationId}`;
    const response = this.http.get(queryUrl, {headers: this.getHeaders()});
    const spectra = response.map(toSpectra);
    return spectra;
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
//TODO: refactorization
function toSpectra(response: Response): Array<Spectrum> {
  const json = response.json();
  var spectra = [];
  for (let spectrum of json) {
    spectra.push(<Spectrum>({
      id: spectrum.Id,
      mz: spectrum.Mz,
      intensities: spectrum.Intensities,
      x: spectrum.X,
      y: spectrum.Y
    }));
  };
  return spectra;
}
