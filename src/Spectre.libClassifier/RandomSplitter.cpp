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

namespace Spectre::libClassifier {

using namespace libPlatform::Functional;
using namespace libPlatform::Math;

RandomSplitter::RandomSplitter(double trainingProbability, Seed rngSeed)
    : m_trainingProbability(trainingProbability),
      m_randomNumberGenerator(rngSeed)
{

}

SplittedOpenCvDataset RandomSplitter::split(const Spectre::libClassifier::OpenCvDataset& data)
{
    std::bernoulli_distribution dist(m_trainingProbability);
    auto& randomNumberGenerator = m_randomNumberGenerator;
    auto randomBoolGenerator = [&randomNumberGenerator, &dist]() { return dist(randomNumberGenerator); };
    std::vector<bool> flags = build<bool, decltype(randomBoolGenerator)>(data.size(), randomBoolGenerator);
    std::vector<Observation> data1_observations = filter(data.GetData(), flags);
    std::vector<Label> labels1 = filter(data.GetSampleMetadata(), flags);
    flags = negate(flags);
    std::vector<Observation> data2_observations = filter(data.GetData(), flags);
    std::vector<Label> labels2 = filter(data.GetSampleMetadata(), flags);

    std::vector<DataType> data1, data2;
    for (auto i = 0u; i < data1_observations.size(); i++)
    {
        for (auto j = 0u; j < data1_observations[i].size(); j++)
        {
            data1.push_back(data1_observations[i][j]);
        }
    }
    for (auto i = 0u; i < data2_observations.size(); i++)
    {
        for (auto j = 0u; j < data2_observations[i].size(); j++)
        {
            data2.push_back(data2_observations[i][j]);
        }
    }
    OpenCvDataset dataset1(data1, labels1);
    OpenCvDataset dataset2(data2, labels2);
    auto result = SplittedOpenCvDataset(std::move(dataset1), std::move(dataset2));
    return result;
}

}
