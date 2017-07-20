/*
* ScorerTest.cpp
* Tests Scorer.
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
#include <memory>
#include "Spectre.libGenetic/FitnessFunction.h"
#include "Spectre.libGenetic/Scorer.h"
#include "Spectre.libException/NullPointerException.h"

namespace
{
using namespace Spectre::libGenetic;

TEST(Scorer, initializes)
{
	EXPECT_THROW(Scorer(nullptr), Spectre::libException::NullPointerException);
}

class ScorerTest : public ::testing::Test
{
public:
	ScorerTest() {}
protected:
	std::unique_ptr<FitnessFunction> fitness;

	void SetUp() override
	{
		fitness = FitnessFunction();
	}
};

TEST_F(ScorerTest, score_generation)
{
	const Individual true_individual = Individual({ true, true, true, true });
	Generation gen = Generation({ true_individual, true_individual, true_individual });
	EXPECT_NO_THROW(fitness.Score(gen));
}
}
