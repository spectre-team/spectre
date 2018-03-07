export const apiUrl = {
  spectrumByIdUrl(preparationId: number, spectrumId: number) {
    return `/spectrum/${preparationId}?spectrumId=${spectrumId}`;
  },
  spectrumByCoordUrl(preparationId: number, x: number, y: number) {
    return `/spectrum/${preparationId}?x=${x}&y=${y}`;
  } ,
  preparationsUrl() {
    return `/preparations`;
  },
  preparationUrl(preparationId: number) {
    return `/preparations/${preparationId}`;
  },
  heatmapUrl(preparationId: number, channelId: number){
    return `/heatmap/${preparationId}?channelId=${channelId}&flag=false`;
  },
  divikResultUrl(preparationId: number, divikId: number, level: number) {
    return `/divikResult/${preparationId}?divikId=${divikId}&level=${level}`;
  },
  divikConfigUrl(preparationId: number, divikId: number) {
    return `/divikResult/${preparationId}?divikId=${divikId}`;
  },
  uploadUrl(){
    return `/download`;
  },
  finishedAnalysesUrl: '/results/{0}/',
  inputsSchemaUrl: '/schema/inputs/{0}/',
  inputsLayoutUrl: '/layout/inputs/{0}/',
  scheduleUrl: '/schedule/{0}/',
};
