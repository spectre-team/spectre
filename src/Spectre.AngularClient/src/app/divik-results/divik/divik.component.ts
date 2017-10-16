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

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser';

import { Heatmap } from '../../heatmaps/shared/heatmap';
import { DivikService } from '../shared/divik.service';
import { DivikConfig } from '../shared/divik-config';
import 'rxjs/Rx';


@Component({
  selector: 'app-divik',
  templateUrl: './divik.component.html',
  styleUrls: ['./divik.component.css'],
})
export class DivikComponent implements OnInit {
  public data: any;
  public configDescription: string;
  public downloadJsonHref: any;
  public config: Map<string, string> = new Map<string, string>();

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
      .subscribe(config => this.buildConfigInfo(config));
  }

  toHeatmapDataset(heatmap: Heatmap) {
    return [{
      z: heatmap.data,
      type: 'heatmap'
    }];
  }

  buildConfigInfo(config: DivikConfig) {
    Object.keys(config).forEach((key) => {
      this.config.set(key, config[key]);
    });
    this.generateDownloadJsonUri(config);
  }

  generateDownloadJsonUri(config: DivikConfig) {
    const theJSON = JSON.stringify(config);
    this.downloadJsonHref = this.sanitizer.bypassSecurityTrustUrl('data:text/json;charset=UTF-8,' + encodeURIComponent(theJSON));
  }
}
