/*
 * generic-form-fetch.service.spec.ts
 * Unit tests for service fetching form definition.
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
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { GenericFormFetchService } from './generic-form-fetch.service';

describe('GenericFormFetchService', () => {
  let client: HttpClient;
  let controller: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
      ],
      providers: [
        GenericFormFetchService,
      ]
    });

    client = TestBed.get(HttpClient);
    controller = TestBed.get(HttpTestingController);
  });

  it('should be created', inject([GenericFormFetchService], (service: GenericFormFetchService) => {
    expect(service).toBeTruthy();
  }));
});
