/*
 * Algorithms.cs
 * Contains .NET interface for implemented algorithms.
 * 
   Copyright 2017 Wilgierz Wojciech, Michal Gallus, Grzegorz Mrukwa

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
using System;
using MatlabAlgorithmsNative;
using Spectre.Algorithms.Results;

namespace Spectre.Algorithms
{
    public class Algorithms:IDisposable
    {
        private Gmm gaussianMixtureModel;
        private Preprocessing preprocessing;
        private Segmentation segmentation;

        private bool disposed = false;

        public Algorithms()
        {
            gaussianMixtureModel = new Gmm();
            preprocessing = new Preprocessing();
            segmentation = new Segmentation();
        }

        public double[,] ApplyGmm(GmmModel model, double[,] data, double[] mz)
        {
	        var matlabModel = model.MatlabStruct;
            return (double[,])gaussianMixtureModel.apply_gmm(matlabModel, data, mz);
            
        }

        public GmmModel EstimateGmm(object mz, double[,] data, bool merge, bool remove)
        {
			var matlabModel = gaussianMixtureModel.estimate_gmm(mz, data, merge, remove);
			var model = new GmmModel(matlabModel);
	        return model;
        }

        public double[,] PeakAlignmentFFT(object mz, object data)
        {
            return (double[,])preprocessing.pafft(mz, data);
        }

        public double[,] RemoveBaseline(double[] mz, double[,] data)
        {
            return (double[,])preprocessing.remove_baseline(mz, data);
        }

        public double[,] TicNorm(double[,] data)
        {
            return (double[,])preprocessing.ticnorm(data);
        }

        public DivikResult Divik(double[,] data, int[,] coordinates, object[] varargin)
        {
            const int numberOfOutputArgs = 2;
            object tmp = segmentation.divik(numberOfOutputArgs, data, coordinates, varargin);
            return new DivikResult();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.gaussianMixtureModel.Dispose();
                    this.preprocessing.Dispose();
                    this.segmentation.Dispose();
                }
                disposed = true;
            }
        }

	    ~Algorithms()
	    {
		    Dispose(false);
	    }
    }
}
