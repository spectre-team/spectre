#define GTEST_LANG_CXX11 1

#include <gtest/gtest.h>

namespace Spectre::libClassifier::Tests
{
	class LinearRegressionTest : public ::testing::Test
	{
	protected:

		std::vector<unsigned int> testData;

		virtual void SetUp() override
		{
			testData = { 1, 2, 3, 4, 5, 6, 7, 8 };
		}

	};

	TEST_F(LinearRegressionTest, fails_on_null)
	{
		FAIL();
	}
}