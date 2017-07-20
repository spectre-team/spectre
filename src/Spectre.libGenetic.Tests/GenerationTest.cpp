/*
* GenerationTest.cpp
* Tests generation.
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

#define GTEST_LANG_CXX11 1

#include <gtest/gtest.h>
#include "Spectre.libGenetic/Generation.h"
#include "Spectre.libGenetic/InconsistentChromosomeLengthException.h"

namespace
{
using namespace Spectre::libGenetic;

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
        generation2(std::vector<Individual>(generation2Data))
    {}
protected:
	Generation generation1;
	Generation generation2;

	void SetUp() override
	{
		generation1 = Generation(std::vector<Individual>(generation1Data));
		generation2 = Generation(std::vector<Individual>(generation2Data));
	}
};

TEST_F(GenerationTest, add)
{
	Generation gen = generation1 + generation2;
	auto size = gen.size();
	EXPECT_EQ(size, 7);
}

TEST_F(GenerationTest, eq_add)
{
	Generation gen({ trueIndividual, trueIndividual, trueIndividual });
	gen += generation2;
	auto size = gen.size();
	EXPECT_EQ(size, 7);
}

TEST_F(GenerationTest, index)
{
	Individual ind1 = generation1[0];
	Individual ind2 = generation2[0];
	EXPECT_EQ(ind1, generation1Data);
	EXPECT_EQ(ind2, generation2Data);
}

}
