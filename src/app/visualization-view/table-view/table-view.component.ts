/*
 * table-view.component.ts
 * Component for displaying tabular summary of results.
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

@Component({
  selector: 'app-table-view',
  templateUrl: './table-view.component.html',
  styleUrls: ['./table-view.component.css']
})
export class TableViewComponent<T> implements OnInit {

  _columns: ColumnDefinition[];
  get columns(): ColumnDefinition[] { return this._columns; }
  @Input() set columns(columns: ColumnDefinition[]) {
    this._columns = columns;
    this.columnNames = this.columns.map(columnDefinition => columnDefinition.key);
  }
  @Input() data: T[];
  public columnNames: string[] = [];

  constructor() { }

  ngOnInit() { }

}
