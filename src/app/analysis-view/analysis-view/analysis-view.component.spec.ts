/*
 * analysis-view.component.spec.ts
 * Tests analysis-view component.
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

import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/observable/of';

import { AnalysisViewComponent } from './analysis-view.component';

describe('AnalysisViewComponent', () => {
  let component: AnalysisViewComponent;
  let fixture: ComponentFixture<AnalysisViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AnalysisViewComponent ],
      providers: [
        {
          provide: ActivatedRoute,
          useValue: {paramMap: Observable.of({get: (name: string) => 'divik'})}
        },
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AnalysisViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
