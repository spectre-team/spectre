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

namespace
{
using namespace Spectre::libGenetic;

class GenerationTest : public ::testing::Test
{
public:
	GenerationTest() {}
protected:
	const Individual smaller_individual = Individual({ true, false, true });
	const Individual true_individual = Individual({ true, true, true, true });
	const Individual false_individual = Individual({ false, false, false, false });
	std::vector<Individual> inconsistent_gen = { true_individual, smaller_individual, true_individual };
	std::vector<Individual> gen1 = { true_individual, true_individual, true_individual };
	std::vector<Individual> gen2 = { false_individual, false_individual, false_individual, false_individual };
	const Generation generation1;
	const Generation generation2;

	void SetUp() override
	{
		generation1 = Generation(gen1);
		generation2 = Generation(gen2);
	}
};

TEST_F(GenerationTest, initializes)
{
	EXPECT_NO_THROW(Generation(gen1));
}

TEST_F(GenerationTest, error_initialization)
{
	EXPECT_THROW(Generation(inconsistent_gen), std::exception);
}

TEST_F(GenerationTest, add)
{
	Generation gen = generation1 + generation2;
	int size = gen.size();
	EXPECT_EQ(size, 7);
}

TEST_F(GenerationTest, eq_add)
{
	Generation gen = Generation({ true_individual, true_individual, true_individual });
	gen += generation2;
	int size = gen.size();
	EXPECT_EQ(size, 7);
}

TEST_F(GenerationTest, index)
{
	Individual ind1 = generation1[0];
	Individual ind2 = generation2[0];
	EXPECT_EQ(ind1, gen1);
	EXPECT_EQ(ind2, gen2);
}

}
