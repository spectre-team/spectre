/*
 * table-view.component.spec.ts
 * Tests for component for displaying tabular summary of results.
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

import { TableViewComponent } from './table-view.component';

describe('TableViewComponent', () => {
  let component: TableViewComponent<any>;
  let fixture: ComponentFixture<TableViewComponent<any>>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TableViewComponent ],
      imports: [
        MatTableModule,
      ],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TableViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
