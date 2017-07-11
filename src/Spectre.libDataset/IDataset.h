#pragma once

#include "IReadOnlyDataset.h"

namespace Spectre::libDataset
{
    template <typename DataType, typename SampleMetadata, typename DatasetMetadata>
    class IDataset : public IReadOnlyDataset<DataType, SampleMetadata, DatasetMetadata>
    {
    public:
        virtual DataType& operator[](int idx) = 0;
        virtual SampleMetadata& getSampleMetadata(int idx) = 0;
        virtual DatasetMetadata& getDatasetMetadata() = 0;

        virtual void setData(gsl::span<const DataType> data) = 0;
        virtual void setSampleMetadata(gsl::span<const SampleMetadata> metadata) = 0;

        virtual void addSample(DataType sample, SampleMetadata metadata) = 0;
    };
}