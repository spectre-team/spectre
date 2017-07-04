#pragma once

#include <vector>

struct DivikResultStruct
{
	double qualityIndex;
	std::vector<std::vector<double>> centroids;
	std::vector<int> partition;

	double amplitudeThreshold;
	std::vector<bool> amplitudeFilter;

	double varianceThreshold;
	std::vector<bool> varianceFilter;

	std::vector<int> merged;

	std::vector<DivikResultStruct> subregions;
};