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
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import 'rxjs/Rx';
import 'rxjs/add/observable/of';

import {AnalysisTypesListService} from "./analysis-types-list.service";

describe('AnalysisTypesListService', () => {
  let injector: TestBed;
  let service: AnalysisTypesListService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AnalysisTypesListService]
    });
    injector = getTestBed();
    service = injector.get(AnalysisTypesListService);
  });

  it('should be created',()  => {
    expect(service).toBeTruthy();
  });
});
