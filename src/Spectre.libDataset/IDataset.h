/*
* IDataset.h
* Modifiable Dataset interface.
*
Copyright 2017 Maciej Gamrat, Grzegorz Mrukwa

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

#include "IReadOnlyDataset.h"

namespace Spectre::libDataset
{
/// <summary>
/// Modifiable interface of the dataset.
/// </summary>
template <typename DataType, typename SampleMetadata, typename DatasetMetadata>
class IDataset : public IReadOnlyDataset<DataType, SampleMetadata, DatasetMetadata>
{
public:
    /// <summary>
    /// Gets sample under specified index.
    /// </summary>
    /// <param name="idx">The index.</param>
    /// <returns>Sample</returns>
    virtual DataType& operator[](size_t idx) = 0;
    /// <summary>
    /// Gets the sample metadata.
    /// </summary>
    /// <param name="idx">The index.</param>
    /// <returns>Sample metadata</returns>
    virtual SampleMetadata& GetSampleMetadata(size_t idx) = 0;
    /// <summary>
    /// Gets the dataset metadata.
    /// </summary>
    /// <returns>Dataset metadata</returns>
    virtual DatasetMetadata& GetDatasetMetadata() = 0;

    /// <summary>
    /// Adds the sample.
    /// </summary>
    /// <param name="sample">The sample.</param>
    /// <param name="metadata">The metadata.</param>
    virtual void AddSample(DataType sample, SampleMetadata metadata) = 0;
};
}
