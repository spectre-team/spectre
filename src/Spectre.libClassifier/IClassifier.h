/*
* IClassifier.h
* Common interface for all classifiers
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
#include <vector>
#include "Spectre.libDataset/IReadOnlyDataset.h"
#include "Types.h"

namespace Spectre::libClassifier
{
/// <summary>
/// Classifier base class.
/// </summary>
class IClassifier
{
public:
    using LabeledDataset = const libDataset::IReadOnlyDataset<Observation, Label, Empty>&;
    /// <summary>
    /// Try to fit dataset in fitness function.
    /// </summary>
    /// <param name="dataset">The dataset.</param>
    /// <returns>void</returns>
    virtual void Fit(LabeledDataset dataset) = 0;
    /// <summary>
    /// Predicts labels on test set.
    /// </summary>
    /// <param name="dataset">The dataset.</param>
    /// <returns>vector of labels</returns>
    virtual std::vector<Label> Predict(LabeledDataset dataset) const = 0;
    virtual ~IClassifier() = default;
};
}
