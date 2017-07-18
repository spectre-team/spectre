#pragma once

namespace Spectre::libGenetic
{
class StopCondition
{
public:
    StopCondition(StopCondition&&) = default;
    explicit StopCondition(unsigned int numberOfIterations);
    bool operator()();
    virtual ~StopCondition() = default;
private:
    unsigned int m_RemainingIterations;
};
}