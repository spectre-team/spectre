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
#include <ctime>

namespace Spectre::libClassifier {

RandomSplitter::RandomSplitter(double trainingPercent, uint rngSeed)
    : trainingPercent(trainingPercent),
      rngSeed(rngSeed)
{

}

std::pair<Spectre::libClassifier::OpenCvDataset, Spectre::libClassifier::OpenCvDataset> RandomSplitter::split(Spectre::libClassifier::OpenCvDataset* data)
{
    std::vector<DataType> data1{}, data2{};
    std::vector<Label> labels1{}, labels2{};

    auto trainingSize = data->size() * 0.7;
    if (rngSeed == 0)
    {
        srand(unsigned int(time(NULL)));
    }
    else {
        srand(rngSeed);
    }
    int random;

    for (auto i = 0u; i < data->size(); i++)
    {
        Observation dataObservation = (*data)[i];
        if (data1.size() < trainingSize)
        {
            random = rand() % data->size();
            if (random < trainingSize)
            {
                copyObservation(dataObservation, &data1);
                copyLabel(data->GetSampleMetadata(i), &labels1);
            }
            else
            {
                copyObservation(dataObservation, &data2);
                labels2.push_back(data->GetSampleMetadata(i));
            }
        }
        else
        {
            copyObservation(dataObservation, &data2);
            labels2.push_back(data->GetSampleMetadata(i));
        }
    }

    Spectre::libClassifier::OpenCvDataset dataset1(data1, labels1);
    Spectre::libClassifier::OpenCvDataset dataset2(data2, labels2);
    std::pair<Spectre::libClassifier::OpenCvDataset, Spectre::libClassifier::OpenCvDataset> result(std::make_pair(dataset1, dataset2));
    return result;
}

void RandomSplitter::copyObservation(Observation observation, std::vector<DataType> *data)
{
    for (auto i=0; i<observation.size(); i++)
    {
        data->push_back(observation.data()[i]);
    }
}

void RandomSplitter::copyLabel(Label label, std::vector<Label> *data)
{
    data->push_back(label);
}

}
