/*
 * finished-analyses.service.ts
 * Gets finished analyses list.
 *
   Copyright 2018 Grzegorz Mrukwa

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
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/Rx';

import { Service } from '../app.service';
import { apiUrl } from '../../environments/apiUrl';
import { Analysis } from './analysis';

@Injectable()
export class FinishedAnalysesService {

  private url = new Service().getBaseAnalysisApiUrl() + apiUrl.finishedAnalysesUrl;

  constructor(private client: HttpClient) { }

  getFinished(algorithm: string): Observable<Analysis[]> {
    return this.client.get<Analysis[]>(this.url.format(algorithm));
  }

}
