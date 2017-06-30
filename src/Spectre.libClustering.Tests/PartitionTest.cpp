#define GTEST_LANG_CXX11 1

#include <gtest\gtest.h>
#include <gtest\gtest-spi.h>
#include <gsl.h>
#include <vector>
#include <algorithm>
#include "Partition.h"

namespace Spectre::libClustering::Tests
{
	class PartitionTest : public ::testing::Test
	{
	protected:

		std::vector<unsigned int> testData;

		virtual void SetUp()
		{
			testData = { 1, 2, 3, 4, 5, 6, 7, 8 };
		}

	};

	TEST_F(PartitionTest, NotFailsOnNull)
	{
		ASSERT_NO_FATAL_FAILURE({
			Partition test(nullptr);
		}) 
			<< "Creation from null data caused fatal failure.";
	}

	TEST_F(PartitionTest, SimplifyToNotEmpty)
	{
		Partition test(testData);
		auto result = test.Get();
		ASSERT_NE(result.size(), 0)
			<< "Sequence has been simplified to empty partition.";
	}

	TEST_F(PartitionTest, SimplifySortsLabels)
	{
		Partition test(testData);
		auto result = test.Get();
		ASSERT_TRUE(std::is_sorted(result.begin(), result.end()))
			<< "Simplification does not sort the labels.";
	}

	TEST_F(PartitionTest, ComparisonDifferentLengths)
	{
		Partition test1(testData);
		Partition test2(std::vector<unsigned int>(testData.begin(), testData.end() - 1));
		ASSERT_NE(test1, test2)
			<< "Marked equal on partition length mismatch.";
	}

	TEST_F(PartitionTest, ComparisonEqualPartitionsNoTolerance)
	{
		Partition test(testData);
		ASSERT_EQ(test, test)
			<< "Marked not equal on comparison of the same instance.";
	}

	TEST_F(PartitionTest, ComparisonNotEqualPartitionsNoTolerance)
	{
		Partition test1(testData);
		std::vector<unsigned int> testData2 = { 1, 2, 3, 3, 4, 5, 6, 7 };
		Partition test2(testData2);
		ASSERT_NE(test1, test2)
			<< "Marked equal on comparison of different data.";
	}

	TEST_F(PartitionTest, ComparisonEqualPartitionsMixedLabelsNoTolerance)
	{
		Partition test1(testData);
		std::vector<unsigned int> testData2 = { 8, 7, 6, 5, 4, 3, 2, 1 };
		Partition test2(testData2);
		ASSERT_EQ(test1, test2)
			<< "Comparison is label-sensitive.";
	}

	TEST_F(PartitionTest, ComparisonEqualPartitionsWithTolerance)
	{
		Partition test1(testData);
		std::vector<unsigned int> testData2 = testData;
		testData2.back() -= 1;
		Partition test2(testData2);
		ASSERT_TRUE(Partition::Compare(test1, test2, 0.2))
			<< "Comparison is tolerance insensitive.";
	}

	TEST_F(PartitionTest, ComparisonNotEqualPartitionsWithTolerance)
	{
		Partition test1(testData);
		std::vector<unsigned int> testData2 = { 2, 3, 3, 5, 5, 7, 7, 9 };
		Partition test2(testData2);
		ASSERT_FALSE(Partition::Compare(test1, test2, 0.2))
			<< "Comparison did not capture inequality.";
	}

	TEST_F(PartitionTest, ComparisonEqualPartitionsMixedLabelsWithTolerance)
	{
		Partition test1(testData);
		std::vector<unsigned int> testData2 = { 8, 7, 6, 5, 4, 3, 2, 2 };
		Partition test2(testData2);
		ASSERT_TRUE(Partition::Compare(test1, test2, 0.2))
			<< "Comparison is tolerance insensitive or label sensitive.";
	}
}

