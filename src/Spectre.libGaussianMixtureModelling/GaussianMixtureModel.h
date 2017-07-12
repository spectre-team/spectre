/*
 * GaussianMixtureModel.h
 * Provides implementation of Gaussian Mixture Model return type.
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
#include <vector>
#include <gsl.h>

namespace Spectre::libGaussianMixtureModelling
{
    /// <summary>
    /// Represents a single component of a Gaussian Mixture.
    /// </summary>
    struct GaussianComponent
    {
        /// <summary>
        /// Mean value of the component or peak's location.
        /// </summary>
        double mean;

        /// <summary>
        /// Standard deviation of the component or peak's width.
        /// </summary>
        double deviation;

        /// <summary>
        /// Weight of the component or peak's height.
        /// </summary>
        double weight;
    };

    /// <summary>
    /// Represents a composite of gaussian distribution components 
    /// that build up a Gaussian Mixture Model.
    /// </summary>
    struct GaussianMixtureModel
    {
        /// <summary>
        /// Constructor used for initialization of m/z, intesities and gaussian components
        /// collections.
        /// </summary>
        /// <param name="mzArray">M/z data shared by all spectra.</param>
        /// <param name="intensities">Mean intensities at each point.</param>
        /// <param name="numberOfComponents">Number of Gaussian Components to be set.</param>
        GaussianMixtureModel(const gsl::span<double>& mzArray,
                             const gsl::span<double>& intensities,
                             unsigned int numberOfComponents) :
            originalMzArray(mzArray.begin(), mzArray.end()), 
            originalMeanSpectrum(intensities.begin(), intensities.end()),
            components(numberOfComponents), 
            isMerged(), isNoiseReduced(), mzMergingThreshold() // will be used in future.
        {
        }

        /// <summary>
        /// Collection of Gaussian components.
        /// </summary>
        std::vector<GaussianComponent> components;

        /// <summary>
        /// Collection of initially supplied mz values.
        /// </summary>
        const std::vector<double> originalMzArray;

        /// <summary>
        /// Collection of average supplied spectra.
        /// </summary>
        const std::vector<double> originalMeanSpectrum;

        /// <summary>
        /// M/z threshold used in components merging.
        /// </summary>
        const double mzMergingThreshold;

        /// <summary>
        /// Value indicating whether this instance is merged.
        /// </summary>
        const bool isMerged;

        /// <summary>
        /// Gets a value indicating whether this instance is noise components reduced.
        /// </summary>
        const bool isNoiseReduced;
    };
}