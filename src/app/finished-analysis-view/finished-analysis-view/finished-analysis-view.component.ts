/*
 * finished-analysis-view.component.ts
 * Component for visualization of all aspects of the result.
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

import {
  Component,
  OnInit,
} from '@angular/core';

import { QuerySchemaDownloadService } from '../query-schema-download.service';
import { AspectDescription } from '../../aspect-view/aspect-description';
import {
  ActivatedRoute,
  ParamMap,
} from '@angular/router';

@Component({
  selector: 'app-finished-analysis-view',
  templateUrl: './finished-analysis-view.component.html',
  styleUrls: ['./finished-analysis-view.component.css']
})
export class FinishedAnalysisViewComponent implements OnInit {

  public aspects: AspectDescription[] = [];
  public id: string;
  public algorithm: string;

  constructor(
    private schemaService: QuerySchemaDownloadService,
    private route: ActivatedRoute,
    ) { }

  ngOnInit() {
    this.route.paramMap.subscribe((params: ParamMap) => {
      this.id = params.get('analysis_id');
      this.algorithm = params.get('algorithm');
      this.schemaService.getAspects(this.algorithm).subscribe(aspects => this.aspects = aspects);
    });
  }

}
