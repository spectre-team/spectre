clc; clear all; close all;
addpath('ms_gmm')

% step 1 - build GMM model
%GMM parameters
opts.base = 1;      %if baseline correction
opts.draw = 1;      %if draw results
opts.mz_thr = 0.3;  %M/Z threshold for merging
opts.if_merge = 0;  %if merge components
opts.if_rem = 0;    %if remove additional components

%load data
load('ms_data_1.mat')
%mz - nx1, y - nx1

mdl = ms_gmm_run(mz,y,opts);
