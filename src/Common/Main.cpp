#define GTEST_LANG_CXX11 1

#include <gtest\gtest.h>

int main(int argc, char **argv) 
{
	::testing::InitGoogleTest(&argc, argv);
	int testsResult = RUN_ALL_TESTS();
	return testsResult;
}
