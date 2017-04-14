#include "Stdafx.h"
#include "Segmentation.h"

using namespace Spectre::Algorithms::Results;
using namespace Spectre::Data::Datasets;
using namespace Spectre::Algorithms::Parameterization;

namespace Spectre::AlgorithmsCli::Methods
{
	DivikResult^ Segmentation::Divik(IDataset^ dataset, DivikOptions options)
	{
		//This is needed to not to make MCR go wild
		const int divikStructureLocation = 1;
		const int numberOfOutputArgs = 2;

		auto coordinates = dataset->GetRawSpacialCoordinates(true);
		auto varargin = options.ToVarargin();
		auto matlabDivikResult = _segmentation->divik(numberOfOutputArgs,
			dataset->GetRawIntensities(),
			coordinates,
			varargin);

		//matlabResult[0] is equal to the "partition" field in matlabResult[1],
		//that's why we only use matlabResult[1]
		//Besides it helps to create recursive single constructor for DivikResult
		auto result = gcnew DivikResult(matlabDivikResult[divikStructureLocation]);

		return result;
	}

	Segmentation::Segmentation()
	{
		this->_segmentation = gcnew MatlabAlgorithmsNative::Segmentation();
	}

	Segmentation::!Segmentation()
	{
		delete _segmentation;
		_segmentation = nullptr;
	}

	Segmentation::~Segmentation()
	{
		this->!Segmentation();
	}
}
