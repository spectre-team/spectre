using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatlabAlgorithmsNative;

namespace Spectre.Algorithms
{
    public class Algorithms
    {
        private Gmm gaussianMixtureModel;
        private Preprocessing preprocessing;
        private Segmentation segmentation;

        public Algorithms()
        {
            gaussianMixtureModel = new Gmm();
            preprocessing = new Preprocessing();
            segmentation = new Segmentation();
        }

        public object ApplyGmm(object model, object data)
        {
            return gaussianMixtureModel.apply_gmm(model, data);
        }

        public object EstimateGmm(object mz, object data, object merge, object remove)
        {
            return gaussianMixtureModel.estimate_gmm(mz, data, merge, remove);
        }

        public System.Double[,] PeakAlignmentFFT(object mz, object data)
        {
            return (double[,])preprocessing.pafft(mz, data);
        }

        public System.Double[,] RemoveBaseline(double[] mz, double[,] data)
        {
            return (double[,])preprocessing.remove_baseline(mz, data);
        }

        public System.Double[,] TicNorm(double[,] data)
        {
            return (double[,])preprocessing.ticnorm(data);
        }

        public DivikResult Divik(double[,] data, int[,] coordinates, object[] varargin)
        {
            const int numberOfOutputArgs = 2;
            object tmp = segmentation.divik(numberOfOutputArgs, data, coordinates, varargin);
            return new DivikResult();
        }
    }
}
