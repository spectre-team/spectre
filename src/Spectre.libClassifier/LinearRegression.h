/*
* LinearRegression.h
* Functions to execute linear regression on dataset
*
Copyright 2017 Wojciech Wilgierz

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
namespace
{
	struct problem
	{
		int l, n;
		int *y;
		struct feature_node **x;
		double bias;
	};

	struct parameter
	{
		int solver_type;

		/* these are for training only */
		double eps;             /* stopping criteria */
		double C;
		int nr_weight;
		int *weight_label;
		double* weight;
		double p;
	};

	struct model
	{
		struct parameter param;
		int nr_class;           /* number of classes */
		int nr_feature;
		double *w;
		int *label;             /* label of each class */
		double bias;
	};

	class LinearRegression
	{
	public:

	private:
	};
}
