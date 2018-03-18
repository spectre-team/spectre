/*
 * analysis-types-list.service.spec.ts
 * Tests for analysis-types-list service.
 *
   Copyright 2018 Roman Lisak

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

import { TestBed, inject, getTestBed } from '@angular/core/testing';
import 'rxjs/Rx';
import 'rxjs/add/observable/of';

import {AnalysisTypesListService} from './analysis-types-list.service';
import {Observable} from 'rxjs/Observable';
import {HttpClient} from '@angular/common/http';
import {Service} from "../app.service";

class MockHttpClient {
  get(url) {
    return Observable.of(
    {
    analysis: ['divik']
    }
    )
  }
}

describe('AnalysisTypesListService', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        {provide: HttpClient, useClass: MockHttpClient},
        {
          provide: AnalysisTypesListService,
          useValue: {
            getAlgorithms: (algorithmsUrl: string) => Observable.of(
              {
                analysis: ['divik']
              }
            ),
            getUrl: () => '/algorithms/'
          }
        },
        {provide: Service, useValue: {
            getBaseAnalysisApiUrl: () => 'analysis-api-url',
          }}
      ]
    });
  });

  it('should be created', inject([AnalysisTypesListService], (service: AnalysisTypesListService) => {
    expect(service).toBeTruthy();
  }));
});
