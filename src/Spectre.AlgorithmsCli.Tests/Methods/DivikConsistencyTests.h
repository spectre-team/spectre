#pragma once

using System::String;
using System::IO::File;
using namespace NUnit::Framework;
using namespace Newtonsoft::Json;
using Spectre::AlgorithmsCli::Methods::Segmentation;
using Spectre::Algorithms::Methods::Utils::Partition;
using Spectre::Data::Datasets::BasicTextDataset;
using Spectre::Algorithms::Parameterization::DivikOptions;
using Spectre::Algorithms::Results::DivikResult;

namespace Spectre::AlgorithmsCli::Tests::Methods
{
	[TestFixture, Category("Algorithm")]
	public ref class DivikConsistencyTests
	{
	private:
		Segmentation^ _segmentation;
		String^ _testDirectory = TestContext::CurrentContext->TestDirectory + "\\..\\..\\..\\test_files";
		bool _equalOnTop;
		bool _equalDownmerged;
		void TestForDataset(String^ dataPath, String^ resultPath, DivikOptions options, bool checkNested)
		{
			auto referenceJson = File::ReadAllText(resultPath);
			auto referenceResult = JsonConvert::DeserializeObject<DivikResult^>(referenceJson);

			auto dataset = gcnew BasicTextDataset(dataPath);

			Service::ConsoleCaptureService^ captureService;
			DivikResult^ result;
			try
			{
				captureService = gcnew Service::ConsoleCaptureService(1000);
				captureService->Written += gcnew System::EventHandler<System::String ^>(this, &Spectre::AlgorithmsCli::Tests::Methods::DivikConsistencyTests::OnWritten);
				result = _segmentation->Divik(dataset, options);
			}
			finally
			{
				delete captureService;
				captureService = nullptr;
			}

			_equalOnTop = Partition::Compare<System::Int32, System::Int32>(result->Partition, referenceResult->Partition, 0);
			
			if (checkNested)
			{
				_equalDownmerged = Partition::Compare<System::Int32, System::Int32>(result->Merged, referenceResult->Merged, 0);
				Assert::Multiple(gcnew TestDelegate(this, &Spectre::AlgorithmsCli::Tests::Methods::DivikConsistencyTests::AssertBothEqualities));
			}
			else
				Assert::True(_equalOnTop, "Not equal on top.");
		}
		void OnWritten(System::Object^, System::String ^e)
		{
			System::Diagnostics::Debug::Write(e);
		}
		void AssertBothEqualities()
		{
			Assert::True(_equalOnTop, "Not equal on top.");
			Assert::True(_equalDownmerged, "Not equal in nested.");
		}
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
				
		[Test, Category("VeryLong")]
		void BigDataSetEuclideanTest()
		{
			auto datasetFilename = _testDirectory + "\\single.txt";
			auto resultPath = _testDirectory + "\\expected_divik_results\\single\\euclidean\\divik-result.json";

			auto options = DivikOptions::ForLevels(2);
			options.Metric = Spectre::Algorithms::Parameterization::Metric::Euclidean;

			TestForDataset(datasetFilename, resultPath, options, true);
		}

		[Test, Category("VeryLong")]
		void BigDataSetPearsonTest()
		{
			auto datasetFilename = _testDirectory + "\\single.txt";
			auto resultPath = _testDirectory + "\\expected_divik_results\\single\\pearson\\divik-result.json";

			auto options = DivikOptions::ForLevels(2);

			TestForDataset(datasetFilename, resultPath, options, true);
		}

		[Test, Category("VeryLong")]
		void BigDataSetPearsonNoFiltersTest()
		{
			auto datasetFilename = _testDirectory + "\\single.txt";
			auto resultPath = _testDirectory + "\\expected_divik_results\\single\\pearson_no_filters\\divik-result.json";

			auto options = DivikOptions::ForLevels(2);
			options.UsingAmplitudeFiltration = false;
			options.UsingVarianceFiltration = false;

			TestForDataset(datasetFilename, resultPath, options, true);
		}

		[Test]
		void MediumDataSetEuclideanTest()
		{
			auto datasetFilename = _testDirectory + "\\hnc1_tumor.txt";
			auto resultPath = _testDirectory + "\\expected_divik_results\\hnc1_tumor\\euclidean\\divik-result.json";

			auto options = DivikOptions::ForLevels(1);
			options.Metric = Spectre::Algorithms::Parameterization::Metric::Euclidean;
			options.UsingAmplitudeFiltration = false;

			TestForDataset(datasetFilename, resultPath, options, false);
		}

		[Test]
		void MediumDataSetPearsonTest()
		{
			auto datasetFilename = _testDirectory + "\\hnc1_tumor.txt";
			auto resultPath = _testDirectory + "\\expected_divik_results\\hnc1_tumor\\pearson\\divik-result.json";

			auto options = DivikOptions::ForLevels(1);
			options.UsingAmplitudeFiltration = false;

			TestForDataset(datasetFilename, resultPath, options, false);
		}

		[Test]
		void SyntheticDataSetTest()
		{
			auto datasetFilename = _testDirectory + "\\synthetic_1.txt";
			auto resultPath = _testDirectory + "\\expected_divik_results\\synthetic\\1\\divik-result.json";

			auto options = DivikOptions::ForLevels(5);
			options.Metric = Spectre::Algorithms::Parameterization::Metric::Euclidean;
			options.UsingAmplitudeFiltration = false;
			options.UsingVarianceFiltration = false;

			TestForDataset(datasetFilename, resultPath, options, true);
		}		
};
}
