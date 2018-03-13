import { environment } from './environment';

export const apiUrl = {
  spectrumByIdUrl(preparationId: number, spectrumId: number) {
    return `${environment.apiPreparationUrl}/spectrum/${preparationId}?spectrumId=${spectrumId}`;
  },
  spectrumByCoordUrl(preparationId: number, x: number, y: number) {
    return `${environment.apiPreparationUrl}/spectrum/${preparationId}?x=${x}&y=${y}`;
  } ,
  preparationsUrl() {
    return `${environment.apiPreparationUrl}/preparations`;
  },
  preparationUrl(preparationId: number) {
    return `${environment.apiPreparationUrl}/preparations/${preparationId}`;
  },
  heatmapUrl(preparationId: number, channelId: number){
    return `${environment.apiPreparationUrl}/heatmap/${preparationId}?channelId=${channelId}&flag=false`;
  },
  divikResultUrl(preparationId: number, divikId: number, level: number) {
    return `${environment.apiDivikUrl}/divikResult/${preparationId}?divikId=${divikId}&level=${level}`;
  },
  divikConfigUrl(preparationId: number, divikId: number) {
    return `${environment.apiDivikUrl}/divikResult/${preparationId}?divikId=${divikId}`;
  },
  uploadUrl(){
    return `${environment.apiUploadUrl}/download`;
  },
  finishedAnalysesUrl: '/results/{0}/',
  inputsSchemaUrl: '/schema/inputs/{0}/',
  inputsLayoutUrl: '/layout/inputs/{0}/',
  scheduleUrl: '/schedule/{0}/',
};
