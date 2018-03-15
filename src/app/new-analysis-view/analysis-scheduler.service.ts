/*
 * analysis-scheduler.service.ts
 * Analysis scheduling.
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

@Injectable()
export class AnalysisSchedulerService {

  private layoutUrlPattern: string;
  private schemaUrlPattern: string;
  private scheduleUrlPattern: string;

  constructor(private client: HttpClient, private urlProvider: Service) {
    const analysisApiUrlBase = urlProvider.getBaseAnalysisApiUrl();
    this.layoutUrlPattern = analysisApiUrlBase + apiUrl.inputsLayoutUrl;
    this.schemaUrlPattern = analysisApiUrlBase + apiUrl.inputsSchemaUrl;
    this.scheduleUrlPattern = analysisApiUrlBase + apiUrl.scheduleUrl;
  }

  formSchemaUrl(algorithm: string): string {
    return this.schemaUrlPattern.format(algorithm);
  }

  formLayoutUrl(algorithm: string): string {
    return this.layoutUrlPattern.format(algorithm);
  }

  enqueue(algorithm: string, parameters: any): Observable<any> {
    const targetUrl = this.scheduleUrlPattern.format(algorithm);
    return this.client.post(targetUrl, parameters);
  }

}
