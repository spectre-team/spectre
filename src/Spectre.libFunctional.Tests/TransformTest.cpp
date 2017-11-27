/*
* TransformTest.cpp
* Tests transform function overloads.
*
Copyright 2017 Grzegorz Mrukwa

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

#include <gtest/gtest.h>
#include <gmock/gmock-matchers.h>
#include "Spectre.libException/InconsistentArgumentSizesException.h"
#include "Spectre.libFunctional/Transform.h"

namespace
{
using namespace testing;
using namespace Spectre::libException;
using namespace Spectre::libFunctional;

const std::vector<int> empty{};
const std::vector<int> ints{ 1,2,3 };
const std::vector<float> floats{ 1.f,2.f,3.f };

TEST(SingleInputVariableOutputTypeTransformTest, maps_sequence_with_output_type_change)
{
    const auto mapped = transform(gsl::as_span(ints), [](int i) {return static_cast<float>(i); }, static_cast<float*>(nullptr));
    EXPECT_THAT(mapped, ContainerEq(floats));
}

TEST(SingleInputVariableOutputTypeTransformTest, returns_empty_for_empty)
{
    const auto mapped = transform(gsl::as_span(empty), [](int i) {return static_cast<float>(i); }, static_cast<float*>(nullptr));
    EXPECT_EQ(mapped.size(), 0);
}

TEST(SingleInputFixedOutputTypeTransformTest, maps_sequence_within_the_same_type)
{
    const std::vector<int> squares{ 1,4,9 };
    const auto mapped = transform(gsl::as_span(ints), [](int i) {return i*i; });
    EXPECT_THAT(mapped, ContainerEq(squares));
}

TEST(SingleInputFixedOutputTypeTransformTest, returns_empty_for_empty)
{
    const auto mapped = transform(gsl::as_span(empty), [](int i) {return i*i; });
    EXPECT_EQ(mapped.size(), 0);
}

TEST(TwoInputVariableOutputTypeTransformTest, maps_sequences_to_common_output_type)
{
    const std::vector<double> squares{ 1.,4.,9. };
    const auto mapped = transform(
        gsl::as_span(ints),
        gsl::as_span(floats),
        [](int i, float f) {return static_cast<double>(i*f); },
        static_cast<double*>(nullptr)
    );
    EXPECT_THAT(mapped, ContainerEq(squares));
}

TEST(TwoInputVariableOutputTypeTransformTest, returns_empty_for_empty)
{
    const auto mapped = transform(
        gsl::as_span(empty),
        gsl::as_span(empty),
        [](int i, int f) {return static_cast<double>(i*f); },
        static_cast<double*>(nullptr)
    );
    EXPECT_EQ(mapped.size(), 0);
}

TEST(TwoInputVariableOutputTypeTransformTest, throws_on_inconsistent_sizes)
{
    EXPECT_THROW(\
        transform(\
            gsl::as_span(empty), \
            gsl::as_span(floats), \
            [](int i, float f) {return static_cast<double>(i*f); }, \
            static_cast<double*>(nullptr)), \
        InconsistentArgumentSizesException);
}

TEST(TwoInputConstantTypeTransformTest, maps_sequences_within_the_same_type)
{
    const std::vector<int> squares{ 1,4,9 };
    const auto mapped = transform(
        gsl::as_span(ints),
        gsl::as_span(ints),
        [](int i, int j) {return i*j; }
    );
    EXPECT_THAT(mapped, ContainerEq(squares));
}

TEST(TwoInputConstantTypeTransformTest, returns_empty_for_empty)
{
    const auto mapped = transform(
        gsl::as_span(empty),
        gsl::as_span(empty),
        [](int i, int j) {return i*j; }
    );
    EXPECT_EQ(mapped.size(), 0);
}

TEST(TwoInputConstantTypeTransformTest, throws_on_inconsistent_sizes)
{
    EXPECT_THROW(\
        transform(\
            gsl::as_span(empty), \
            gsl::as_span(ints), \
            [](int i, int j) {return i*j; }), \
        InconsistentArgumentSizesException);
}
}
