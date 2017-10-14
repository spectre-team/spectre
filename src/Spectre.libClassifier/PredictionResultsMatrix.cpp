/*
* PredictionResultsMatrix.cpp
* It contains knowledge about true positive, true negative etc. prediction of SVM
*
Copyright 2017 Grzegorz Mrukwa, Wojciech Wilgierz

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

#include "Spectre.libClassifier/PredictionResultsMatrix.h"

namespace Spectre::libClassifier {

PredictionResultsMatrix::PredictionResultsMatrix()
{
    true_positive = 0;
    true_negative = 0;
    false_positive = 0;
    false_negative = 0;
}


}
