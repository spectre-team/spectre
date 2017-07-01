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
import {HeatmapService} from './heatmap.service';
import {Heatmap} from './heatmap';



@Component({
  selector: 'app-heatmap',
  templateUrl: './heatmap.component.html',
  styleUrls: ['./heatmap.component.css']
})
export class HeatmapComponent implements OnInit {

  @Input() public id: number;
  public HeatmapLayout: any;
  public HeatmapData: any;
  public Options: any;

  constructor(
    private heatmapService: HeatmapService
  ) { }

  ngOnInit() {
      this.heatmapService
        .get(this.id, 2)
        .subscribe(heatmap => this.HeatmapData = this.toHeatmapDataset(heatmap));
      console.log('[HeatmapComponent] layout setup');
      this.HeatmapLayout = this.defaultHeatmapLayout();
      this.HeatmapData = this.defaultData();
      this.Options = [];
      console.log('[HeatmapComponent] plot layout set');
  }

  defaultHeatmapLayout() {
    return {
      height: 600,
      width: 600,
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
        showticklabels: false
      }
    };
  }

  toHeatmapDataset(heatmap: Heatmap) {
    return [{
      z: heatmap.data,
      type: 'heatmap'
    }];
  }

  defaultData() {
    return [{}];
  }
}
