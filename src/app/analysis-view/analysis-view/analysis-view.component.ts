/*
 * analysis-view.component.ts
 * View of all finished analyses of particular type with "new" button.
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

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { Analysis } from '../analysis';
import { FinishedAnalysesService } from '../finished-analyses.service';

@Component({
  selector: 'app-analysis-view',
  templateUrl: './analysis-view.component.html',
  styleUrls: ['./analysis-view.component.css']
})
export class AnalysisViewComponent implements OnInit {

  public algorithmName: string;
  public errorMessage: string;
  public analyses: Analysis[];

  constructor(
    private route: ActivatedRoute,
    private client: FinishedAnalysesService,
  ) { }

  ngOnInit() {
    this.route.paramMap.subscribe(
      (params: ParamMap) => {
        this.algorithmName = params.get('algorithmName');
        this.client.getFinished(this.algorithmName).subscribe(
          analyses => this.analyses = analyses,
          error => this.errorMessage = error.message);
      });
  }

}
