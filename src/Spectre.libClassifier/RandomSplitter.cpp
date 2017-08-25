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

#include "RandomSplitter.h"
#include <random>
#include "Spectre.libPlatform/Filter.h"

namespace Spectre::libClassifier {

RandomSplitter::RandomSplitter(double trainingPercent, Seed rngSeed)
    : m_trainingPercent(trainingPercent),
      m_randomNumberGenerator(rngSeed)
{

}

std::pair<Spectre::libClassifier::OpenCvDataset, Spectre::libClassifier::OpenCvDataset> RandomSplitter::split(const Spectre::libClassifier::OpenCvDataset& data)
{
    std::vector<bool> flags;
    std::bernoulli_distribution dist(m_trainingPercent);

    for (auto i = 0u; i < data.size(); i++)
    {
        flags.push_back(dist(m_randomNumberGenerator));
    }
    std::vector<Observation> data1_observations = libPlatform::Functional::filter(data.GetData(), flags);
    std::vector<Label> labels1 = libPlatform::Functional::filter(data.GetSampleMetadata(), flags);
    for (auto i = 0u; i < flags.size(); i++)
    {
        flags[i] = !flags[i];
    }
    std::vector<Observation> data2_observations = libPlatform::Functional::filter(data.GetData(), flags);
    std::vector<Label> labels2 = libPlatform::Functional::filter(data.GetSampleMetadata(), flags);

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
    Spectre::libClassifier::OpenCvDataset dataset1(data1, labels1);
    Spectre::libClassifier::OpenCvDataset dataset2(data2, labels2);
    auto result = std::make_pair(std::move(dataset1), std::move(dataset2));
    return result;
}

}
