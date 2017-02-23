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

        public object PeakAlignmentFFT(object mz, object data)
        {
            return preprocessing.pafft(mz, data);
        }

        public object RemoveBaseline(object mz, object data)
        {
            return preprocessing.remove_baseline(mz, data);
        }

        public object TicNorm(object data)
        {
            return preprocessing.ticnorm(data);
        }

        public object[] Divik(int numberOfOutputArgs, object data, object coordinates, params object[] varargin)
        {
            return segmentation.divik(numberOfOutputArgs, data, coordinates, varargin);
        }
    }
}
