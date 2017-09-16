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
import { SpectrumService} from '../../spectra/shared/spectrum.service';
import { HeatmapService} from '../../heatmaps/shared/heatmap.service';
import { Spectrum } from '../../spectra/shared/spectrum';
import { Heatmap } from '../../heatmaps/shared/heatmap';
import { PreparationService } from '../shared/preparation.service';
import { Preparation } from '../shared/preparation';
import {MessageService} from 'primeng/components/common/messageservice';


@Component({
  selector: 'app-preparation',
  templateUrl: './preparation.component.html',
  styleUrls: ['./preparation.component.css'],
  providers: [PreparationService, MessageService]
})
export class PreparationComponent implements OnInit {
  public id: number;
  public heatmapData: any;
  public spectrumData: any;
  public mzLenth: number;
  public mzValue: number;
  public currentChannelId = 0;
  public mz = [];
  public preparation: Preparation;
  public xCoordinate = 0;
  public yCoordinate = 0;
  public xHeatmapSize: number;
  public yHeatmapSize: number;
  public minHeatmapColumn: number;
  public minHeatmapRow: number;

  constructor(
      private route: ActivatedRoute,
      private spectrumService: SpectrumService,
      private heatmapService: HeatmapService,
      private preparationService: PreparationService,
      private messageService: MessageService
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
        console.log('[SpectrumComponent] layout setup');
      });
  }

  onInputChannelId(event: any) {
    this.mzValue = this.mz[event.value];
  }

  onChangedChannelId(event: any) {
    this.heatmapService
      .get(this.id, this.currentChannelId)
      .subscribe(heatmap => this.heatmapData = this.toHeatmapDataset(heatmap));
  }

  onChangedXCoordinate(event: any) {
    this.xCoordinate = event.value;
    this.getSpectrumByCoordinates();
  }

  onChangedYCoordinate(event: any) {
    this.yCoordinate = event.value;
    this.getSpectrumByCoordinates();
  }

  getSpectrum(selectNumber: number) {
    this.spectrumService
      .get(this.id, selectNumber)
      .subscribe(spectrum => this.spectrumData = this.toSpectrumDataset(spectrum));
  }

  /*
   * Parameters:
   * id: preparation id
   * x: x coordinate as a sum of selected value from x slider and minimum heatmap column value returned from server
   * y: y coordinate as additive inverse of subtraction between selected value from y slider and row size plus minimum
   * heatmap row returned from server
   */
  getSpectrumByCoordinates() {
    const x = this.xCoordinate + this.minHeatmapColumn;
    const y = (this.yCoordinate - this.yHeatmapSize)*(-1) + this.minHeatmapRow;
    this.spectrumService
      .getByCoordinates(this.id, x, y)
      .subscribe(spectrum => {
        this.spectrumData = this.toSpectrumDataset(spectrum);
        this.showSuccess("Spectrum found");
      }, error => {
        this.spectrumData = [{}];
        this.showError("Spectrum not found");
      });
  }

  showError(msg: string) {
    console.log(msg);
    this.messageService.add({severity:'error', summary:'Error Message', detail:msg});
  }

  showSuccess(msg: string){
    this.messageService.add({severity:'success', summary:'Success Message', detail:msg});
  }

  selectSpectrum(number: string) {
    const selectNumber = Number.parseInt(number);
    console.log(selectNumber);

    if (isNaN(selectNumber) || selectNumber > this.preparation.spectraNumber || selectNumber < 0) {
      this.showError('Type properly number from 0 to ' + 997);
    } else { this.getSpectrum(selectNumber); }
  }

  toHeatmapDataset(heatmap: Heatmap) {
    this.xHeatmapSize = heatmap.maxColumn - heatmap.minColumn;
    this.yHeatmapSize = heatmap.maxRow - heatmap.minRow;
    this.minHeatmapColumn = heatmap.minColumn;
    this.minHeatmapRow = heatmap.minRow;
    return [{
      z: heatmap.data,
      type: 'heatmap'
    }];
  }

  toSpectrumDataset(spectrum: Spectrum) {
    this.mzLenth = spectrum.mz.length - 1;
    this.mz = spectrum.mz;
    return [{
      x: spectrum.mz,
      y: spectrum.intensities,
      name: `Spectrum ${spectrum.id}, (X=${spectrum.x},Y=${spectrum.y})`
    }];
  }
}
