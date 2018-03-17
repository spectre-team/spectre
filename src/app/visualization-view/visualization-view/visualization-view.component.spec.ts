/*
 * visualization-view.component.spec.ts
 * Tests for visualization-view component.
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

import {
  MatTableModule,
} from '@angular/material';

import { PlotlyModule } from '../../plotly/plotly.module';
import { TableViewComponent } from '../table-view/table-view.component';
import { VisualizationType } from '../visualization-type.enum';
import { VisualizationViewComponent } from './visualization-view.component';

describe('VisualizationViewComponent', () => {
  let component: VisualizationViewComponent;
  let fixture: ComponentFixture<VisualizationViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        TableViewComponent,
        VisualizationViewComponent,
      ],
      imports: [
        MatTableModule,
        PlotlyModule,
      ],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VisualizationViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should work for plot', () => {
    component.data = {
      data: [{x: [1, 2, 3], y: [4, 5, 6]}],
      layout: {title: 'Blah'}
    }
    component.type = VisualizationType.Plot;
    expect(component).toBeTruthy();
  });

  it('should work for table', () => {
    component.data = {
      columns: [{key: 'x', name: 'blah'}],
      data: [{x: 1}]
    }
    component.type = VisualizationType.Table;
    expect(component).toBeTruthy();
  });
});
