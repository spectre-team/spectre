/*
* RandomSplitter.cpp
* Splits dataset into training and control datasets.
*
Copyright 2017 Grzegorz Mrukwa, Wojciech Wilgierz

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

#include <random>
#include "RandomSplitter.h"
#include "Spectre.libPlatform/Filter.h"
#include "Spectre.libPlatform/Math.h"
#include "Spectre.libGenetic/DataTypes.h"
#include "Spectre.libClassifier/NegativeTrainingRateException.h"
#include "Spectre.libClassifier/ExcessiveTrainingRateException.h"
#include "Spectre.libException/EmptyArgumentException.h"

namespace Spectre::libClassifier {

using namespace libPlatform::Functional;
using namespace libPlatform::Math;

RandomSplitter::RandomSplitter(double trainingRate, libGenetic::Seed rngSeed)
    : m_trainingRate(trainingRate),
      m_randomNumberGenerator(rngSeed)
{
    if (trainingRate >= 0)
    {
    }
    else
    {
        throw NegativeTrainingRateException(trainingRate);
    }
    if (trainingRate <= 1)
    {
    }
    else
    {
        throw ExcessiveTrainingRateException(trainingRate);
    }
}

SplittedOpenCvDataset RandomSplitter::split(const Spectre::libClassifier::OpenCvDataset& data)
{
    if (data.empty())
    {
        throw libException::EmptyArgumentException("data");
    }
    std::vector<int> indexes = range(0, int(data.size()));
    libGenetic::RandomDevice randomDevice;
    libGenetic::RandomNumberGenerator g(randomDevice());
    std::shuffle(indexes.begin(), indexes.end(), g);

    std::vector<DataType> trainingData{};
    std::vector<DataType> validationData{};
    std::vector<Label> trainingLabels{};
    std::vector<Label> validationLabels{};
    trainingData.reserve(data.size() * data[0].size());
    validationData.reserve(data.size() * data[0].size());
    trainingLabels.reserve(data.size());
    validationLabels.reserve(data.size());
    int trainingLimit = static_cast<int>(data.size() * m_trainingRate);
    for (auto i = 0; i < trainingLimit; i++)
    {
        Observation observation(data[i]);
        trainingData.insert(trainingData.end(), observation.begin(), observation.end());
        trainingLabels.push_back(data.GetSampleMetadata(i));
    }
    for (auto i = trainingLimit; i < data.size(); i++)
    {
        Observation observation(data[i]);
        validationData.insert(validationData.end(), observation.begin(), observation.end());
        validationLabels.push_back(data.GetSampleMetadata(i));
    }
    OpenCvDataset dataset1(trainingData, trainingLabels);
    OpenCvDataset dataset2(validationData, validationLabels);
    auto result = SplittedOpenCvDataset(std::move(dataset1), std::move(dataset2));
    return result;
}

}
