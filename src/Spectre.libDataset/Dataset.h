#pragma once

#include <vector>
#include "IDataset.h"

namespace Spectre::libDataset
{
    template<typename DataType, typename SampleMetadata, typename DatasetMetadata>
    class Dataset : public IDataset<DataType, SampleMetadata, DatasetMetadata>
    {
    public:
        Dataset(gsl::span<const DataTypeDT> data, gsl::span<const SampleMetadataDT> sampleMetadata, DatasetMetadata metadata) :
            m_data(data), m_sampleMetadata(sampleMetadata), m_metadata(metadata)
        {
        }

        virtual const DataTypeDT& operator[](int idx) const override
        {
            return m_data[idx];
        }

        virtual const SampleMetadataDT& getSampleMetadata(int idx) const override
        {
            return m_sampleMetadata[idx];
        }

        virtual const DatasetMetadata& getDatasetMetadata() const override
        {
            return m_metadata;
        }

        virtual DataTypeDT& operator[](int idx) override
        {
            return m_data[idx];
        }

        virtual SampleMetadataDT& getSampleMetadata(int idx) override
        {
            return m_sampleMetadata[idx];
        }

        virtual DatasetMetadata& getDatasetMetadata() override
        {
            return m_metadata;
        }

        virtual gsl::span<const DataTypeDT&> getData() const override
        {
            return gsl::span<const DataTypeDT&>(m_data);
        }

        virtual gsl::span<const SampleMetadataDT&> getSampleMetadata() const override
        {
            return gsl::span<const SampleMetadataDT&>(m_sampleMetadata);
        }

        virtual void setData(gsl::span<const DataTypeDT> data) override
        {
            m_data = data;
        }

        virtual void setSampleMetadata(gsl::span<const SampleMetadataDT> metadata) override
        {
            m_metadata = metadata;
        }

        virtual void addSample(DataTypeDT sample, SampleMetadataDT metadata) override
        {
            m_data.push_back(sample);
            m_sampleMetadata.push_back(metadata);
        }

        virtual const_iterator begin() const override
        {
            return gsl::span<const DataTypeDT>(m_data).begin();
        }

        virtual const_iterator end() const override
        {
            return gsl::span<const DataTypeDT>(m_data).end();
        }

        virtual iterator begin() override
        {
            return gsl::span<const DataTypeDT>(m_data).begin();
        }

        virtual iterator end() override
        {
            return gsl::span<const DataTypeDT>(m_data).end();
        }

        virtual size_t size() const override
        {
            return m_data.size();
        }

        virtual bool empty() const override
        {
            return m_data.empty();
        }

    private:
        std::vector<DataTypeDT> m_data;
        std::vector<SampleMetadataDT> m_sampleMetadata;
        DatasetMetadata m_metadata;
    };
}