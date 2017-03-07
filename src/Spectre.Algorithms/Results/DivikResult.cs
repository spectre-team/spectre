/*
 * DivikResult.cs
 * Contains structure organizing all the output from DiviK algorithm.
 * 
   Copyright 2017 Wilgierz Wojciech, Grzegorz Mrukwa

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


namespace Spectre.Algorithms.Results
{
	/// <summary>
	/// Wraps DiviK algorithm results.
	/// </summary>
	public class DivikResult
    {
	    private object _matlabResultStruct;

		/// <summary>
		/// Initializes a new instance of the <see cref="DivikResult"/> class.
		/// </summary>
		/// <param name="matlabResult">The results coming from MCR.</param>
		internal DivikResult(object[] matlabResult)
        {
	        _matlabResultStruct = matlabResult[1];
        }
    }
}
