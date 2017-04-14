#pragma once

using System::String;
using namespace NUnit::Framework;
using Spectre::AlgorithmsCli::Methods::Segmentation;
using Spectre::Data::Datasets::BasicTextDataset;
using Spectre::Algorithms::Parameterization::DivikOptions;

namespace Spectre::AlgorithmsCli::Tests::Methods
{
	[TestFixture, Category("Algorithm")]
	public ref class SegmentationTests
	{
	private:
		Segmentation^ _segmentation;
		String^ TestDirectory = TestContext::CurrentContext->TestDirectory + "\\..\\..\\..\\..\\..\\test_files";
	public:
		[OneTimeSetUp]
		void SetUpClass()
		{
			_segmentation = gcnew Segmentation();
		}

		[OneTimeTearDown]
		void TearDownClass()
		{
			delete _segmentation;
			_segmentation = nullptr;
		}

		[Test]
		void DivikSimple()
		{
			auto mz = gcnew array<double>{ 1, 2, 3, 4 };
			auto data = gcnew array<double,2>{ { 1, 1, 1, 1 },
											   { 2, 2, 2, 2 },
											   { 2, 2, 2, 2 },
											   { 1, 1, 1, 1 } };
			auto coordinates = gcnew array<int,2>{ { 1, 1 }, { 2, 2 }, { 1, 2 }, { 2, 1 } };
			auto dataset = gcnew BasicTextDataset(mz, data, coordinates);

			auto options = DivikOptions::ForLevels(1);
			options.UsingVarianceFiltration = false;
			options.UsingAmplitudeFiltration = false;
			options.MaxK = 2;
			options.Metric = Spectre::Algorithms::Parameterization::Metric::Euclidean;

			auto result = _segmentation->Divik(dataset, options);

			Assert::IsNotNull(result);
		}

		[Test, Category("VeryLong")]
		void DivikBigData()
		{
			// path to directory with test project
			auto path = TestDirectory + "\\hnc1_tumor.txt";
			auto dataset = gcnew BasicTextDataset(path);
			auto options = DivikOptions::ForLevels(2);

			auto result = _segmentation->Divik(dataset, options);

			Assert::IsNotNull(result);
		}
	};
}