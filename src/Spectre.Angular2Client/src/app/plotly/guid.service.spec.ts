/*
 * guid.service.sppec.ts
 * Tests GUID-providing service.
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

import { TestBed, inject } from '@angular/core/testing';

import { GuidService } from './guid.service';

describe('GuidService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GuidService]
    });
  });

  it('should inject service', inject([GuidService], (service: GuidService) => {
    expect(service).toBeTruthy();
  }));

  it('should return different GUIDs', inject([GuidService], (service: GuidService) => {
    const guid1 = service.next();
    const guid2 = service.next();
    expect(guid1).not.toEqual(guid2);
  }));
});
