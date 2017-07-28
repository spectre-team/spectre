/*
* IClassifier.h
* Basic interface of a classifier.
*
Copyright 2017 Grzegorz Mrukwa

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

#include <vector>
#include "Spectre.libDataset/IReadOnlyDataset.h"

namespace Spectre::libClassifier
{
using Label = unsigned;
using DataType = double;
using Empty = void;
using DatasetPtr = libDataset::IReadOnlyDataset<DataType, Empty, Empty>*;
using LabeledDatasetPtr = libDataset::IReadOnlyDataset<DataType, Label, Empty>*;

/// <summary>
/// This is the basic interface of a classifier.
/// </summary>
class IClassifier
{
public:
    /// <summary>
    /// Applies the specified classifier on a dataset.
    /// </summary>
    /// <param name="dataset">The dataset.</param>
    /// <returns>Estimated labeling</returns>
    virtual std::vector<Label> apply(DatasetPtr dataset) = 0;
    virtual ~IClassifier() = default;
};
}