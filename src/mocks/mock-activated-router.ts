/*
 * mock-activated-router.ts
 * Mock for ActivatedRoute to check behaviour on different URL params.
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

import { ActivatedRoute, ActivatedRouteSnapshot, Params } from '@angular/router';
import { Observable } from 'rxjs/Observable';

class MockActivatedRouteSnapshot extends ActivatedRouteSnapshot {
    params: Observable<Params>;
    constructor(parameters: Observable<Params>) {
        super();
        this.params = parameters;
    }

    toString(): string {
        return '';
    }
}

export class MockActivatedRoute extends ActivatedRoute {
  params: Observable<Params>;

  constructor(parameters: Observable<Params>) {
    super();
    this.params = parameters;
    this.snapshot = new MockActivatedRouteSnapshot(this.params);
  }
}
