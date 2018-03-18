/*
* analysis-names-list.component.ts
* Component for analysis names list.
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

import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-analysis-names-list',
  templateUrl: './analysis-names-list.component.html',
  styleUrls: ['./analysis-names-list.component.css']
})
export class AnalysisNamesListComponent implements OnInit {

  @Input() analysisType: string;
  @Input() analysisNames: string[];

  constructor() { }

  ngOnInit() {
  }

}
