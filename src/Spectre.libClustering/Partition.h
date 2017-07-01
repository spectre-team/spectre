/*
 * Partition.h
 * Provides class prototype that simplifies partition vectors.
 *
   Copyright 2017 Dariusz Kuchta

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

namespace Spectre::libClustering
{
	/// <summary>
	/// Class storing simplified representation of partition vectors.
	/// </summary>
	class Partition
	{
	public:

		/// <summary>
		/// Default constructor. Deleted so no dataless instance can be created.
		/// </summary>
		Partition() = delete;

		/// <summary>
		/// Constructor taking partition data and computing its simplified version.
		/// </summary>
		/// <param name="partition">Input partition data.</param>
		explicit Partition(gsl::span<unsigned int> partition);

		/// <summary>
		/// Getter for simplified partition data.
		/// </summary>
		/// <returns>Stored simplified partition.</returns>
		const std::vector<unsigned int>& Get() const;

		/// <summary>
		/// Method for comparing specified partitions.
		/// </summary>
		/// <param name="lhs">The first partition.</param>
		/// <param name="rhs">The second partition.</param>
		/// <param name="tolerance">The tolerance rate of mismatch.</param>
		static bool Compare(const Partition &lhs, const Partition &rhs, double tolerance = 0);
	
		/// <summary>
		/// Equality operator. Compares with tolerance = 0.
		/// </summary>
		/// <param name="other">The second partition.</param>
		bool operator==(const Partition& other) const;

		/// <summary>
		/// Inequality operator. Compares with tolerance = 0.
		/// </summary>
		/// <param name="other">The second partition.</param>
		bool operator!=(const Partition& other) const;

	private:
		std::vector<unsigned int> m_Partition;
	};
}
