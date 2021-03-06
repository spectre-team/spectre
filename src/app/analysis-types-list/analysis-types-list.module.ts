/*
 * analysis-types-list.module.ts
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

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import {
  MatListModule,
  MatExpansionModule,
  MatButtonModule,
  MatProgressSpinnerModule,
} from '@angular/material';

import { AnalysisTypesListComponent } from './analysis-types-list/analysis-types-list.component';
import { AnalysisTypesListService } from './analysis-types-list.service';
import { AnalysisNamesListComponent } from './analysis-names-list/analysis-names-list.component';

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    MatListModule,
    MatExpansionModule,
    MatButtonModule,
    HttpClientModule,
    MatProgressSpinnerModule,
    RouterModule,
  ],
  declarations: [
    AnalysisTypesListComponent,
    AnalysisNamesListComponent,
  ],
  providers: [
    AnalysisTypesListService,
  ],
  exports: [
    AnalysisTypesListComponent,
  ],
})
export class AnalysisTypesListModule {
}
