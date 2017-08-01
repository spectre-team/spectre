/*
* PartitionTest.h
* Tests for Partition class.
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

#define GTEST_LANG_CXX11 1

#include <gtest/gtest.h>
#include <gsl.h>
#include <vector>
#include <algorithm>
#include "Partition.h"
#include "ArgumentNullException.h"


namespace Spectre::libClustering::Tests
{
	class PartitionTest : public ::testing::Test
	{
	protected:

		std::vector<unsigned int> testData;

		virtual void SetUp() override
		{
			testData = { 1, 2, 3, 4, 5, 6, 7, 8 };
		}

	};

	TEST_F(PartitionTest, fails_on_null)
	{
		ASSERT_THROW({
			Partition test(nullptr);
		}, ArgumentNullException) 
			<< "Creation from null data did not throw exception.";
	}

	TEST_F(PartitionTest, simplify_to_not_empty)
	{
		Partition test(testData);
		auto result = test.Get();
		ASSERT_NE(result.size(), 0)
			<< "Sequence has been simplified to empty partition.";
	}

	TEST_F(PartitionTest, simplify_sorts_labels)
	{
		auto unsorted = std::vector<unsigned int>({ 5, 6, 8, 4, 1, 2, 7, 3 });
		Partition test(unsorted);
		auto result = test.Get();
		ASSERT_TRUE(std::is_sorted(result.begin(), result.end()))
			<< "Simplification does not sort the labels.";
	}

	TEST_F(PartitionTest, comparison_different_lengths)
	{
		Partition test1(testData);
		auto shorter = std::vector<unsigned int>(testData.begin(), testData.end() - 1);
		Partition test2(shorter);
		ASSERT_NE(test1, test2)
			<< "Marked equal on partition length mismatch.";
	}

	TEST_F(PartitionTest, comparison_equal_partitions_no_tolerance)
	{
		Partition test(testData);
		ASSERT_EQ(test, test)
			<< "Marked not equal on comparison of the same instance.";
	}

	TEST_F(PartitionTest, comparison_not_equal_partitions_no_tolerance)
	{
		Partition test1(testData);
		std::vector<unsigned int> testData2 = { 1, 2, 3, 3, 4, 5, 6, 7 };
		Partition test2(testData2);
		ASSERT_NE(test1, test2)
			<< "Marked equal on comparison of different data.";
	}

	TEST_F(PartitionTest, comparison_equal_partitions_mixed_labels_no_tolerance)
	{
		Partition test1(testData);
		std::vector<unsigned int> testData2 = { 8, 7, 6, 5, 4, 3, 2, 1 };
		Partition test2(testData2);
		ASSERT_EQ(test1, test2)
			<< "Comparison is label-sensitive.";
	}

	TEST_F(PartitionTest, comparison_equal_partitions_with_tolerance)
	{
		Partition test1(testData);
		std::vector<unsigned int> testData2 = testData;
		testData2.back() -= 1;
		Partition test2(testData2);
		ASSERT_TRUE(Partition::Compare(test1, test2, 0.2))
			<< "Comparison is tolerance insensitive.";
	}

	TEST_F(PartitionTest, comparison_not_equal_partitions_with_tolerance)
	{
		Partition test1(testData);
		std::vector<unsigned int> testData2 = { 2, 3, 3, 5, 5, 7, 7, 9 };
		Partition test2(testData2);
		ASSERT_FALSE(Partition::Compare(test1, test2, 0.2))
			<< "Comparison did not capture inequality.";
	}

	TEST_F(PartitionTest, comparison_equal_partitions_mixed_labels_with_tolerance)
	{
		Partition test1(testData);
		std::vector<unsigned int> testData2 = { 8, 7, 6, 5, 4, 3, 2, 2 };
		Partition test2(testData2);
		ASSERT_TRUE(Partition::Compare(test1, test2, 0.2))
			<< "Comparison is tolerance insensitive or label sensitive.";
	}
}

