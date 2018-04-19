/*
 * new-analysis-view.component.ts
 * Component with form for scheduling new analysis.
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

import { MatSnackBar } from '@angular/material';

import { AnalysisSchedulerService } from '../analysis-scheduler.service';

@Component({
  selector: 'app-new-analysis-view',
  templateUrl: './new-analysis-view.component.html',
  styleUrls: ['./new-analysis-view.component.css']
})
export class NewAnalysisViewComponent implements OnInit {

  private algorithm: string;
  private layoutUrl: string;
  private schemaUrl: string;
  private snackBarConfig = {duration: 3000};

  constructor(
    private scheduler: AnalysisSchedulerService,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar,
  ) { }

  ngOnInit() {
    this.route.paramMap.subscribe(
      (params: ParamMap) => {
        this.algorithm = params.get('algorithm');
        this.layoutUrl = this.scheduler.formLayoutUrl(this.algorithm);
        this.schemaUrl = this.scheduler.formSchemaUrl(this.algorithm);
      }
    )
  }

  submit(algorithmParameters: any): void {
    this.scheduler.enqueue(this.algorithm, algorithmParameters).subscribe({
      next: response => this.notify('Analysis scheduled successfully'),
      error: message => this.notify('Error scheduling your analysis')});
  }

  private notify(message): void {
    this.snackBar.open(message, '', this.snackBarConfig);
  }

}
