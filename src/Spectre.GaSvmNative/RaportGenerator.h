/*
* RaportGenerator.h
* Class that generates reports for Genetic Algorithm execution into a file.
*
Copyright 2017 Spectre Team

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
#include <fstream>
#include <omp.h>
#include "Spectre.libClassifier/ConfusionMatrix.h"
#include "Spectre.libClassifier/OpenCvDataset.h"
#include "Spectre.libGenetic/Individual.h"

namespace Spectre::GaSvmNative
{
/// <summary>
/// Class used to create file reports from genetic algorithm execution results.
/// </summary>
class RaportGenerator final
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="RaportGenerator"/> class.
    /// </summary>
    /// <param name="filename">The filename.</param>
    /// <param name="populationSize">The population size.</param>
    /// <param name="separator">The separator.</param>
    explicit RaportGenerator(std::string filename,
                             unsigned int populationSize,
                             const std::string& separator=",");
    /// <summary>
    /// Writes results.
    /// </summary>
    /// <param name="matrix">The confusion matrix.</param>
    /// <param name="individual">The individual.</param>
    /// <param name="trainingTime">The training time.</param>
    /// <param name="meanClassificationTime">The mean classification time.</param>
    /// <param name="numberOfSupportVectors">The number of support vectors.</param>
    /// <param name="validationResults">The validation results.</param>
    /// <returns>void</returns>
    void Write(const libClassifier::ConfusionMatrix& matrix,
               const libGenetic::Individual& individual,
               double trainingTime,
               double meanClassificationTime,
               unsigned int numberOfSupportVectors,
               const libClassifier::ConfusionMatrix* validationResults);
    ~RaportGenerator();
private:
    /// <summary>
    /// The file.
    /// </summary>
    std::ofstream m_File;
    /// <summary>
    /// The separator.
    /// </summary>
    const std::string m_Separator;
    /// <summary>
    /// The number of processed individuals.
    /// </summary>
    uint m_IndividualsProcessed;
    /// <summary>
    /// The population size.
    /// </summary>
    const uint m_PopulationSize;
    /// <summary>
    /// The filename.
    /// </summary>
    const std::string m_Filename;
    /// <summary>
    /// The writelock.
    /// </summary>
    omp_lock_t m_WriteLock;
};
}
