import {environment} from './environment';

export const apiUrl = {
  spectrumByIdUrl(preparationId: number, spectrumId: number) {
    return `${environment.apiPreparationUrl}/spectrum/${preparationId}?spectrumId=${spectrumId}`;
  },
  spectrumByCoordUrl(preparationId: number, x: number, y: number) {
    return `${environment.apiPreparationUrl}/spectrum/${preparationId}?x=${x}&y=${y}`;
  },
  preparationsUrl() {
    return `${environment.apiPreparationUrl}/preparations`;
  },
  preparationUrl(preparationId: number) {
    return `${environment.apiPreparationUrl}/preparations/${preparationId}`;
  },
  heatmapUrl(preparationId: number, channelId: number) {
    return `${environment.apiPreparationUrl}/heatmap/${preparationId}?channelId=${channelId}&flag=false`;
  },
  divikResultUrl(preparationId: number, divikId: number, level: number) {
    return `${environment.apiDivikUrl}/divikResult/${preparationId}?divikId=${divikId}&level=${level}`;
  },
  divikConfigUrl(preparationId: number, divikId: number) {
    return `${environment.apiDivikUrl}/divikResult/${preparationId}?divikId=${divikId}`;
  },
  uploadUrl() {
    return `${environment.apiUploadUrl}/download`;
  },
  finishedAnalysesUrl(algorithm: string) {
    return `${environment.analysisApiUrl}/results/${algorithm}/`;
  },
  aspectResultUrl(algorithm: string, id: string, aspect: string) {
    return `${environment.analysisApiUrl}/results/${algorithm}/${id}/${aspect}/`;
  },
  aspectDescriptionUrl(algorithm: string) {
    return `${environment.analysisApiUrl}/schema/outputs/${algorithm}/`;
  },
  aspectSchemaUrl(algorithm: string, aspect: string) {
    return `${environment.analysisApiUrl}/schema/outputs/${algorithm}/${aspect}/`;
  },
  aspectLayoutUrl(algorithm: string, aspect: string) {
    return `${environment.analysisApiUrl}/layout/outputs/${algorithm}/${aspect}/`;
  },
  inputsSchemaUrl(algorithm: string) {
    return `${environment.analysisApiUrl}/schema/inputs/${algorithm}/`;
  },
  inputsLayoutUrl(algorithm: string) {
    return `${environment.analysisApiUrl}/layout/inputs/${algorithm}/`;
  },
  outputsSchemaUrl(algorithm: string) {
    return `${environment.analysisApiUrl}/schema/outputs/${algorithm}/`;
  },
  outputsLayoutUrl(algorithm: string) {
    return `${environment.analysisApiUrl}/layout/outputs/${algorithm}/`;
  },
  scheduleUrl(algorithm: string) {
    return `${environment.analysisApiUrl}/schedule/${algorithm}/`;
  },
  algorithmsUrl() {
    return `${environment.analysisApiUrl}/algorithms/`;
  }
};
