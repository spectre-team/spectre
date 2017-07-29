/*
 * preparation.component.ts
 * Component of single preparation.
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
import {SpectrumService} from '../../spectrums/shared/spectrum.service';
import {HeatmapService} from '../../heatmaps/shared/heatmap.service';
import {Spectrum} from '../../spectrums/shared/spectrum';
import {Heatmap} from '../../heatmaps/shared/heatmap';
import {PreparationService} from '../shared/preparation.service';
import {Preparation} from '../shared/preparation';
import { MessagesService } from '../../../../node_modules/ng2-messages/ng2-messages';


@Component({
  selector: 'app-preparation',
  templateUrl: './preparation.component.html',
  styleUrls: ['./preparation.component.css'],
  providers: [PreparationService]
})
export class PreparationComponent implements OnInit {
  public id: number;
  public heatmapData: any;
  public spectrumData: any;
  public preparation: Preparation;

  constructor(
      private route: ActivatedRoute,
      private spectrumService: SpectrumService,
      private heatmapService: HeatmapService,
      private preparationService: PreparationService,
      private messagesService: MessagesService
  ) { }

  ngOnInit() {
      this.route.params.subscribe(params => {
        console.log('[PreparationComponent] ngOnInit');
        this.id = Number.parseInt(params['id']);
        console.log(this.id);
        console.log('[PreparationComponent] parsed id');
        this.preparationService
          .getPreparationById(this.id)
          .subscribe(preparation => this.preparation = preparation);
        this.heatmapService
          .get(this.id, 100)
          .subscribe(heatmap => this.heatmapData = this.toHeatmapDataset(heatmap));
        this.getSpectrum(1);
        console.log('[SpectrumComponent] layout setup');
      });
  }

  getSpectrum(selectNumber: number) {
    this.spectrumService
      .get(this.id, selectNumber)
      .subscribe(spectrum => this.spectrumData = this.toSpectrumDataset(spectrum));
  }

  showError(msg: string) {
    this.messagesService.error(msg);
  }

  selectSpectrum(number: string) {
    const selectNumber = Number.parseInt(number);
    console.log(selectNumber);

    if (isNaN(selectNumber) || selectNumber > this.preparation.spectraNumber || selectNumber < 0) {
      this.showError('Type properly number from 0 to ' + 997);
    } else { this.getSpectrum(selectNumber); }
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
}
