#include "DivikResultStruct.h"

DivikResultStruct::DivikResultStruct()
{
	qualityIndex = 0;
	amplitudeThreshold = 0;
	varianceThreshold = NAN;

	centroids.push_back(std::vector<double>());
	centroids.push_back(std::vector<double>());
	centroids.push_back(std::vector<double>());

	for (int i = 0; i < centroids.size(); i++)
		for (int j = i; j < i + 5; j++)
			centroids[i].push_back(j);

	for (int i = 0; i < 5; i++)
		partition.push_back(i);

	for (int i = 0; i < 5; i++)
		amplitudeFilter.push_back(true);

	for (int i = 0; i < 5; i++)
		varianceFilter.push_back(false);

	for (int i = 0; i < 5; i++)
		merged.push_back(i);

}
