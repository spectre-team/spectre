/*
* Partition.h
* Provides class implementation that simplifies partition vectors.
*
Copyright 2017 Dariusz Kuchta

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

#include "Partition.h"
#include "ArgumentNullException.h"

#include <map>

namespace Spectre::libClustering
{
Partition::Partition(gsl::span<unsigned int> partition) : m_Partition(partition.size())
{
    if (partition.data() == nullptr)
        throw ArgumentNullException("partition");

    std::map<unsigned int, int> labelsDictionary;

    int currentIntLabel = 1;
    for (auto label : partition)
        if (labelsDictionary.find(label) == labelsDictionary.end())
            labelsDictionary.emplace(label, currentIntLabel++);

    for (int i = 0; i < partition.size(); i++)
        m_Partition[i] = labelsDictionary[partition[i]];
}

const std::vector<unsigned int>& Partition::Get() const
{
    return m_Partition;
}

bool Partition::Compare(const Partition &lhs, const Partition &rhs, double tolerance)
{
    auto &lhsPartition = lhs.Get();
    auto &rhsPartition = rhs.Get();

    if (lhsPartition.size() != rhsPartition.size())
        return false;

    unsigned int matchesCount = 0;
    size_t length = lhsPartition.size();

    for (int i = 0; i < length; i++)
        if (lhsPartition[i] == rhsPartition[i])
            ++matchesCount;

    double compatibilityRate = matchesCount / static_cast<double>(length);
    double requiredCompatibilityRate = 1 - tolerance;

    return requiredCompatibilityRate <= compatibilityRate;
}

bool Partition::operator==(const Partition &other) const
{
    return Compare(*this, other);
}

bool Partition::operator!=(const Partition &other) const
{
    return !(*this == other);
}
}
