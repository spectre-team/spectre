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

import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import {
  MatExpansionModule,
  MatButtonModule,
  MatListModule,
  MatCardModule,
  MatProgressSpinnerModule
} from '@angular/material';

import { AnalysisTypesListComponent } from './analysis-types-list.component';
import {AnalysisNamesListComponent} from '../analysis-names-list/analysis-names-list.component';
import {HttpClient, HttpClientModule} from '@angular/common/http';
import {routing} from '../../app.routing';
import {PreparationListComponent} from '../../preparations/preparation-list/preparation-list.component';
import {MainPageComponent} from '../../main-page/main-page.component';
import {AnalysisViewComponent} from '../../analysis-view/analysis-view/analysis-view.component';
import {PageNotFoundComponent} from '../../page-not-found/page-not-found.component';
import {NewAnalysisViewComponent} from '../../new-analysis-view/new-analysis-view/new-analysis-view.component';
import {UploadComponent} from '../../upload/upload.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {GenericFormModule} from '../../generic-form/generic-form.module';
import {AnalysisTypesListService} from '../analysis-types-list.service';
import {Observable} from 'rxjs/Observable';
import {Service} from '../../app.service';

class MockHttpClient {
  get(url) {
    return Observable.of(
      {
        analysis: ['divik']
      }
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
        PreparationListComponent,
        MainPageComponent,
        UploadComponent,
        AnalysisViewComponent,
        NewAnalysisViewComponent,
        PageNotFoundComponent
      ],
      imports: [
        MatListModule,
        MatExpansionModule,
        MatButtonModule,
        HttpClientModule,
        routing,
        FormsModule,
        ReactiveFormsModule,
        MatCardModule,
        GenericFormModule,
        MatProgressSpinnerModule
      ],
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
