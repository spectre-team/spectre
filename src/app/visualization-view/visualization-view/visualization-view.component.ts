/*
 * visualization-view.component.ts
 * Component for flexible data visualization.
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

import { ColumnDefinition } from '../column-definition';
import { VisualizationType } from '../visualization-type.enum';

@Component({
  selector: 'app-visualization-view',
  templateUrl: './visualization-view.component.html',
  styleUrls: ['./visualization-view.component.css']
})
export class VisualizationViewComponent implements OnInit {

  visualizationType = VisualizationType;
  @Input() type: VisualizationType;
  @Input() data: {columns: ColumnDefinition[], data} | {layout, data};

  constructor() { }

  ngOnInit() {
  }

}
