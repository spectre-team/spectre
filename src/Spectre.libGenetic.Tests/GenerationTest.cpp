/*
* GenerationTest.cpp
* Tests generation.
*
Copyright 2017 Spectre Team

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
#include "Spectre.libException/OutOfRangeException.h"
#include "Spectre.libGenetic/Generation.h"
#include "Spectre.libGenetic/InconsistentChromosomeLengthException.h"

namespace
{
using namespace Spectre::libGenetic;
using namespace Spectre::libException;

const auto seed = 0ul;

TEST(GenerationInitialization, initializes)
{
    const auto generationSize = 5u;
    const auto individualSize = 6u;
    const auto initialFillup = 4u;
    EXPECT_NO_THROW(Generation(generationSize, individualSize, initialFillup, seed));
}

TEST(GenerationInitialization, throws_when_required_number_of_active_is_bigger_than_length)
{
    const auto generationSize = 5u;
    const auto individualSize = 4u;
    const auto excessiveInitialFillup = 6u;
    EXPECT_THROW(Generation(generationSize, individualSize, excessiveInitialFillup, seed), ArgumentOutOfRangeException<size_t>);
}

TEST(GenerationInitialization, initializes_with_proper_size)
{
    const auto generationSize = 8u;
    const auto individualSize = 10u;
    const auto initialFillup = 6u;
    Generation generation(generationSize, individualSize, initialFillup, seed);
    EXPECT_EQ(generation.size(), generationSize);
}

class GenerationInitializationTest: public ::testing::Test
{
protected:
    const Individual smallerIndividual = Individual({ true, false, true });
    const Individual trueIndividual = Individual({ true, true, true, true });
    const Individual falseIndividual = Individual({ false, false, false, false });
    const std::vector<Individual> inconsistentGeneration = { trueIndividual, smallerIndividual, trueIndividual };
    const std::vector<Individual> generation1Data = { trueIndividual, trueIndividual, trueIndividual };
    const std::vector<Individual> generation2Data = { falseIndividual, falseIndividual, falseIndividual, falseIndividual };
};

TEST_F(GenerationInitializationTest, initializes)
{
    EXPECT_NO_THROW(Generation(std::vector<Individual>(generation1Data)));
}

TEST_F(GenerationInitializationTest, throws_on_inconsistent_chromosome_lengths)
{
    EXPECT_THROW(Generation(std::vector<Individual>(inconsistentGeneration)), InconsistentChromosomeLengthException);
}

class GenerationTest : public GenerationInitializationTest
{
public:
    GenerationTest():
        generation1(std::vector<Individual>(generation1Data)),
        generation2(std::vector<Individual>(generation2Data)) {}

protected:
    Generation generation1;
    Generation generation2;

    void SetUp() override
    {
        generation1 = Generation(std::vector<Individual>(generation1Data));
        generation2 = Generation(std::vector<Individual>(generation2Data));
    }
};

TEST_F(GenerationTest, give_proper_size)
{
    EXPECT_EQ(generation1.size(), generation1Data.size());
    EXPECT_EQ(generation2.size(), generation2Data.size());
}

TEST_F(GenerationTest, addition_produces_generation_of_proper_size)
{
    const auto generation = generation1 + generation2;
    const auto size = generation.size();
    EXPECT_EQ(size, generation1.size() + generation2.size());
}

TEST_F(GenerationTest, addition_produces_generation_of_elements_from_first_and_then_second)
{
    const auto generation = Generation({ trueIndividual }) + Generation({ falseIndividual });
    const auto first = generation[0];
    const auto second = generation[1];
    EXPECT_EQ(first, trueIndividual);
    EXPECT_EQ(second, falseIndividual);
}

TEST_F(GenerationTest, addition_throws_on_inconsistent_chromosome_size)
{
    const Generation shorters({ Individual({}) });
    const Generation longers({ Individual({ true }) });

    EXPECT_THROW(shorters + longers, InconsistentChromosomeLengthException);
}

TEST_F(GenerationTest, plus_equal_extends_instance_to_proper_size)
{
    Generation generation({ trueIndividual, trueIndividual, trueIndividual });
    const auto initialSize = generation.size();
    generation += generation2;
    const auto size = generation.size();
    EXPECT_EQ(size, initialSize + generation2.size());
}

TEST_F(GenerationTest, plus_equal_extends_instance_with_elements_of_second)
{
    Generation falseGeneration({ Individual({ false }) });
    const Generation trueGeneration({ Individual({ true }) });

    falseGeneration += trueGeneration;

    EXPECT_EQ(falseGeneration[1], trueGeneration[0]);
}

TEST_F(GenerationTest, plus_equal_throws_on_inconsistent_chromosome_size)
{
    Generation shorters({ Individual({}) });
    const Generation longers({ Individual({ true }) });

    EXPECT_THROW(shorters += longers, InconsistentChromosomeLengthException);
}

TEST_F(GenerationTest, immutable_index_throws_on_exceeded_size)
{
    EXPECT_THROW(generation1[generation1.size()], OutOfRangeException);
}

TEST_F(GenerationTest, mutable_index_throws_on_exceeded_size)
{
    Generation generation(std::vector<Individual> { generation1Data });
    EXPECT_THROW(generation[generation.size()] = generation1Data[0], OutOfRangeException);
}

TEST_F(GenerationTest, immutable_index_allows_read_only_access_to_individuals)
{
    const auto &ind1 = generation1[0];
    const auto &ind2 = generation2[0];
    EXPECT_EQ(ind1, generation1Data[0]);
    EXPECT_EQ(ind2, generation2Data[0]);
}

TEST_F(GenerationTest, mutable_index_allows_full_access_to_individuals)
{
    ASSERT_NE(generation1Data[0], generation2Data[1]);
    generation1[0] = generation2Data[1];
    EXPECT_EQ(generation1[0], generation2Data[1]);
    EXPECT_NE(generation1[0], generation1Data[0]);
}

TEST_F(GenerationTest, mutable_index_throws_on_inconsistent_chromosome_length)
{
    // @gmrukwa: This is realized by assignment operator, however this test is for double-check
    Individual tooShortIndividual(std::vector<bool>(generation1Data.size() - 1, false));
    EXPECT_THROW(generation1[0] = tooShortIndividual, InconsistentChromosomeLengthException);
}

TEST_F(GenerationTest, iterators_allow_to_iterate_the_individuals)
{
    auto generationIterator = generation1.begin();
    auto dataIterator = generation1Data.begin();

    while (generationIterator != generation1.end() && dataIterator != generation1Data.end())
    {
        const auto generationIndividual = *generationIterator;
        const auto dataIndividual = *dataIterator;
        for (auto i = 0u; i < generationIndividual.size(); ++i)
        {
            EXPECT_EQ(generationIndividual[i], dataIndividual[i]);
        }
        ++generationIterator;
        ++dataIterator;
    }

    EXPECT_EQ(generationIterator, generation1.end());
    EXPECT_EQ(dataIterator, generation1Data.end());
}

}
