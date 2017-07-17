#pragma once
#include <vector>
#include <numeric>
#include <algorithm>
#include <span.h>

namespace Spectre::libGenetic
{
class Sorting
{
public:
    template <class T>
    static std::vector<size_t> indices(gsl::span<T> data)
    {
        std::vector<size_t> indices(data.size());
        std::iota(indices.begin(), indices.end(), 0);
        std::sort(indices.begin(), indices.end(), [&data](size_t first, size_t second) { return data[second] < data[first]; });
        return indices;
    }
};
}
