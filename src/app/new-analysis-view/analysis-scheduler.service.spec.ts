/*
 * analysis-scheduler.service.spec.ts
 * Tests for analysis scheduling.
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
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/observable/of';
import 'rxjs/Rx';

import { Service } from '../app.service';

import { AnalysisSchedulerService } from './analysis-scheduler.service';

const scheduleUrl = 'analysis-api-url/schedule/blah/';

describe('AnalysisSchedulerService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientModule,
      ],
      providers: [
        {provide: Service, useValue: {
          getBaseAnalysisApiUrl: () => 'analysis-api-url',
        }},
        {provide: HttpClient, useValue: {
          post: (url: string, data: any): Observable<any> => {
            if (url === scheduleUrl) {
              return Observable.of('');
            } else {
              return new Observable(observer => {
                observer.error('Error: bad url used in API call');
                observer.complete();
              })
            }
          }
        }},
        AnalysisSchedulerService,
      ]
    });
  });

  it('should be created', inject([AnalysisSchedulerService], (service: AnalysisSchedulerService) => {
    expect(service).toBeTruthy();
  }));

  it('provides valid layout url for algorithm', inject([AnalysisSchedulerService], (service: AnalysisSchedulerService) => {
    expect(service.formLayoutUrl('blah')).toBe('analysis-api-url/layout/inputs/blah/');
  }));

  it('provides valid schema url for algorithm', inject([AnalysisSchedulerService], (service: AnalysisSchedulerService) => {
    expect(service.formSchemaUrl('blah')).toBe('analysis-api-url/schema/inputs/blah/');
  }));

  it('queries scheduler with post', inject([AnalysisSchedulerService], (service: AnalysisSchedulerService) => {
    service.enqueue('blah', {}).subscribe(
      response => expect(response).toBeDefined(),
      error => expect(error).toBeUndefined()
    )
  }));
});
