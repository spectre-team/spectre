using System;
using MatlabAlgorithmsNative;

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

        public double[,] ApplyGmm(object model, double[,] data, double[] mz)
        {
            return (double[,])gaussianMixtureModel.apply_gmm(model, data, mz);
            
        }

        public object EstimateGmm(object mz, double[,] data, bool merge, bool remove)
        {
            return gaussianMixtureModel.estimate_gmm(mz, data, merge, remove);
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
