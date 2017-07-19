/*
 * MaximizationRunnerRef.h
 * Provides reference implementation of maximization part of EM algorithm
 * used for Gaussian Mixture Modelling.
 * 
 * Knowledge required for understanding the following code has been
 * presented in the following document
 * https://brilliant.org/wiki/gaussian-mixture-model/#learning-the-model
 * which shall serve as a mathematical reference, and to which the
 * comments in the code will refer to.
 *
 * In regard to the following code concerning Maximization procedure,
 * please refer to 
 * Learning the Model 
 *      Algorithm for Univariate Gaussian Mixture Models 
 *           Maximization (M) Step
 *
 * Note: throught reading of the document, you may notice that each
 * time usage of affiliation matrix occurs it is multiplied by 
 * corresponding intesity, to increase the probability *intensity*
 * times that a certain mz belongs to a given component. This is 
 * because the spectra we are given are histograms, and therefore
 * a single mz value with intensity e.g. 500 indicates that
 * there are de facto 500 of entities holding that certain mz value
 * that are stored in the dataset.
 *
Copyright 2017 Michal Gallus

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
#pragma once
#include <vector>
#include "ArgumentNullException.h"
#include "DataType.h"
#include "GaussianMixtureModel.h"
#include "Matrix.h"

namespace Spectre::libGaussianMixtureModelling
{
    /// <summary>
    /// Class serves the purpose of maximization step of Expectation Maximization algorithm. 
    /// Serves as a reference to learn from and is purpously not optimized to avoid 
    /// obfuscation of the code.
    /// </summary>
    class MaximizationRunnerRef
    {
    public:
        /// <summary>
        /// Constructor initializing the class with data required during maximization step.
        /// </summary>
        /// <param name="mzArray">Array of m/z values.</param>
        /// <param name="intensities">Set of corresponding mean intensities values.</param>
        /// <param name="size">Size of the mzArray and itensities arrays.</param>
        /// <param name="affilationMatrix">Matrix symbolising the probability of affilation
        /// of each sample to a certain gaussian component.</param>
        /// <param name="components">Gaussian components to be updated</param>
        /// <exception cref="ArgumentNullException">Thrown when either of mzArray or intensities pointers are null</exception>
        MaximizationRunnerRef(DataType* mzArray, DataType* intensities, unsigned size,
            Matrix& affilationMatrix, std::vector<GaussianComponent>& components)
            : m_pMzArray(mzArray), m_pIntensities(intensities), m_DataSize(size), 
              m_AffilationMatrix(affilationMatrix), m_Components(components)
        {
            if (mzArray == nullptr)
            {
                throw ArgumentNullException("mzArray");
            }

            if (intensities == nullptr)
            {
                throw ArgumentNullException("intensities");
            }
        }

        /// <summary>
        /// Updates weights in gaussian components, based on affilation (gamma) matrix.
        /// </summary>
        void MaximizationRunnerRef::UpdateWeights()
        {
            // This part conducts the instruction:
            // "Using the gamma calculated in the Expectation step, 
            // calculate in the following order, for every k: (phi part)"
            // from the document
            DataType totalDataSize = 0.0;
            for (unsigned i = 0; i < m_DataSize; i++)
            {
                totalDataSize += m_pIntensities[i];
            }

            const unsigned numberOfComponents = (unsigned)m_Components.size();
            for (unsigned k = 0; k < numberOfComponents; k++)
            {
                DataType weight = 0.0;
                for (unsigned i = 0; i < m_DataSize; i++)
                {
                    weight += m_AffilationMatrix[i][k] * m_pIntensities[i];
                }
                m_Components[k].weight = weight / totalDataSize;
            }
        }

        /// <summary>
        /// Updates means in gaussian components, based on affilation (gamma) matrix.
        /// </summary>
        void MaximizationRunnerRef::UpdateMeans()
        {
            // This part conducts the instruction:
            // "Using the gamma calculated in the Expectation step, 
            // calculate in the following order, for every k: (mu part)"
            // from the document
            const unsigned numberOfComponents = (unsigned)m_Components.size();
            for (unsigned k = 0; k < numberOfComponents; k++)
            {
                DataType denominator = 0.0;
                DataType numerator = 0.0;
                for (unsigned i = 0; i < m_DataSize; i++)
                {
                    denominator += m_AffilationMatrix[i][k] * m_pIntensities[i];
                    numerator += m_AffilationMatrix[i][k] * m_pMzArray[i] * m_pIntensities[i];
                }
                m_Components[k].mean = numerator / denominator;
            }
        }

        /// <summary>
        /// Updates standard deviations in gaussian components, based on affilation 
        /// (gamma) matrix.
        /// </summary>
        void MaximizationRunnerRef::UpdateStdDeviations()
        {
            // This part conducts the instruction:
            // "Using the gamma calculated in the Expectation step, 
            // calculate in the following order, for every k: (sigma part)"
            // from the document.
            // Please note, that equation presented in the document 
            // is wrong in that it states that the right hand side of the
            // equation is equal to sigma, while in a fact, it should be 
            // equated to sigma^2. Hence the sqrt() is used during the 
            // assignment of the standard deviation.
            const unsigned numberOfComponents = (unsigned)m_Components.size();
            for (unsigned k = 0; k < numberOfComponents; k++)
            {
                DataType denominator = 0.0;
                DataType numerator = 0.0;
                for (unsigned i = 0; i < m_DataSize; i++)
                {
                    denominator += m_AffilationMatrix[i][k] * m_pIntensities[i];
                    numerator += m_AffilationMatrix[i][k] * pow(m_pMzArray[i] - m_Components[k].mean, 2) * m_pIntensities[i];
                }
                m_Components[k].deviation = sqrt(numerator / denominator);
            }
        }

    private:
        DataType* m_pMzArray;
        DataType* m_pIntensities;
        unsigned m_DataSize;
        Matrix& m_AffilationMatrix;
        std::vector<GaussianComponent>& m_Components;
    };
}