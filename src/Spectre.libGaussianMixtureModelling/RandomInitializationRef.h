/*
 * RandomInitializationRef.h
 * Provides reference implementation of basic initialization used in 
 * Expectation Maximization algorithm.
 * 
 * Knowledge required for understanding the following code has been
 * presented in the following document
 * https://brilliant.org/wiki/gaussian-mixture-model/#learning-the-model
 * which shall serve as a mathematical reference, and to which the
 * comments in the code will refer to.
 *
 * In regard to the following code concerning Initialization procedure,
 * please refer to 
 * Learning the Model 
 *      Algorithm for Univariate Gaussian Mixture Models 
 *           Initialization Step
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
#include <random>
#include "GaussianMixtureModel.h"
#include "DataType.h"

typedef std::mt19937_64 RandomNumberGenerator;

namespace Spectre::libGaussianMixtureModelling
{
/// <summary>
/// Class serves the purpose of basic initialization of data for gaussian components
/// used by Expectation Maximization algorithm. Serves as a reference to learn from
/// and is purpously not optimized to avoid obfuscation of the code.
/// </summary>
class RandomInitializationRef
{
public:
    /// <summary>
    /// Constructor initializing the class with data required during initialization.
    /// </summary>
    /// <param name="mzArray">Array of m/z values.</param>
    /// <param name="size">Size of the mzArray and itensities arrays.</param>
    /// <param name="rngEngine">Mersenne-Twister engine to be used during initialization step.</param>
    /// <param name="components">Gaussian components to be initialized.</param>
    /// <exception cref="ArgumentNullException">Thrown when mzArray pointer is null</exception>
    RandomInitializationRef(DataType *mzArray, unsigned size,
                            std::vector<GaussianComponent> &components, RandomNumberGenerator &rngEngine) :
        m_pMzArray(mzArray), m_DataSize(size), m_Components(components),
        m_RandomNumberGenerator(rngEngine)
    {
        if (mzArray == nullptr)
        {
            throw ArgumentNullException("mzArray");
        }
    }

    /// <summary>
    /// Assigns means to gaussian components, by choosing random samples from the dataset.
    /// </summary>
    void RandomInitializationRef::AssignRandomMeans()
    {
        // This part conducts the instruction:
        // "Randomly assign samples without replacement from the dataset"
        // from the document
        const unsigned numberOfComponents = (unsigned)m_Components.size();
        for (unsigned i = 0; i < numberOfComponents; i++)
        {
            unsigned randomIndex = m_RandomNumberGenerator() % m_DataSize;
            m_Components[i].mean = m_pMzArray[randomIndex];
        }
    }

    /// <summary>
    /// Calculates variance of the dataset, and assigns its square root
    /// (standard deviation) to variances of all components.
    /// </summary>
    void RandomInitializationRef::AssignVariances()
    {
        // This part conducts the instruction:
        // "Set all component variance estimates to the sample variance"
        // from the document
        DataType sampleMean = CalculateMean();
        DataType variance = CalculateVariance(sampleMean);
        const unsigned numberOfComponents = (unsigned)m_Components.size();
        for (unsigned i = 0; i < numberOfComponents; i++)
        {
            m_Components[i].deviation = sqrt(variance);
        }
    }

    /// <summary>
    /// Assigns uniform weight to each component. Each weight equals to
    /// 1 / K, and therefore they all sum to 1.
    /// </summary>
    void RandomInitializationRef::AssignWeights()
    {
        // This part conducts the instruction:
        // "Set all component distribution prior estimates to the uniform distribution"
        // from the document
        const unsigned numberOfComponents = (unsigned)m_Components.size();
        DataType weight = 1.0 / numberOfComponents;
        for (unsigned i = 0; i < numberOfComponents; i++)
        {
            m_Components[i].weight = weight;
        }
    }

private:
    DataType RandomInitializationRef::CalculateMean()
    {
        DataType mean = 0.0;

        for (unsigned i = 0; i < m_DataSize; i++)
        {
            mean += m_pMzArray[i];
        }

        return mean / (DataType)m_DataSize;
    }

    DataType RandomInitializationRef::CalculateVariance(DataType mean)
    {
        DataType variance = 0.0;

        for (unsigned i = 0; i < m_DataSize; i++)
        {
            variance += pow(m_pMzArray[i] - mean, 2);
        }

        return variance / (DataType)m_DataSize;
    }

    DataType *m_pMzArray;
    unsigned m_DataSize;
    std::vector<GaussianComponent> &m_Components;
    RandomNumberGenerator &m_RandomNumberGenerator;
};
}
