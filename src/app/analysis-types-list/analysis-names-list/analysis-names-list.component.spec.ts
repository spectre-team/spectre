/*
 * analysis-names-list.component.ts
 * Module for analysis types list.
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
import { MatListModule, MatExpansionModule, MatButtonModule } from '@angular/material';
import { AnalysisTypesListComponent } from "../analysis-types-list/analysis-types-list.component";
import {} from 'jasmine';
import { AnalysisNamesListComponent } from './analysis-names-list.component';
import {AnalysisTypesListService} from "../analysis-types-list.service";
import 'rxjs/Rx';
import 'rxjs/add/observable/of';
import {routing} from "../../app.routing";

describe('AnalysisNamesListComponent', () => {
  let component: AnalysisNamesListComponent;
  let fixture: ComponentFixture<AnalysisNamesListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AnalysisNamesListComponent, AnalysisTypesListComponent ],
      imports: [MatListModule, MatExpansionModule, MatButtonModule, routing]
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
