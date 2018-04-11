/*
 * analysis-types-list.component.spec.ts
 * Tests for analysis-types-list component.
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

import {
  async,
  ComponentFixture,
  TestBed,
} from '@angular/core/testing';
import {
  HttpClient,
  HttpClientModule,
} from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { Observable } from 'rxjs/Observable';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import {
MatExpansionModule,
MatButtonModule,
MatListModule,
MatCardModule,
MatProgressSpinnerModule,
} from '@angular/material';
import {
FormsModule,
ReactiveFormsModule,
} from '@angular/forms';

import { AnalysisTypesListComponent } from './analysis-types-list.component';
import { AnalysisNamesListComponent } from '../analysis-names-list/analysis-names-list.component';
import { AnalysisTypesListService } from '../analysis-types-list.service';

class MockHttpClient {
  get(url) {
    return Observable.of(
      {
        analysis: ['divik'],
      },
    )
  }
}

describe('AnalysisTypesListComponent', () => {
  let component: AnalysisTypesListComponent;
  let fixture: ComponentFixture<AnalysisTypesListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AnalysisTypesListComponent,
        AnalysisNamesListComponent,
      ],
      imports: [
        BrowserAnimationsModule,
        HttpClientModule,
        RouterModule,
        RouterTestingModule.withRoutes([]),
        FormsModule,
        ReactiveFormsModule,
        MatListModule,
        MatExpansionModule,
        MatButtonModule,
        MatCardModule,
        MatProgressSpinnerModule,
      ],
      providers: [
        {provide: HttpClient, useClass: MockHttpClient},
        {
          provide: AnalysisTypesListService,
          useValue: {
            getAlgorithms: (algorithmsUrl: string) => Observable.of(
              {
                analysis: ['divik'],
              },
            ),
            getUrl: () => '/algorithms/',
          },
        },
      ],
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AnalysisTypesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
