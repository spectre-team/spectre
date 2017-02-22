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

        public void ApplyGmm()
        {
            
        }

        public object[] EstimateGmm()
        {
            return null;
        }

        public void PeakAlignmentFFT()
        {
            
        }

        public void RemoveBaseline()
        {
            
        }

        public void TicNorm()
        {
            
        }

        public object[] Divik()
        {
            return null;
        }
    }
}
