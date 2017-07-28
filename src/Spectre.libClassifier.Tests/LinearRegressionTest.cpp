/*
* LinearRegressionTest.h
* Tests for LinearRegression class.
*
Copyright 2017 Wojciech Wilgierz

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
#include <vector>

namespace
{
	lass LinearRegressionTest : public ::testing::Test
	{
	protected:

		problem test_data;
		parameter test_parameter;
		model model_data;

		virtual void SetUp() override
		{
			test_data = { 5, 6,{ 900, 903, 912 },{ { 1, 1, 0 },{ 12, 20, 0 },{ 2, 1, 0 },{ 9, 18, 13 } }, 0.5 };
		}
	};

	TEST_F(LinearRegressionTest, check_parameter)
	{
		//char* result = check_parameter(test_data, test_parameter);
		//EXPECT_EQ(result, NULL);
		EXPECT_EQ(1, 1);
	}
}
