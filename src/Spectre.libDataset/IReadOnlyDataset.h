#pragma once

#include <gsl.h>

namespace Spectre::libDataset
{
    template<typename DataType, typename SampleMetadata, typename DatasetMetadata>
    class IReadOnlyDataset
    {
    public:
        virtual const DataType& operator[](int idx) const = 0;
        virtual const SampleMetadata& getSampleMetadata(int idx) const = 0;
        virtual const DatasetMetadata& getDatasetMetadata() const = 0;

        virtual gsl::span<const DataType> getData() const = 0;
        virtual gsl::span<const SampleMetadata> getSampleMetadata() const = 0;

        virtual size_t size() const = 0;
        virtual bool empty() const = 0;

        virtual ~IReadOnlyDataset() = default;
    };
}