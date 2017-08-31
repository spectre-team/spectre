import { TestBed, inject } from '@angular/core/testing';
import { Http, BaseRequestOptions, Response, ResponseOptions } from '@angular/http';
import { MockBackend, MockConnection } from '@angular/http/testing';

import { DivikService } from './divik.service';
import { HeatmapUtil } from '../../heatmaps/shared/heatmap-util';

function divikConfig(): string {
  return JSON.stringify({
    MaxK: 10,
    Level: 3,
    UsingLevels: false,
    UsingAmplitudeFiltration: true,
    UsingVarianceFiltration: true,
    PercentSizeLimit: .01,
    FeaturePreservationLimit: .05,
    Metric: 'euclidean',
    PlottingPartitions: false,
    PlottingRecursively: false,
    PlottingDecomposition: false,
    PlottingDecompositionRecursively: false,
    MaxComponentsForDecomposition: 7
  });
}

function divikResult(): string {
  return JSON.stringify({
    Id: 1,
    X: [4, 5, 6],
    Y: [5, 8, 9],
    Data: [1.11, 4.44, 9.99]
  });
}

function setResponse(backend: MockBackend, response: string) {
  backend.connections.subscribe((connection: MockConnection) => {
    const options = new ResponseOptions({
      body: response
    });
    connection.mockRespond(new Response(options));
  });
}

describe('DivikService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        MockBackend,
        BaseRequestOptions,
        {
          provide: Http,
          useFactory: (backendInstance: MockBackend, defaultOptions: BaseRequestOptions) => {
            return new Http(backendInstance, defaultOptions);
          },
          deps: [MockBackend, BaseRequestOptions]
        },
        DivikService,
        HeatmapUtil
      ]
    });
  });

  it('should inject service', inject([DivikService], (service: DivikService) => {
    expect(service).toBeTruthy();
  }));

  it('should parse preparation 1 first split data', inject([DivikService, MockBackend],
    (divikService: DivikService, mockBackend: MockBackend) => {
      setResponse(mockBackend, divikResult());

      divikService.get(1, 1, 1).subscribe((divik) => {
        expect(divik.data.length).toEqual(5);
        expect(divik.data[0].length).toEqual(3);
        expect(divik.data[1].length).toEqual(3);
        expect(divik.data[2].length).toEqual(3);
        expect(divik.data[3].length).toEqual(3);
        expect(divik.data[4].length).toEqual(3);
        expect(divik.data[1][1]).toEqual(4.44);
        expect(divik.data[4][0]).toEqual(1.11);
        expect(divik.data[0][2]).toEqual(9.99);
      });
    }));

  [
    {name: 'parses max k from DiviK config', value: 'Max K', expectedValue: 10},
    {name: 'parses level from DiviK config', value: 'Level', expectedValue: 3},
    {name: 'parses whether level was used in analysis from DiviK config', value: 'Using levels', expectedValue: false},
    {name: 'parses whether amplitude filtering was enabled from DiviK config', value: 'Amplitude', expectedValue: true},
    {name: 'parses whether variance filtering was enabled from DiviK config', value: 'Variance', expectedValue: true},
    {name: 'parses percent size limit from DiviK config', value: 'Percent size limit', expectedValue: .01},
    {name: 'parses feature preservation limit from DiviK config', value: 'Feature preservation limit', expectedValue: .05},
    {name: 'parses metric from DiviK config', value: 'Metric', expectedValue: 'euclidean'},
    {name: 'parses whether partition was plotted from DiviK config', value: 'Plotting partitions', expectedValue: false},
    {name: 'parses whether partitions were plotted recursively from DiviK config', value: 'Plotting recursively', expectedValue: false},
    {name: 'parses whether decomposition was plotted from DiviK config', value: 'Plotting decomposition', expectedValue: false},
    {name: 'parses whether decompositions were plotted recursively from DiviK config', value: 'Plotting decomposition recursively', expectedValue: false},
    {name: 'parses maximal number of decomposition components from DiviK config', value: 'Max decomposition components', expectedValue: 7}
  ]
    .forEach(function (test) {
      it(test.name, inject([DivikService, MockBackend],
        (divikService: DivikService, mockBackend: MockBackend) => {
          setResponse(mockBackend, divikConfig());
          divikService.getConfig(1, 1).subscribe((config) => {
            console.log(config.properties);
            expect(config.properties[test.value]).toEqual(test.expectedValue);
          });
        }));
    });
});
