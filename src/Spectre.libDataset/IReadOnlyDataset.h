#pragma once

#include <gsl.h>

namespace Spectre
{
    template<bool, typename T>
    struct deduce_helper
    {
        using type = typename T;
    };

    template<typename T>
    struct deduce_helper<false, T>
    {
        using type = typename const T&;
    };

    template<typename T>
    struct deduce : deduce_helper<std::is_fundamental<T>::value || std::is_enum<T>::value || std::is_pointer<T>::value, T>
    {};
}

namespace Spectre::libDataset
{
    template<typename DataType, typename SampleMetadata, typename DatasetMetadata>
    class IReadOnlyDataset
    {
    public:
        using DataTypeDT = typename deduce<DataType>::type;
        using SampleMetadataDT = typename deduce<SampleMetadata>::type;
        using const_iterator = typename gsl::span<const DataTypeDT>::const_iterator;

        virtual const DataTypeDT& operator[](int idx) const = 0;
        virtual const SampleMetadataDT& getSampleMetadata(int idx) const = 0;
        virtual const DatasetMetadata& getDatasetMetadata() const = 0;

        virtual gsl::span<const DataTypeDT&> getData() const = 0;
        virtual gsl::span<const SampleMetadataDT&> getSampleMetadata() const = 0;

        virtual const_iterator begin() const = 0;
        virtual const_iterator end() const = 0;
        virtual size_t size() const = 0;
        virtual bool empty() const = 0;

        virtual ~IReadOnlyDataset() = default;
    };
}