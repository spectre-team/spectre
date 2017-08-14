import { TestBed, inject } from '@angular/core/testing';
import { Http, BaseRequestOptions, Response, ResponseOptions } from '@angular/http';
import { MockBackend, MockConnection } from '@angular/http/testing';

import {DivikService} from './divik.service';

function divikConfig(): string {
  return JSON.stringify({
    MaxK: 10,
    Level: 3,
    UsingLevels: false,
    Amplitude: true,
    Variance: true,
    PercentSizeLimit: .01,
    FeaturePreservationLimit: .05,
    Metric: 'euclidean',
    PlottingPartitions: false,
    PlottingPartitionsRecursively: false,
    PlottingDecomposition: false,
    PlottingDecompositionRecursively: false,
    MaxDecompositionComponents: 7
  });
}

function divikResult(): string {
  return JSON.stringify({
    Id: 1,
    X: [4, 5, 6],
    Y: [5, 8, 9 ],
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
          DivikService
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

    it('parses max k from DiviK config', inject([DivikService, MockBackend],
      (divikService: DivikService, mockBackend: MockBackend) => {
        setResponse(mockBackend, divikConfig());
        divikService.getConfig(1, 1).subscribe((config) => {
          expect(config.maxK).toEqual(10);
        });
      }));

    it('parses level from DiviK config', inject([DivikService, MockBackend],
      (divikService: DivikService, mockBackend: MockBackend) => {
        setResponse(mockBackend, divikConfig());
        divikService.getConfig(1, 1).subscribe((config) => {
          expect(config.level).toEqual(3);
        });
      }));

    it('parses whether level was used in analysis from DiviK config', inject([DivikService, MockBackend],
      (divikService: DivikService, mockBackend: MockBackend) => {
        setResponse(mockBackend, divikConfig());
        divikService.getConfig(1, 1).subscribe((config) => {
          expect(config.usingLevels).toEqual(false);
        });
      }));

    it('parses whether amplitude filtering was enabled from DiviK config', inject([DivikService, MockBackend],
      (divikService: DivikService, mockBackend: MockBackend) => {
        setResponse(mockBackend, divikConfig());
        divikService.getConfig(1, 1).subscribe((config) => {
          expect(config.amplitude).toEqual(true);
        });
      }));

    it('parses whether variance filtering was enabled from DiviK config', inject([DivikService, MockBackend],
      (divikService: DivikService, mockBackend: MockBackend) => {
        setResponse(mockBackend, divikConfig());
        divikService.getConfig(1, 1).subscribe((config) => {
          expect(config.variance).toEqual(true);
        });
      }));

    it('parses percent size limit from DiviK config', inject([DivikService, MockBackend],
      (divikService: DivikService, mockBackend: MockBackend) => {
        setResponse(mockBackend, divikConfig());
        divikService.getConfig(1, 1).subscribe((config) => {
          expect(config.percentSizeLimit).toEqual(.01);
        });
      }));

    it('parses feature preservation limit from DiviK config', inject([DivikService, MockBackend],
      (divikService: DivikService, mockBackend: MockBackend) => {
        setResponse(mockBackend, divikConfig());
        divikService.getConfig(1, 1).subscribe((config) => {
          expect(config.featurePreservationLimit).toEqual(.05);
        });
      }));

    it('parses metric from DiviK config', inject([DivikService, MockBackend],
      (divikService: DivikService, mockBackend: MockBackend) => {
        setResponse(mockBackend, divikConfig());
        divikService.getConfig(1, 1).subscribe((config) => {
          expect(config.metric).toEqual('euclidean');
        });
      }));

    it('parses whether partition was plotted from DiviK config', inject([DivikService, MockBackend],
      (divikService: DivikService, mockBackend: MockBackend) => {
        setResponse(mockBackend, divikConfig());
        divikService.getConfig(1, 1).subscribe((config) => {
          expect(config.plottingPartitions).toEqual(false);
        });
      }));

    it('parses whether partitions were plotted recursively from DiviK config', inject([DivikService, MockBackend],
      (divikService: DivikService, mockBackend: MockBackend) => {
        setResponse(mockBackend, divikConfig());
        divikService.getConfig(1, 1).subscribe((config) => {
          expect(config.plottingPartitionsRecursively).toEqual(false);
        });
      }));

    it('parses whether decomposition was plotted from DiviK config', inject([DivikService, MockBackend],
      (divikService: DivikService, mockBackend: MockBackend) => {
        setResponse(mockBackend, divikConfig());
        divikService.getConfig(1, 1).subscribe((config) => {
          expect(config.plottingDecomposition).toEqual(false);
        });
      }));

    it('parses whether decompositions were plotted recursively from DiviK config', inject([DivikService, MockBackend],
      (divikService: DivikService, mockBackend: MockBackend) => {
        setResponse(mockBackend, divikConfig());
        divikService.getConfig(1, 1).subscribe((config) => {
          expect(config.plottingDecompositionRecursively).toEqual(false);
        });
      }));

    it('parses maximal number of decomposition components from DiviK config', inject([DivikService, MockBackend],
      (divikService: DivikService, mockBackend: MockBackend) => {
        setResponse(mockBackend, divikConfig());
        divikService.getConfig(1, 1).subscribe((config) => {
          expect(config.maxDecompositionComponents).toEqual(7);
        });
      }));
});
