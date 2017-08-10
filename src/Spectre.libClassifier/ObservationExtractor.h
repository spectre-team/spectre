// /*
// * ObservationExtractor.h
// * 
// *
// Copyright 2017 Spectre Team
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// */

#pragma once
#include "Spectre.libDataset/IReadOnlyDataset.h"
#include "Spectre.libClassifier/Empty.h"
#include "Spectre.libClassifier/OpenCvDataset.h"
#include "Spectre.libGenetic/Individual.h"

namespace Spectre::libClassifier {

using DataPointer = libDataset::IReadOnlyDataset<Observation, Label, Empty>*;

class ObservationExtractor
{
public:
    ObservationExtractor(const DataPointer data);
    DataPointer getOpenCVDatasetFromIndividual(const libGenetic::Individual &individual);

private:
    const DataPointer m_data;
};

}
