/*
 * new-analysis-view.module.ts
 * Module with view for adding new analyses.
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

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import {
  MatCardModule,
  MatSnackBarModule,
} from '@angular/material';

import { GenericFormModule } from '../generic-form/generic-form.module';
import { AnalysisSchedulerService } from './analysis-scheduler.service';
import { NewAnalysisViewComponent } from './new-analysis-view/new-analysis-view.component';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    MatCardModule,
    MatSnackBarModule,
    GenericFormModule,
  ],
  declarations: [
    NewAnalysisViewComponent,
  ],
  exports: [
    NewAnalysisViewComponent,
  ],
  providers: [
    AnalysisSchedulerService,
  ]
})
export class NewAnalysisViewModule { }
