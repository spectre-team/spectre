/*
* RandomSplitter.h
* Splits OpenCVDataset to training set and test set
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
#include "Spectre.libClassifier/OpenCvDataset.h"
#include "Spectre.libClassifier/SplittedOpevCvDataset.h"
// @gmrukwa: TODO: Fix this include. It should be local to this project.
#include "Spectre.libGenetic/DataTypes.h"

namespace Spectre::libClassifier
{
/// <summary>
/// Splits an input dataset randomly into two, proportionally sized subsets.
///
/// Split ratio is constant, as the seed. Therefore, repeated splits for the
/// same dataset are consistent: there will be exactly the same output.
/// To obtain a different split a re-creation of this object with another seed
/// is needed.
/// </summary>
class RandomSplitter
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="RandomSplitter"/> class.
    /// </summary>
    /// <param name="trainingPercent">The percent of observations included in training set.</param>
    /// <param name="rngSeed">The RNG seed.</param>
    explicit RandomSplitter(double trainingPercent, libGenetic::Seed rngSeed = 0);
    /// <summary>
    /// Splits the specified data.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>Dataset splitted into training and validation subsets.</returns>
    SplittedOpenCvDataset RandomSplitter::split(const OpenCvDataset& data) const;
private:
    const double m_trainingRate;
    const libGenetic::Seed m_Seed;
};
}
