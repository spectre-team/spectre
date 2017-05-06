import { Component, OnInit } from '@angular/core';
import { Preparation } from './preparation';
import { PreparationsService } from './preparation.service';

@Component({
  selector: 'app-preparation-list',
  templateUrl: './preparation-list.component.html',
  styleUrls: ['./preparation-list.component.css'],
  providers: [PreparationsService]
})
export class PreparationListComponent implements OnInit {
  preparations: Preparation[] = [];
  constructor(private _preparationsService: PreparationsService) {
  }


  ngOnInit() {
    console.log('onInit');
    this._preparationsService
      .getAll()
      .subscribe(p => this.preparations = p);
  }

}
