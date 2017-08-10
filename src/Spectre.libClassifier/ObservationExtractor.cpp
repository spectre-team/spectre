/*
* OpenCvDataset.cpp
* class for getting data from Individual object
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

#include "ObservationExtractor.h"

namespace Spectre::libClassifier {

ObservationExtractor::ObservationExtractor(const DataPointer data): m_data(data) { }

DataPointer ObservationExtractor::getOpenCVDatasetFromIndividual(const libGenetic::Individual &individual)
{
    std::vector<DataType> data {};
    std::vector<Label> labels {};
    for (auto i = 0u; i < individual.size(); ++i)
    {
        if (individual[i] == true)
        {
            Observation observation = (*m_data)[i];
            for (auto j = 0u; j < observation.size(); j++)
            {
                data.push_back(observation[j]);
            }
            labels.push_back(m_data->GetSampleMetadata(i));
        }
    }
    OpenCvDataset dataset = OpenCvDataset(data, labels);
    return &dataset;
}

}
