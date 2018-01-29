/*
 * upload.component.ts
 * Component of upload dataset.
 *
 Copyright 2018 Sebastian Pustelnik

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
import {UploadService} from "./shared/upload.service";

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})

export class UploadComponent implements OnInit {

  public message: string;
  public linkInput: string;
  public nameInput: string;

  constructor(
    private uploadService: UploadService
  ) { }

  ngOnInit() {
  }

  uploadData() {
    this.uploadService.uploadData(this.linkInput, this.nameInput)
      .subscribe(message => this.message = message,
          error => this.message = error);
  }
}
