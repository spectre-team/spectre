/*
 * short-summary.component.ts
 * Component of a short-summary of divik.
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

import {Component, OnInit, Input, ViewChild} from '@angular/core';
import {PreparationService} from "../preparations/shared/preparation.service";
import {Preparation} from "../preparations/shared/preparation";
import {IAnalysisData} from "../analysis-form/IAnalysisData";
import {AnalysisFormComponent} from "../analysis-form/analysis-form.component";

@Component({
  selector: 'app-flow-layout',
  templateUrl: './flow-layout.component.html',
  styleUrls: ['./flow-layout.component.css'],
  providers: [PreparationService],
})
export class FlowLayoutComponent implements OnInit {
  preparations: Preparation[] = [];
  ChosenPreparation: string;

  @ViewChild(AnalysisFormComponent)
  public AnalysisComponent: AnalysisFormComponent;

  constructor(private _preparationService: PreparationService) {
  }

  ngOnInit() {
    this._preparationService
      .getAll()
      .subscribe(p => this.preparations = p);
    this.AnalysisComponent.AnalysisData.DatasetName = this.ChosenPreparation;
  }

  public test(){
    console.log('name', this.AnalysisComponent.AnalysisData.DatasetName);
    console.log('AnalysisName', this.AnalysisComponent.AnalysisData.AnalysisName);
    console.log('MaxK', this.AnalysisComponent.AnalysisData.MaxK);
    console.log('Level', this.AnalysisComponent.AnalysisData.Level);
    console.log('UseLevels', this.AnalysisComponent.AnalysisData.UseLevels);
    console.log('PercentSizeLimit', this.AnalysisComponent.AnalysisData.PercentSizeLimit);
    console.log('FeaturePreservationLimit', this.AnalysisComponent.AnalysisData.FeaturePreservationLimit);
    console.log('Metric', this.AnalysisComponent.AnalysisData.Metric);
    console.log('MaxComponentsForDecomposition', this.AnalysisComponent.AnalysisData.MaxComponentsForDecomposition);
    console.log('KMeansMaxIters', this.AnalysisComponent.AnalysisData.KmeansMaxIters);
  }
}
