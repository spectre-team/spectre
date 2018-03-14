import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-algorithm-expansion-panel',
  templateUrl: './algorithm-expansion-panel.component.html',
  styleUrls: ['./algorithm-expansion-panel.component.css']
})
export class AlgorithmExpansionPanelComponent implements OnInit {

  panelOpenState: boolean = false;

  constructor() { }

  ngOnInit() {
  }

}
