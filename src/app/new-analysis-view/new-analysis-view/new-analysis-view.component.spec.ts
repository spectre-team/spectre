/*
 * new-analysis-view.component.spec.ts
 * Tests new-analysis-view component.
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
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/observable/of';

import {
  MatCardModule,
  MatSnackBar,
  MatSnackBarModule
} from '@angular/material';

import { GenericFormModule } from '../../generic-form/generic-form.module';
import { GenericFormFetchService } from '../../generic-form/generic-form-fetch.service';
import { AnalysisSchedulerService } from '../analysis-scheduler.service';
import { NewAnalysisViewComponent } from './new-analysis-view.component';

describe('NewAnalysisViewComponent', () => {
  let client: HttpClient;
  let scheduler: AnalysisSchedulerService;
  let component: NewAnalysisViewComponent;
  let fixture: ComponentFixture<NewAnalysisViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewAnalysisViewComponent ],
      imports: [
        MatCardModule,
        MatSnackBarModule,
        GenericFormModule,
      ],
      providers: [
        {provide: HttpClient, useValue: {
          get: (url: string) => Observable.of({}),
          post: (url: string, data: any) => Observable.of({})}},
        MatSnackBar,
        GenericFormFetchService,
        AnalysisSchedulerService,
        {
          provide: ActivatedRoute,
          useValue: {paramMap: Observable.of({get: (parameterName: string) => 'divik'})}
        },
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    client = TestBed.get(HttpClient);
    scheduler = TestBed.get(AnalysisSchedulerService);
    fixture = TestBed.createComponent(NewAnalysisViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('submits form data into schedule', () => {
    spyOn(scheduler, 'enqueue').and.returnValue(Observable.of({}));
    component.submit({blah: 1});
    expect(scheduler.enqueue).toHaveBeenCalledWith('divik', {blah: 1})
  });
});
