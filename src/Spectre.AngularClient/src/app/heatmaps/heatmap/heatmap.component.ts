/*
 * heatmap.component.ts
 * Component of single heatmap.
 *
 Copyright 2017 Grzegorz Mrukwa, Sebastian Pustelnik, Daniel Babiak

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
import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-heatmap',
  templateUrl: './heatmap.component.html',
  styleUrls: ['./heatmap.component.css']
})
export class HeatmapComponent implements OnInit {
  @Input() public heatmapData: any;
  @Input() public height: number;
  @Input() public width: number;
  @Input() public onClickFunction: Function;
  public heatmapLayout: any;
  public options: any;

  constructor() { }

  ngOnInit() {
      this.heatmapLayout = this.defaultHeatmapLayout();
      this.heatmapData = this.defaultData();
      this.options = [];
      console.log('[HeatmapComponent] plot layout set');
  }

  defaultHeatmapLayout() {
    return {
      height: 800,
      width: 800 / 109 * 44,
      xaxis: {
        autotick: false,
        dtick: 1,
        ticklen: 1,
        tickwidth: 1,
        showticklabels: false
      },
      yaxis: {
        autotick: false,
        dtick: 1,
        ticklen: 1,
        tickwidth: 1,
        showticklabels: false,
      }
    };
  }

  defaultData() {
    return [{}];
  }
}
