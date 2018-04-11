/*
 * result-download.service.spec.ts
 * Tests of service downloading schema for algorithm output querying.
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

import { TestBed, inject } from '@angular/core/testing';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/observable/of';
import 'rxjs/Rx';

import { QuerySchemaDownloadService } from './query-schema-download.service';

class MockHttpClient {
  get(url) {
    return Observable.of([
      {
        aspect: 'summary',
        friendly_name: 'Some longer name',
        description: 'A description too long to read. For sure.',
        query_format: {
          title: 'divik',
          type: 'object',
          properties: { datasetName: { type: 'string' } }
        },
        output_type: 'table',
      },
      {
        aspect: 'negligible',
        friendly_name: 'Nothing to see here',
        description: 'Lorem ipsum or not.',
        query_format: {
          title: 'wololo',
          type: 'object',
          properties: { blah: { type: 'string' } }
        },
        output_type: 'plot',
      },
    ]);
  }
}

describe('QuerySchemaDownloadService', () => {
  let client: HttpClient;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        {provide: HttpClient, useClass: MockHttpClient},
        QuerySchemaDownloadService,
      ]
    });

    client = TestBed.get(HttpClient);
  });

  it('should be created', inject([QuerySchemaDownloadService], (service: QuerySchemaDownloadService) => {
    expect(service).toBeTruthy();
  }));

  it('returns all aspects', inject([QuerySchemaDownloadService], (service: QuerySchemaDownloadService) => {
    service.getAspects('divik').subscribe(response => {
      expect(response.length).toBe(2);
      expect(response[0].aspect).toBe('summary');
      expect(response[1].aspect).toBe('negligible');
    });
  }));
});
