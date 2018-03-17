/*
 * aspect-view.component.spec.ts
 * Tests ofs component for visualization of single aspect of the result.
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
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/observable/of';
import 'rxjs/Rx';

import {
  MatExpansionModule,
} from '@angular/material';

import { Service } from '../../app.service';
import { AspectViewComponent } from './aspect-view.component';
import { GenericFormModule } from '../../generic-form/generic-form.module';
import { VisualizationViewModule } from '../../visualization-view/visualization-view.module';
import { VisualizationType } from '../../visualization-view/visualization-type.enum';
import { ResultDownloadService } from '../result-download.service';

describe('AspectViewComponent', () => {
  let component: AspectViewComponent;
  let fixture: ComponentFixture<AspectViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AspectViewComponent ],
      imports: [
        MatExpansionModule,
        GenericFormModule,
        VisualizationViewModule,
      ],
      providers: [
        {provide: HttpClient, useValue: {
          get: (url) => Observable.of({}),
          post: (url, data) => Observable.of({}),
        }},
        Service,
        ResultDownloadService,
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AspectViewComponent);
    component = fixture.componentInstance;
    component.description = {
      aspect: 'summary',
      friendly_name: 'Summary name',
      description: 'Very long description. To long to read it.',
      output_type: VisualizationType.Plot,
    }
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
