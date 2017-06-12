import { Component, OnInit, Input } from '@angular/core';

declare var Plotly: any;

@Component({
  selector: 'app-plotly',
  templateUrl: './plotly.component.html',
  styleUrls: ['./plotly.component.css']
})
export class PlotlyComponent implements OnInit {

  @Input() data: any;
  @Input() layout: any;
  @Input() options: any;
  @Input() displayRawData: boolean;

  constructor() { }

  ngOnInit() {
    Plotly.plot('plotlyDiv',  [{
    x: [1, 2, 3, 4, 5],
    y: [1, 2, 4, 8, 16] }], {
    margin: { t: 0 } } );
  }

}
