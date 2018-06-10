/*
 * aspect-view.component.ts
 * Component for visualization of single aspect of the result.
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

import { Component, Input, OnInit } from '@angular/core';

import { apiUrl } from '../../../environments/apiUrl';
import { AspectDescription } from '../aspect-description';
import { ResultDownloadService } from '../result-download.service';

@Component({
  selector: 'app-aspect-view',
  templateUrl: './aspect-view.component.html',
  styleUrls: ['./aspect-view.component.css']
})
export class AspectViewComponent implements OnInit {

  @Input() algorithm: string;
  @Input() id: string;
  @Input() description: AspectDescription;
  public schemaUrl: string;
  public layoutUrl: string;
  public result: any;

  constructor(
    private resultsService: ResultDownloadService,
  ) { }

  ngOnInit() {
    if (this.description !== undefined) {
      const aspect = this.description.aspect;
      this.schemaUrl = apiUrl.aspectSchemaUrl(this.algorithm, aspect);
      this.layoutUrl = apiUrl.aspectLayoutUrl(this.algorithm, aspect);
    }
  }

  public submit(query: any) {
    this.resultsService
      .getResult(this.algorithm, this.id, this.description.aspect, query)
      .subscribe(data => this.result = data);
  }

}
