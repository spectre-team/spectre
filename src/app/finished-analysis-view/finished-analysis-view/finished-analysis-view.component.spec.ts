/*
 * finished-analysis-view.component.spec.ts
 * Tests component for visualization of all aspects of the result.
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
import { ActivatedRoute } from '@angular/router';
import { MatCardModule } from '@angular/material';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';

import { FinishedAnalysisViewComponent } from './finished-analysis-view.component';
import { AspectViewModule } from '../../aspect-view/aspect-view.module';
import { QuerySchemaDownloadService } from '../query-schema-download.service';
import { VisualizationType } from '../../visualization-view/visualization-type.enum';
import { Service } from '../../app.service';
import { ResultDownloadService } from '../../aspect-view/result-download.service';
import { GenericFormFetchService } from '../../generic-form/generic-form-fetch.service';


describe('FinishedAnalysisViewComponent', () => {
  let component: FinishedAnalysisViewComponent;
  let fixture: ComponentFixture<FinishedAnalysisViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FinishedAnalysisViewComponent ],
      imports: [
        MatCardModule,
        AspectViewModule,
      ],
      providers: [
        {
          provide: ActivatedRoute,
          useValue: {paramMap: Observable.of({get: (what: string) => 'some_id'})}
        },
        {
          provide: QuerySchemaDownloadService,
          useValue: { getAspects: (algorithm: string) => Observable.of([
                {
                  aspect: 'summary',
                  friendly_name: 'S U M M A R Y',
                  description: 'Do not read it.            I asked gently.',
                  output_type: VisualizationType.Plot,
                },
            ])}
        },
        Service,
        {
          provide: ResultDownloadService,
          useValue: {
            getResult: (algorithm, is, aspect, query) => Observable.of({}),
          },
        },
        {
          provide: GenericFormFetchService,
          useValue: {
            getSchema: (url) => Observable.of({}),
            getLayout: (url) => Observable.of({}),
          },
        },
      ],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FinishedAnalysisViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
