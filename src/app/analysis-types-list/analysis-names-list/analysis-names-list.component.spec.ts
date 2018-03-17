/*
 * analysis-names-list.component.ts
 * Module for analysis names list.
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
import {MatListModule, MatExpansionModule, MatButtonModule, MatCardModule} from '@angular/material';
import { AnalysisTypesListComponent } from "../analysis-types-list/analysis-types-list.component";
import {} from 'jasmine';
import { AnalysisNamesListComponent } from './analysis-names-list.component';
import 'rxjs/Rx';
import 'rxjs/add/observable/of';
import {routing} from "../../app.routing";
import {PreparationListComponent} from "../../preparations/preparation-list/preparation-list.component";
import {MainPageComponent} from "../../main-page/main-page.component";
import {UploadComponent} from "../../upload/upload.component";
import {AnalysisViewComponent} from "../../analysis-view/analysis-view/analysis-view.component";
import {NewAnalysisViewComponent} from "../../new-analysis-view/new-analysis-view/new-analysis-view.component";
import {PageNotFoundComponent} from "../../page-not-found/page-not-found.component";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {GenericFormModule} from "../../generic-form/generic-form.module";

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
        PageNotFoundComponent
      ],
      imports: [
        MatListModule,
        MatExpansionModule,
        MatButtonModule,
        routing,
        FormsModule,
        ReactiveFormsModule,
        MatCardModule,
        GenericFormModule
      ]
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
