%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
%                           ms_gmm_params
%
%
%  set all paramters for ms_gmm
%
%
function param=ms_gmm_params(K)
param_v=zeros(100,1);

% DRAW - show plots of decompositions during computations
% used in many finctions
DRAW=0;

% Res_Par - used in the main body of ms_gmm - multiplied by estimated
% average width of the peak in the spectrum defines resolutioon of the 
% decomposition
Res_Par=0.5;

% Par_P_Sens parameter for peak detection sensitivity
% used in find_split_peaks split peaks must be of height 
% >= Par_P_Sens * maximal peak height
Par_P_Sens=0;

% Par_Q_Thr parameter for peak quality threshold
% used in find_split_peaks
% split peaks must have quality >= Par_Q_Thr
Par_Q_Thr=0.5;

% Par_Ini_J parameter for initial jump
% used in find_split_peaks
Par_Ini_J=5;

% Par_P_L_R parameter for range for peak lookup
% used in find_split_peaks
Par_P_L_R=4;

% Par_P_J parameter for jump
% used in find_split_peaks
Par_P_J=4;

% QFPAR - parameter used in the dynamic programming quality funtion
% 
QFPAR=0.5;

% Res_Par_2 - used in the EM iterations to define lower bounds for standard
% deviations
Res_Par_2=0.1;

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%% TUNABLE 
% Prec_Par_1 - precision parameter - weight used to pick best gmm decomposition
% penalty coefficient for number of components in the quality funtio
Prec_Par_1=0.0001;
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


% Penet_Par_1  - penetration parameter 1 used to continue searching for
% best number of components in gmm decomposition (bigger for splitting
% segments)
Penet_Par_1=15;

% Prec_Par_2 - precision parameter 2 - weight used to pick best gmm decomposition
Prec_Par_2=0.001;

% Penet_Par_1  - penetration parameter 2 used to continue searching for
% best number of components in gmm decomposition (smaller for segments)
Penet_Par_2=30;

% Buf_size_split_Par - size of the buffer for computing GMM paramters of
% splitters
Buf_size_split_Par=10;

% Buf_size_seg_Par - size of the buffer for computing GMM paramters of
% segments
Buf_size_seg_Par=30;

% eps_Par - parameter for EM iterations - tolerance for mixture parameters
% change
eps_Par=0.0001;

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% 
param_v(1)=DRAW;
param_v(2)=Res_Par;
param_v(3)=Par_P_Sens;
param_v(4)=Par_Q_Thr;
param_v(5)=Par_Ini_J;
param_v(6)=Par_P_L_R;
param_v(7)=Par_P_J;
param_v(8)=QFPAR;
param_v(9)=Res_Par_2;
param_v(10)=Prec_Par_1;
param_v(11)=Penet_Par_1;
param_v(12)=Prec_Par_2;
param_v(13)=Penet_Par_2;
param_v(14)=Buf_size_split_Par;
param_v(15)=Buf_size_seg_Par;
param_v(16)=eps_Par;


%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
param=param_v(K);
return
