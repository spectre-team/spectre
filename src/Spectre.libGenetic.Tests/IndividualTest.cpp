/*
* IndividualTest.cpp
* Tests individual.
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
#include "Spectre.libGenetic/Individual.h"

namespace
{
using namespace Spectre::libGenetic;

TEST(IndividualInitialization, initializes)
{
	Individual true_individual({ true, true, true, true });
	Individual false_individual({ false, false, false, false });
	Individual diff_individual({ true, false, true, false });
}

class IndividualTest : public ::testing::Test
{
public:
	IndividualTest() {}
protected:
	const std::vector<bool> TRUE_VECTOR = { true, true, true, true };
	const std::vector<bool> FALSE_VECTOR = { false, false, false, false };
	const std::vector<bool> DIFF_VECTOR = { true, false, true, false };
	const Individual true_individual, false_individual, diff_individual;

	void SetUp() override
	{
		true_individual = Individual(TRUE_VECTOR);
		false_individual = Individual(FALSE_VECTOR);
		diff_individual = Individual(DIFF_VECTOR);
	}
};

TEST_F(IndividualTest, throws_on_inconsistent_sizes)
{
	int size = true_individual.size();
	EXPECT_EQ(size, 4);
}

TEST_F(IndividualTest, index)
{
	bool b1 = diff_individual[0];
	bool b2 = diff_individual[1];
	bool b3 = diff_individual[2];
	bool b4 = diff_individual[3];
	EXPECT_EQ(b1, true);
	EXPECT_EQ(b2, false);
	EXPECT_EQ(b3, true);
	EXPECT_EQ(b4, false);
}

}
