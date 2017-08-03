/*
* IReadOnlyDataset.h
* Read-only dataset interface.
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

#include <gsl.h>

namespace Spectre::libDataset
{
/// <summary>
/// Read-only dataset interface.
/// </summary>
template <typename DataType, typename SampleMetadata, typename DatasetMetadata>
class IReadOnlyDataset
{
public:
    /// <summary>
    /// Gets sample under specified index in read-only fashion.
    /// </summary>
    /// <param name="idx">The index.</param>
    /// <returns>Sample</returns>
    virtual const DataType& operator[](size_t idx) const = 0;
    /// <summary>
    /// Gets the sample metadata in read-only fashion.
    /// </summary>
    /// <param name="idx">The index.</param>
    /// <returns>Sample metadata</returns>
    virtual const SampleMetadata& GetSampleMetadata(size_t idx) const = 0;
    /// <summary>
    /// Gets the dataset metadata in read-only fashion.
    /// </summary>
    /// <returns>Dataset metadata</returns>
    virtual const DatasetMetadata& GetDatasetMetadata() const = 0;

    /// <summary>
    /// Gets the data in read-only fashion.
    /// </summary>
    /// <returns></returns>
    virtual gsl::span<const DataType> GetData() const = 0;
    /// <summary>
    /// Gets the sample metadata in read-only fashion.
    /// </summary>
    /// <returns></returns>
    virtual gsl::span<const SampleMetadata> GetSampleMetadata() const = 0;

    /// <summary>
    /// Number of elements in dataset.
    /// </summary>
    /// <returns>Size</returns>
    virtual size_t size() const = 0;
    /// <summary>
    /// Checks, whether dataset is empty.
    /// </summary>
    /// <returns>True, if dataset is empty.</returns>
    virtual bool empty() const = 0;

    /// <summary>
    /// Cleans up an instance of the <see cref="IReadOnlyDataset"/> class.
    /// </summary>
    virtual ~IReadOnlyDataset() = default;
};
}
