#pragma once

#include "../../Spectre.libDivik/DivikResultStruct.h"	// @dkuchta: TODO: Examine why includes ignore $(SolutionDir) 
#include <memory>

using namespace NUnit::Framework;
using DivikResultCli = Spectre::AlgorithmsCli::Results::DivikResult;

namespace Spectre::AlgorithmsCli::Tests::Results
{
	[TestFixture, Category("Algorithm")]
	public ref class DivikResultTests
	{
	private:
		DivikResultStruct* _defaultNative;
		DivikResultCli^ _result;
	public:
		[OneTimeSetUp]
		void SetUpClass()
		{
			_defaultNative = new DivikResultStruct();
			_result = gcnew DivikResultCli(_defaultNative);
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
			auto requiredQualityIndex = 0.95;
			DivikResultStruct native;
			native.qualityIndex = requiredQualityIndex;
			DivikResultCli^ _newResult = gcnew DivikResultCli(&native);
			Assert::AreEqual(_newResult->QualityIndex, requiredQualityIndex, "Data differs between native and CLI class");
		}

		[Test]
		void EqualsTest()
		{
			Assert::True(_result->Equals(_result), "Same instance marked nonequal.");
			DivikResultCli^ _nextResult;
			
			_nextResult = gcnew DivikResultCli(_defaultNative);
			Assert::True(_result->Equals(_nextResult), "Mirror instance marked not equal.");
			
			DivikResultStruct nativeQualityChanged;
			nativeQualityChanged.qualityIndex = _result->QualityIndex + 1;
			_nextResult = gcnew DivikResultCli(&nativeQualityChanged);
			Assert::False(_result->Equals(_nextResult), "Results with different quality indeces marked equal.");

			DivikResultStruct nativePartitionChanged;
			nativePartitionChanged.partition.push_back(int());
			_nextResult = gcnew DivikResultCli(&nativePartitionChanged);
			Assert::False(_result->Equals(_nextResult), "Results with different partitions marked equal.");
		}

		[Test]
		void HashCodeTest()
		{
			DivikResultCli^ _nextResult = gcnew DivikResultCli(_defaultNative);
			Assert::AreEqual(_result->GetHashCode(), _nextResult->GetHashCode(), "Mirror instances have different hash codes.");

			DivikResultStruct nativeQualityChanged;
			nativeQualityChanged.qualityIndex = _result->QualityIndex + 1;
			_nextResult = gcnew DivikResultCli(&nativeQualityChanged);
			Assert::AreNotEqual(_result->GetHashCode(), _nextResult->GetHashCode(), "Different instances have equal hash codes.");
		}

	};
}
