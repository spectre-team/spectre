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
import { MessageService } from 'primeng/components/common/messageservice';
import { BlockUI, NgBlockUI } from 'ng-block-ui';

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
  public mzLength: number;
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
  public disabledChanelIdSlider = true;
  public onClickBind: Function;
  @BlockUI() blockUI: NgBlockUI;
  public colors = [ {value:  'RdBu'}, {value: 'Greys'}, {value:  'YlGnBu'} , {value: 'Greens'}, {value:  'YlOrRd'},
    {value:  'Bluered'}, {value:  'Reds'}, {value: 'Blues'}, {value:  'Picnic'}, {value:  'Rainbow'}, {value: 'Portland'},
    {value:  'Jet'}, {value:  'Hot'}, {value:  'Blackbody'}, {value:  'Earth'}, {value: 'Electric'}, {value:  'Viridis'}];
  public selectedValue = 'RdBu';
  public heatmap: Heatmap;

  constructor(
      private route: ActivatedRoute,
      private spectrumService: SpectrumService,
      private heatmapService: HeatmapService,
      private preparationService: PreparationService,
      private messageService: MessageService
  ) { }

  ngOnInit() {
    this.onClickBind = this.onClickFunction.bind(this);
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
    if (this.disabledChanelIdSlider === true) {
      this.showError('Please select spectrum by coordinates');
    } else {
      this.blockUI.start('Getting heatmap...');
      this.heatmapService
        .get(this.id, this.currentChannelId)
        .subscribe(heatmap => {
          this.heatmapData = this.toHeatmapDataset(heatmap);
          this.blockUI.stop();
        });
    }
  }

  changeColor() {
    this.heatmapData = this.toHeatmapDataset(this.heatmap);
  }

  getSpectrum(selectNumber: number) {
    this.blockUI.start('Getting spectrum...');
    this.spectrumService
      .get(this.id, selectNumber)
      .subscribe(spectrum => {
        this.spectrumData = this.toSpectrumDataset(spectrum);
        this.blockUI.stop();
      }, error => {
        this.spectrumData = [{}];
        this.showError('Spectrum not found');
      });
  }

  onClickFunction(event) {
    const point = event.points[0];
    this.xCoordinate = point.x;
    this.yCoordinate = point.y;
    this.getSpectrumByCoordinates();
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
    const y = (this.yCoordinate - this.yHeatmapSize) * (-1) + this.minHeatmapRow;
    this.blockUI.start('Getting spectrum...');
    this.spectrumService
      .getByCoordinates(this.id, x, y)
      .subscribe(spectrum => {
        this.spectrumData = this.toSpectrumDataset(spectrum);
        this.disableChanelIdSlider(false);
        this.showSuccess('Spectrum found');
      }, error => {
        this.spectrumData = [{}];
        this.disableChanelIdSlider(true);
        this.showError('Spectrum not found');
      });
  }

  disableChanelIdSlider(disableFlag: boolean) {
    if (disableFlag) {
      this.mzValue = 0;
      this.currentChannelId = 0;
      this.disabledChanelIdSlider = true;
    } else {
      this.disabledChanelIdSlider = false;
    }
  }

  showError(msg: string) {
    this.blockUI.stop();
    console.log(msg);
    this.messageService.add({severity: 'error', summary: 'Error Message', detail: msg});
  }

  showSuccess(msg: string) {
    this.blockUI.stop();
    this.messageService.add({severity: 'success', summary: 'Success Message', detail: msg});
  }

  selectSpectrum(number: string) {
    const selectNumber = Number.parseInt(number);
    console.log(selectNumber);

    if (isNaN(selectNumber) || selectNumber > this.preparation.spectraNumber || selectNumber < 0) {
      this.showError('Type properly number from 0 to ' + 997);
    } else { this.getSpectrum(selectNumber); }
  }

  toHeatmapDataset(heatmap: Heatmap) {
    this.heatmap = heatmap;
    this.xHeatmapSize = heatmap.maxColumn - heatmap.minColumn;
    this.yHeatmapSize = heatmap.maxRow - heatmap.minRow;
    this.minHeatmapColumn = heatmap.minColumn;
    this.minHeatmapRow = heatmap.minRow;
    return [{
      z: heatmap.data,
      type: 'heatmap',
      colorscale: this.selectedValue
    }];
  }

  toSpectrumDataset(spectrum: Spectrum) {
    this.mzLength = spectrum.mz.length - 1;
    this.mz = spectrum.mz;
    return [{
      x: spectrum.mz,
      y: spectrum.intensities,
      name: `Spectrum ${spectrum.id}, (X=${spectrum.x},Y=${spectrum.y})`
    }];
  }
}
