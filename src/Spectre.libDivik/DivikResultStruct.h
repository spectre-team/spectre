#pragma once

#include <vector>

typedef std::vector<double> Centroid;

struct DivikResultStruct
{
	DivikResultStruct();

	double qualityIndex;
	std::vector<Centroid> centroids;
	std::vector<int> partition;

	double amplitudeThreshold;
	std::vector<bool> amplitudeFilter;

	double varianceThreshold;
	std::vector<bool> varianceFilter;

	std::vector<int> merged;

	std::vector<DivikResultStruct> subregions;
};