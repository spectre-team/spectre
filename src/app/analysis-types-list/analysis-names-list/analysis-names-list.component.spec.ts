/*
 * analysis-names-list.component.ts
 * Tests for analysis-names-list component.
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
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';
import 'rxjs/add/observable/of';
import {
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';

import {
MatListModule,
MatExpansionModule,
MatButtonModule,
MatCardModule,
MatProgressSpinnerModule,
} from '@angular/material';

import { routing } from '../../app.routing';
import { PreparationListComponent } from '../../preparations/preparation-list/preparation-list.component';
import { MainPageComponent } from '../../main-page/main-page.component';
import { UploadComponent } from '../../upload/upload.component';
import { AnalysisViewComponent } from '../../analysis-view/analysis-view/analysis-view.component';
import { NewAnalysisViewComponent } from '../../new-analysis-view/new-analysis-view/new-analysis-view.component';
import { PageNotFoundComponent } from '../../page-not-found/page-not-found.component';
import { GenericFormModule } from '../../generic-form/generic-form.module';

import { AnalysisTypesListService } from '../analysis-types-list.service';
import { AnalysisTypesListComponent } from '../analysis-types-list/analysis-types-list.component';
import { AnalysisNamesListComponent } from './analysis-names-list.component';

describe('AnalysisNamesListComponent', () => {
  let component: AnalysisNamesListComponent;
  let fixture: ComponentFixture<AnalysisNamesListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AnalysisNamesListComponent,
        AnalysisTypesListComponent,
        PreparationListComponent,
        MainPageComponent,
        UploadComponent,
        AnalysisViewComponent,
        NewAnalysisViewComponent,
        PageNotFoundComponent,
      ],
      imports: [
        MatListModule,
        MatExpansionModule,
        MatButtonModule,
        routing,
        FormsModule,
        ReactiveFormsModule,
        MatCardModule,
        GenericFormModule,
        MatProgressSpinnerModule,
      ],
      providers: [
        {
          provide: AnalysisTypesListService,
          useValue: {
            getAlgorithms: (algorithmsUrl: string) => Observable.of(
              {
                analysis: ['divik'],
              },
            ),
          },
        },
      ],
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AnalysisNamesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
