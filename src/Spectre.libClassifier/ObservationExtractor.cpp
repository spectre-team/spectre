/*
* ObservationExtractor.cpp
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

#include "Spectre.libException/NullPointerException.h"
#include "ObservationExtractor.h"

namespace Spectre::libClassifier {

ObservationExtractor::ObservationExtractor(const DataPointer data): m_Data(data)
{
    if(m_Data!=nullptr)
    {
        // @gmrukwa: Purposeful empty branch
    }
    else
    {
        throw libException::NullPointerException("data");
    }
}

OpenCvDataset ObservationExtractor::getOpenCvDatasetFromIndividual(const std::vector<bool>& individual) const
{
    std::vector<DataType> data;
    std::vector<Label> labels;
    for (auto i = 0u; i < individual.size(); ++i)
    {
        if (individual[i])
        {
            const auto& observation = m_Data->operator[](i);
            data.insert(data.end(), observation.begin(), observation.end());
            labels.push_back(m_Data->GetSampleMetadata(i));
        }
    }
    return OpenCvDataset(data, labels);
}

}
