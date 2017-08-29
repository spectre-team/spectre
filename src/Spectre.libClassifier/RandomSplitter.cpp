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
    std::vector<int> indexes = range(0, int(data.size()));
    RandomDevice rd;
    RandomNumberGenerator g(rd());
    std::shuffle(indexes.begin(), indexes.end(), g);

    std::vector<DataType> data1{};
    std::vector<DataType> data2{};
    std::vector<Label> labels1{};
    std::vector<Label> labels2{};
    int trainingLimit = int(data.size() * m_trainingProbability);
    for (auto i = 0; i < trainingLimit; i++)
    {
        Observation observation(data[i]);
        for (DataType dataType: observation)
        {
            data1.push_back(dataType);
        }
        labels1.push_back(data.GetSampleMetadata(i));
    }
    for (auto i = trainingLimit; i < data.size(); i++)
    {
        Observation observation(data[i]);
        for (DataType dataType : observation)
        {
            data2.push_back(dataType);
        }
        labels2.push_back(data.GetSampleMetadata(i));
    }

    OpenCvDataset dataset1(data1, labels1);
    OpenCvDataset dataset2(data2, labels2);
    auto result = SplittedOpenCvDataset(std::move(dataset1), std::move(dataset2));
    return result;
}

}
