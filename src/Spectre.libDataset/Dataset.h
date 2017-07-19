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
#include "InconsistentInputSizeException.h"

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
            m_Data(data.begin(), data.end()), m_SampleMetadata(sampleMetadata.begin(), sampleMetadata.end()), m_Metadata(metadata)
        {
            if(data.size() == sampleMetadata.size())
            {
                
            }
            else
            {
                throw InconsistentInputSizeException(data.size(), sampleMetadata.size());
            }
        }

        /// <summary>
        /// Gets sample under specified index in read-only fashion.
        /// </summary>
        /// <param name="idx">The index.</param>
        /// <returns>Sample</returns>
        const DataType& operator[](size_t idx) const override
        {
            if (idx < m_Data.size())
            {
                return m_Data[idx];
            }
            else
            {
                throw OutOfRange(idx, m_Data.size());
            }
        }

        /// <summary>
        /// Gets the sample metadata in read-only fashion.
        /// </summary>
        /// <param name="idx">The index.</param>
        /// <returns>Sample metadata</returns>
        const SampleMetadata& GetSampleMetadata(size_t idx) const override
        {
            if (idx < m_SampleMetadata.size())
            {
                return m_SampleMetadata[idx];
            }
            else
            {
                throw OutOfRange(idx, m_SampleMetadata.size());
            }
        }

        /// <summary>
        /// Gets the dataset metadata in read-only fashion.
        /// </summary>
        /// <returns>Dataset metadata</returns>
        const DatasetMetadata& GetDatasetMetadata() const override
        {
            return m_Metadata;
        }

        /// <summary>
        /// Gets sample under specified index.
        /// </summary>
        /// <param name="idx">The index.</param>
        /// <returns>Sample</returns>
        DataType& operator[](size_t idx) override
        {
            if (idx < m_Data.size())
            {
                return m_Data[idx];
            }
            else
            {
                throw OutOfRange(idx, m_Data.size());
            }
        }

        /// <summary>
        /// Gets the sample metadata.
        /// </summary>
        /// <param name="idx">The index.</param>
        /// <returns>Sample metadata</returns>
        SampleMetadata& GetSampleMetadata(size_t idx) override
        {
            if (idx < m_SampleMetadata.size())
            {
                return m_SampleMetadata[idx];
            }
            else
            {
                throw OutOfRange(idx, m_SampleMetadata.size());
            }
        }

        /// <summary>
        /// Gets the dataset metadata.
        /// </summary>
        /// <returns>Dataset metadata</returns>
        DatasetMetadata& GetDatasetMetadata() override
        {
            return m_Metadata;
        }

        /// <summary>
        /// Gets the data in read-only fashion.
        /// </summary>
        /// <returns>Read-only data</returns>
        gsl::span<const DataType> GetData() const override
        {
            return gsl::span<const DataType>(m_Data);
        }

        /// <summary>
        /// Gets the sample metadata in read-only fashion.
        /// </summary>
        /// <returns>Read-only metadata</returns>
        gsl::span<const SampleMetadata> GetSampleMetadata() const override
        {
            return gsl::span<const SampleMetadata>(m_SampleMetadata);
        }

        /// <summary>
        /// Adds the sample.
        /// </summary>
        /// <param name="sample">The sample.</param>
        /// <param name="metadata">The metadata.</param>
        void AddSample(DataType sample, SampleMetadata metadata) override
        {
            m_Data.push_back(sample);
            m_SampleMetadata.push_back(metadata);
        }

        /// <summary>
        /// Number of elements in dataset.
        /// </summary>
        /// <returns>Size</returns>
        size_t size() const override
        {
            return m_Data.size();
        }

        /// <summary>
        /// Checks, whether dataset is empty.
        /// </summary>
        /// <returns>True, if dataset is empty.</returns>
        bool empty() const override
        {
            return m_Data.empty();
        }

    private:
        std::vector<DataType> m_Data;
        std::vector<SampleMetadata> m_SampleMetadata;
        DatasetMetadata m_Metadata;
    };
}
