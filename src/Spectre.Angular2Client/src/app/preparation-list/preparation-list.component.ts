import { Component, OnInit } from '@angular/core';
import { Preparation } from './preparation';
import { PreparationService } from './preparation.service';

@Component({
  selector: 'app-preparation-list',
  templateUrl: './preparation-list.component.html',
  styleUrls: ['./preparation-list.component.css'],
  providers: [PreparationService]
})
export class PreparationListComponent implements OnInit {
  preparations: Preparation[] = [];
  constructor(private _preparationService: PreparationService) {
  }


  ngOnInit() {
    this._preparationService
      .getAll()
      .subscribe(p => this.preparations = p);
  }

}
