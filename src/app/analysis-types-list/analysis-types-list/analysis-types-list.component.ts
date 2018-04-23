/*
 * analysis-types-list.component.ts
 * Component for analysis-types-list.
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

import {
  Component,
  OnInit,
} from '@angular/core';
import { AnalysisTypesListService } from '../analysis-types-list.service';

@Component({
  selector: 'app-analysis-types-list',
  templateUrl: './analysis-types-list.component.html',
  styleUrls: ['./analysis-types-list.component.css'],
})
export class AnalysisTypesListComponent implements OnInit {

  panelOpenState: Boolean[] = [false, false, false];
  algorithmsUrl: string;
  algorithms: any;
  types: string[];
  isTableEmpty = false;
  displayMode = "flat";

  constructor(private fetchService: AnalysisTypesListService) {
  }

  ngOnInit() {
    this.algorithmsUrl = this.fetchService.getUrl();
    this.fetchService.getAlgorithms(this.algorithmsUrl).subscribe(
      data => {
        this.algorithms = data;
        this.types = Object.keys(this.algorithms);
        console.log(this.algorithms);
        console.log(this.types);
        console.log(this.algorithms[this.types[0]]);
        if (this.types.length > 0) {
          this.isTableEmpty = true;
        }
      },
    );
  }
}
