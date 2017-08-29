/*
 * divik.component.ts
 * Component of single divik result.
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

import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import { DivikService } from '../shared/divik.service';
import { Heatmap } from '../../heatmaps/shared/heatmap';
import { DivikConfig } from '../shared/divik-config';
import { DomSanitizer } from '@angular/platform-browser';


@Component({
  selector: 'app-divik',
  templateUrl: './divik.component.html',
  styleUrls: ['./divik.component.css'],
})
export class DivikComponent implements OnInit {
  public data: any;
  public configDescription: string;
  public downloadJsonHref: any;
  constructor(
      private route: ActivatedRoute,
      private divikService: DivikService,
      private sanitizer: DomSanitizer
  ) { }

  ngOnInit() {
    this.divikService
      .get(1, 1, 1)
      .subscribe(heatmap => this.data = this.toHeatmapDataset(heatmap));
    this.divikService
      .getConfig(1, 1)
      .subscribe(config => this.configDescription = this.displayConfig(config));
  }

  toHeatmapDataset(heatmap: Heatmap) {
    return [{
      z: heatmap.data,
      type: 'heatmap'
    }];
  }
  displayConfig(config: DivikConfig): string {
    let description = '';
    Object.keys(config.properties).forEach((key) => {
      description += key + ': ' +  config.properties[key] + '\n';
    });
    this.generateDownloadJsonUri(config);
    return description;
  }

  generateDownloadJsonUri(config: DivikConfig) {
    const theJSON = JSON.stringify(config.properties);
    this.downloadJsonHref = this.sanitizer.bypassSecurityTrustUrl('data:text/json;charset=UTF-8,' + encodeURIComponent(theJSON));
  }
}
