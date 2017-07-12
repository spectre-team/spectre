/*
* Dataset.h
* Data-owning implementation of Dataset interfaces.
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

#include <vector>
#include "IDataset.h"

namespace Spectre::libDataset
{
    /// <summary>
    /// Data-owning implementation of dataset interfaces.
    /// </summary>
    template<typename DataType, typename SampleMetadata, typename DatasetMetadata>
    class Dataset : public IDataset<DataType, SampleMetadata, DatasetMetadata>
    {
    public:
        /// <summary>
        /// Initializes a new instance of the <see cref="Dataset"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="sampleMetadata">The sample metadata.</param>
        /// <param name="metadata">The metadata.</param>
        Dataset(gsl::span<const DataType> data, gsl::span<const SampleMetadata> sampleMetadata, DatasetMetadata metadata) :
            m_data(data.begin(), data.end()), m_sampleMetadata(sampleMetadata.begin(), sampleMetadata.end()), m_metadata(metadata)
        {
        }

        /// <summary>
        /// Gets sample under specified index in read-only fashion.
        /// </summary>
        /// <param name="idx">The index.</param>
        /// <returns>Sample</returns>
        const DataType& operator[](int idx) const override
        {
            return m_data[idx];
        }

        /// <summary>
        /// Gets the sample metadata in read-only fashion.
        /// </summary>
        /// <param name="idx">The index.</param>
        /// <returns>Sample metadata</returns>
        const SampleMetadata& getSampleMetadata(int idx) const override
        {
            return m_sampleMetadata[idx];
        }

        /// <summary>
        /// Gets the dataset metadata in read-only fashion.
        /// </summary>
        /// <returns>Dataset metadata</returns>
        const DatasetMetadata& getDatasetMetadata() const override
        {
            return m_metadata;
        }

        /// <summary>
        /// Gets sample under specified index.
        /// </summary>
        /// <param name="idx">The index.</param>
        /// <returns>Sample</returns>
        DataType& operator[](int idx) override
        {
            return m_data[idx];
        }

        /// <summary>
        /// Gets the sample metadata.
        /// </summary>
        /// <param name="idx">The index.</param>
        /// <returns>Sample metadata</returns>
        SampleMetadata& getSampleMetadata(int idx) override
        {
            return m_sampleMetadata[idx];
        }

        /// <summary>
        /// Gets the dataset metadata.
        /// </summary>
        /// <returns>Dataset metadata</returns>
        DatasetMetadata& getDatasetMetadata() override
        {
            return m_metadata;
        }

        /// <summary>
        /// Gets the data in read-only fashion.
        /// </summary>
        /// <returns></returns>
        gsl::span<const DataType> getData() const override
        {
            return gsl::span<const DataType>(m_data);
        }

        /// <summary>
        /// Gets the sample metadata in read-only fashion.
        /// </summary>
        /// <returns></returns>
        gsl::span<const SampleMetadata> getSampleMetadata() const override
        {
            return gsl::span<const SampleMetadata>(m_sampleMetadata);
        }

        /// <summary>
        /// Adds the sample.
        /// </summary>
        /// <param name="sample">The sample.</param>
        /// <param name="metadata">The metadata.</param>
        void addSample(DataType sample, SampleMetadata metadata) override
        {
            m_data.push_back(sample);
            m_sampleMetadata.push_back(metadata);
        }

        /// <summary>
        /// Number of elements in dataset.
        /// </summary>
        /// <returns>Size</returns>
        size_t size() const override
        {
            return m_data.size();
        }

        /// <summary>
        /// Checks, whether dataset is empty.
        /// </summary>
        /// <returns>True, if dataset is empty.</returns>
        bool empty() const override
        {
            return m_data.empty();
        }

    private:
        std::vector<DataType> m_data;
        std::vector<SampleMetadata> m_sampleMetadata;
        DatasetMetadata m_metadata;
    };
}