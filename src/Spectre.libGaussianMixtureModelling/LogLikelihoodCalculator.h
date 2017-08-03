/*
 * LogLikelihoodCalculator.h
 * Provides implementation of log likelihood calculating class
 * used in Gaussian Mixture Modelling algorithms.
 * Knowledge required for understanding the following code has been
 * presented in the following book
 * http://www.rmki.kfki.hu/~banmi/elte/bishop_em.pdf
 * which shall serve as a mathematical reference, and to which the
 * comments in the code will refer to.
 *
 * In regard to the following code concerning calculation of log 
 * likelihood procedure, please refer to 
 * 9.2.1 Maximum likelihood (page 432)
 * and more specifically
 * Equation (9.14) (page 433)
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
#include "ArgumentNullException.h"
#include "GaussianMixtureModel.h"
#include "GaussianDistribution.h"
#include "DataType.h"

namespace Spectre::libGaussianMixtureModelling
{
/// <summary>
/// Class serves the purpose of calculation of log likelihood for the gaussian 
/// mixture modelling. Serves as a reference to learn from and is purpously 
/// not optimized to avoid obfuscation of the code.
/// </summary>
class LogLikelihoodCalculator
{
public:
    /// <summary>
    /// Constructor initializing the class with data required for calculation of 
    /// log likelihood.
    /// </summary>
    /// <param name="mzArray">Array of m/z values.</param>
    /// <param name="intensities">Set of corresponding mean intensities values.</param>
    /// <param name="size">Size of the mzArray and itensities arrays.</param>
    /// <param name="components">Gaussian components.</param>
    /// <exception cref="ArgumentNullException">Thrown when either of mzArray or intensities pointers are null</exception>
    LogLikelihoodCalculator(DataType *mzArray, DataType *intensities,
                            unsigned size, const std::vector<GaussianComponent> &components)
        : m_pMzArray(mzArray), m_pIntensities(intensities), m_DataSize(size),
          m_Components(components)
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
    /// Calculates the log likelihood using the class supplied template type.
    /// </summary>
    /// <returns>
    /// Value of log likelihood.
    /// </returns>
    DataType LogLikelihoodCalculator::CalculateLikelihood()
    {
        // This part performs calcluation of log likelihood
        // performed with accordance to equation (9.14)
        // presented in the book.
        DataType sumOfLogs = 0.0;
        for (unsigned i = 0; i < m_DataSize; i++)
        {
            DataType sum = 0.0;
            for (unsigned k = 0; k < m_Components.size(); k++)
            {
                auto component = m_Components[k];
                sum += component.weight * m_pIntensities[i] * Gaussian(m_pMzArray[i], component.mean, component.deviation);
            }
            sumOfLogs += log(sum);
        }
        return sumOfLogs;
    }

private:
    DataType *m_pMzArray;
    DataType *m_pIntensities;
    unsigned m_DataSize;
    const std::vector<GaussianComponent> &m_Components;
};
};
