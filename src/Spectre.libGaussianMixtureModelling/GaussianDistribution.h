/*
* GaussianDistribution.h
* Provides implementation of Gaussian Distribution function (bell curve).
*
Copyright 2017 Michal Gallus

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

#pragma once
#define _USE_MATH_DEFINES // used for M_PI
#include <math.h>

namespace Spectre::libGaussianMixtureModelling
{
	/// <summary>
	/// Computes value of Guassian Function, also known as Normal distribution
	/// based on given mean, standard deviation for a single observation.
	/// </summary>
	inline double Gaussian(double x, double mean, double std)
	{
		return (1.0 / sqrt(2.0 * M_PI * std * std)) * exp(-pow(x - mean, 2) / (2.0 * std * std));
	}
}