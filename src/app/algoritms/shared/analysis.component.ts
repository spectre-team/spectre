/*
 * analysis.component.ts
 * Generic component of single analysis result.
 *
   Copyright 2018 Grzegorz Mrukwa, Sebastian Pustelnik, Daniel Babiak

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

import { Component, Input, OnInit} from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

import { Heatmap } from '../../heatmaps/shared/heatmap';
import 'rxjs/Rx';
import { AnalysisService } from './analysis.service';


@Component({
  selector: 'app-analysis',
  templateUrl: './analysis.component.html',
  styleUrls: ['./analysis.component.css'],
})
export class AnalysisComponent implements OnInit {
  @Input() public preparationId;
  @Input() public analysisType;
  @Input() public analysisId;
  public data: any;
  public analysisConfig: any;
  public analysisConfigKeys: Array<string>;
  public downloadJsonHref: SafeUrl;

  constructor(
      private analysisService: AnalysisService,
      private sanitizer: DomSanitizer,
  ) { }

  ngOnInit() {
    this.analysisService
      .get(this.preparationId, this.analysisType, this.analysisId)
      .subscribe(heatmap => this.data = this.toHeatmapDataset(heatmap));
  }

  toHeatmapDataset(heatmap: Heatmap) {
    return [{
      z: heatmap.data,
      type: 'heatmap'
    }];
  }

  generateDownloadJsonUri() {
    const theJSON = JSON.stringify(this.analysisConfig);
    this.downloadJsonHref = this.sanitizer.bypassSecurityTrustUrl('data:text/json;charset=UTF-8,' + encodeURIComponent(theJSON));
  }
}
