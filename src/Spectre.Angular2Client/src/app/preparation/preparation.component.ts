/*
 * preparation.component.ts
 * Component of single preparation.
 *
   Copyright 2017 Grzegorz Mrukwa

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
import { ActivatedRoute, Params } from '@angular/router';

import { PreparationRoutingModule } from './preparation-routing.module';
import { SpectrumService } from './spectrum.service';
import { Spectrum } from './spectrum';
import { HeatmapService } from './heatmap.service';
import { Heatmap } from './heatmap';

@Component({
  selector: 'app-preparation',
  templateUrl: './preparation.component.html',
  styleUrls: ['./preparation.component.css']
})
export class PreparationComponent implements OnInit {

  id: number = null;
  public SpectrumLayout: any;
  public SpectrumData: any;
  public HeatmapLayout: any;
  public HeatmapData: any;
  public Options: any;

  constructor(
      private route: ActivatedRoute,
      private spectrumService: SpectrumService,
      private heatmapService: HeatmapService
  ) { }

  ngOnInit() {
      this.route.params.subscribe(params => {
        console.log('[PreparationComponent] ngOnInit');
        this.id = Number.parseInt(params['id']);
        console.log('[PreparationComponent] parsed id');
        this.spectrumService
          .get(this.id, 1)
          .subscribe(spectrum => this.SpectrumData = this.toSpectrumDataset(spectrum));
        console.log('[PreparationComponent] layout setup');
        this.SpectrumLayout = this.defaultSpectrumLayout();
        this.SpectrumData = this.defaultData();
        this.HeatmapLayout = this.defaultHeatmapLayout();
        this.HeatmapData = this.defaultData();
        this.Options = [];
        console.log('[PreparationComponent] plot layout set');
        this.heatmapService
          .get(this.id, 100)
          .subscribe(heatmap => this.HeatmapData = this.toHeatmapDataset(heatmap));
      });
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

  toSpectrumDataset(spectrum: Spectrum) {
      return [{
              x: spectrum.mz,
              y: spectrum.intensities,
              name: `Spectrum ${spectrum.id}, (X=${spectrum.x},Y=${spectrum.y})`
          }];
  }

  defaultSpectrumLayout() {
    return {
      height: 500,
      width: 1200
    };
  }

  defaultData() {
      return [{
              x: [1, 2, 3, 4],
              y: [1, 4, 9, 16],
              name: `Sample data`
      }];
  }



}
