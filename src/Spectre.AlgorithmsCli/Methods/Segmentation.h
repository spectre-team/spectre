#pragma once

namespace Spectre::AlgorithmsCli::Methods
{
	public ref class Segmentation
	{
	private:
		/// <summary>
		/// MATLAB services provider.
		/// </summary>
		MatlabAlgorithmsNative::Segmentation^ _segmentation;
	public:
		/// <summary>
		/// Initializes a new instance of the <see cref="Segmentation"/> class.
		/// </summary>
		Segmentation();
		/// <summary>
		/// Finalizes an instance of the <see cref="Segmentation"/> class.
		/// </summary>
		~Segmentation();
		/// <summary>
		/// Disposes an instance of the <see cref="Segmentation"/> class.
		/// </summary>
		!Segmentation();

		/// <summary>
		/// Performs DiviK clustering on the specified data.
		/// </summary>
		/// <param name="dataset">Input dataset.</param>
		/// <param name="options">Configuration.</param>
		/// <returns>Segmentation result.</returns>
		Spectre::Algorithms::Results::DivikResult^ Divik(
			Spectre::Data::Datasets::IDataset^ dataset, // class has to be passed with ^ (reference)
			Spectre::Algorithms::Parameterization::DivikOptions options); // value type (struct) - without ^, passed by value
	};
}
