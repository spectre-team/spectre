/*
 * result-download.service.ts
 * Service downloading schema for algorithm output querying.
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

import { apiUrl } from '../../environments/apiUrl';
import { AspectDescription } from '../aspect-view/aspect-description';

@Injectable()
export class QuerySchemaDownloadService {

  constructor(private client: HttpClient) { }

  getAspects(algorithm: string): Observable<AspectDescription[]> {
    return this.client.get<AspectDescription[]>(apiUrl.outputsSchemaUrl(algorithm));
  }

}
