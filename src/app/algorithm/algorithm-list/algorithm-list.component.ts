import { Component, OnInit } from '@angular/core';
import {AlgorithmsListService} from "./algorithms-list.service";

@Component({
  selector: 'app-algorithm-list',
  templateUrl: './algorithm-list.component.html',
  styleUrls: ['./algorithm-list.component.css'],
  providers: [AlgorithmsListService]
})
export class AlgorithmListComponent implements OnInit {
  algorithms: any;
  constructor(private _algorithmListService: AlgorithmsListService) { }

  ngOnInit() {
    this.getAlgorithms();
  }

  getAlgorithms() {
    this._algorithmListService.getAlgorithms().subscribe(
      data => {
        this.algorithms = data;
        console.log(this.algorithms);
      },
      err => console.error(err),
      () => console.log('done loading')
    );
  }
}
