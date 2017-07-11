#pragma once

#include <vector>
#include "IDataset.h"

namespace Spectre::libDataset
{
    template<typename DataType, typename SampleMetadata, typename DatasetMetadata>
    class Dataset : public IDataset<DataType, SampleMetadata, DatasetMetadata>
    {
    public:
        Dataset(gsl::span<const DataType> data, gsl::span<const SampleMetadata> sampleMetadata, DatasetMetadata metadata) :
            m_data(data.begin(), data.end()), m_sampleMetadata(sampleMetadata.begin(), sampleMetadata.end()), m_metadata(metadata)
        {
        }

        const DataType& operator[](int idx) const override
        {
            return m_data[idx];
        }

        const SampleMetadata& getSampleMetadata(int idx) const override
        {
            return m_sampleMetadata[idx];
        }

        const DatasetMetadata& getDatasetMetadata() const override
        {
            return m_metadata;
        }

        DataType& operator[](int idx) override
        {
            return m_data[idx];
        }

        SampleMetadata& getSampleMetadata(int idx) override
        {
            return m_sampleMetadata[idx];
        }

        DatasetMetadata& getDatasetMetadata() override
        {
            return m_metadata;
        }

        gsl::span<const DataType> getData() const override
        {
            return gsl::span<const DataType>(m_data);
        }

        gsl::span<const SampleMetadata> getSampleMetadata() const override
        {
            return gsl::span<const SampleMetadata>(m_sampleMetadata);
        }

        void setData(gsl::span<const DataType> data) override
        {
            m_data.assign(data.begin(), data.end());
        }

        void setSampleMetadata(gsl::span<const SampleMetadata> metadata) override
        {
            m_sampleMetadata.assign(metadata.begin(), metadata.end());
        }

        void addSample(DataType sample, SampleMetadata metadata) override
        {
            m_data.push_back(sample);
            m_sampleMetadata.push_back(metadata);
        }

        size_t size() const override
        {
            return m_data.size();
        }

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