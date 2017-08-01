#pragma once

#include "../../Spectre.libDivik/DivikResultStruct.h"	//TODO
#include <memory>

using namespace NUnit::Framework;

namespace Spectre::AlgorithmsCli::Tests::Results
{
	[TestFixture, Category("Algorithm")]
	public ref class DivikResultTests
	{
	private:
		DivikResult^ _result;
	public:
		[OneTimeSetUp]
		void SetUpClass()
		{
			_result = gcnew DivikResult();
		}

		[OneTimeTearDown]
		void TearDownClass()
		{
			delete _result;
			_result = nullptr;
		}

		[Test]
		void ConstructFromNativeTest()
		{
			//TODO
			Assert::True(true);
		}

		[Test]
		void EqualsTest()
		{
			Assert::True(_result->Equals(_result), "Same instance marked nonequal.");
			DivikResult^ _nextResult;
			
			_nextResult = gcnew DivikResult();
			Assert::True(_result->Equals(_nextResult), "Mirror instance marked not equal.");
			
			_nextResult->QualityIndex = _result->QualityIndex + 1;
			Assert::False(_result->Equals(_nextResult), "Results with different quality indeces marked equal.");

			_nextResult = gcnew DivikResult();
			_nextResult->Partition = gcnew cli::array<int>(_nextResult->Partition->Length);
			Assert::False(_result->Equals(_nextResult), "Results with different partitions marked equal.");
		}

		[Test]
		void HashCodeTest()
		{
			DivikResult^ _nextResult = gcnew DivikResult();
			Assert::AreEqual(_result->GetHashCode(), _nextResult->GetHashCode(), "Mirror instances have different hash codes.");

			_nextResult->QualityIndex = _result->QualityIndex + 1;
			Assert::AreNotEqual(_result->GetHashCode(), _nextResult->GetHashCode(), "Different instances have equal hash codes.");
		}

	};
}