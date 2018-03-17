/*
 * aspect-view.module.ts
 * Module for visualization of single aspect of analysis result.
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
  MatExpansionModule,
} from '@angular/material';

import { Service } from '../app.service';
import { AspectViewComponent } from './aspect-view/aspect-view.component';
import { GenericFormModule } from '../generic-form/generic-form.module';
import { VisualizationViewModule } from '../visualization-view/visualization-view.module';
import { ResultDownloadService } from './result-download.service';

@NgModule({
  declarations: [
    AspectViewComponent,
  ],
  exports: [
    AspectViewComponent,
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    MatExpansionModule,
    GenericFormModule,
    VisualizationViewModule,
  ],
  providers: [
    Service,
    ResultDownloadService,
  ],
})
export class AspectViewModule { }
