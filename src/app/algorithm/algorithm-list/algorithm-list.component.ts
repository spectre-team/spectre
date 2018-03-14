import { Component, OnInit } from '@angular/core';
import {AlgorithmsListService} from "./algorithms-list.service";

@Component({
  selector: 'app-algorithm-list',
  templateUrl: './algorithm-list.component.html',
  styleUrls: ['./algorithm-list.component.css'],
  providers: [AlgorithmsListService]
})
export class AlgorithmListComponent implements OnInit {
  temp: string;
  algorithms: IAlgorithm;
  constructor(private _algorithmListService: AlgorithmsListService) { }

  ngOnInit() {
    this.getAlgorithms();
    if (this.algorithms !== undefined) {
      console.log('Name: ', this.algorithms.name);
    }

    // let jsonObj: any = JSON.parse(this.temp);
    // this.algorithms = <IAlgorithm>jsonObj;
    // console.log(this.algorithms.name);
  }

  getAlgorithms() {
    this._algorithmListService.getAlgorithms().subscribe(
      data => {this.temp = <string>data},
      err => console.error(err),
      () => console.log('done loading')
    );
  }
}

export interface IAlgorithm {
  id: string;
  name: string;
}
