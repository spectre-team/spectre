/*
 * app.routing.ts
 * Root for routing throught the app.
 *
   Copyright 2017 Grzegorz Mrukwa

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

import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { PreparationListComponent } from './preparations/preparation-list/preparation-list.component';
import { MainPageComponent } from './main-page/main-page.component';
import { UploadComponent } from './upload/upload.component';
import { AnalysisViewComponent } from './analysis-view/analysis-view/analysis-view.component';
import { NewAnalysisViewComponent } from './new-analysis-view/new-analysis-view/new-analysis-view.component';
import { FinishedAnalysisViewComponent } from './finished-analysis-view/finished-analysis-view/finished-analysis-view.component';

const appRoutes: Routes = [
  { path: '', component: PreparationListComponent, pathMatch: 'full'}, // redirect to home page on load
  { path: 'preparations', component: PreparationListComponent, pathMatch: 'full'},
  { path: 'mainPage', component: MainPageComponent, pathMatch: 'full'},
  { path: 'uploadDataset', component: UploadComponent, pathMatch: 'full'},
  { path: 'analysis/:algorithmName', component: AnalysisViewComponent, pathMatch: 'full' },
  { path: 'analysis/new/:algorithm', component: NewAnalysisViewComponent, pathMatch: 'full'},
  { path: 'results/:algorithm/:analysis_id', component: FinishedAnalysisViewComponent, pathMatch: 'full'},
  { path: '**', component: PageNotFoundComponent }
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes, {useHash: true});
