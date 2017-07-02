#pragma once

#include "IReadOnlyDataset.h"

namespace Spectre::libDataset
{
    template <typename DataType, typename SampleMetadata, typename DatasetMetadata>
    class IDataset : public IReadOnlyDataset<DataType, SampleMetadata, DatasetMetadata>
    {
    public:
        using iterator = typename gsl::span<DataTypeDT>::iterator;

        virtual DataTypeDT& operator[](int idx) = 0;
        virtual SampleMetadataDT& getSampleMetadata(int idx) = 0;
        virtual DatasetMetadata& getDatasetMetadata() = 0;

        virtual void setData(gsl::span<const DataTypeDT> data) = 0;
        virtual void setSampleMetadata(gsl::span<const SampleMetadataDT> metadata) = 0;

        virtual void addSample(DataTypeDT sample, SampleMetadataDT metadata) = 0;

        virtual iterator begin() = 0;
        virtual iterator end() = 0;
    };
}