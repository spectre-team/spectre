/*
* GeneticAlgorithmExecutor.cpp
* Executes whole genetic algorithm and returns results
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

#include "GeneticTrainingSetSelectionScenario.h"
#include "Spectre.libClassifier/SplittedOpevCvDataset.h"
#include "Spectre.libClassifier/RandomSplitter.h"
#include "Spectre.libClassifier/SVMFitnessFunction.h"
#include "Spectre.libGenetic/GeneticAlgorithm.h"
#include "Spectre.libGenetic/RaportGenerator.h"
#include <ctime>
#include "Spectre.libGenetic/GeneticAlgorithmFactory.h"

namespace Spectre::libGenetic
{

    GeneticTrainingSetSelectionScenario::GeneticTrainingSetSelectionScenario(double trainingRate,
                                                                             double mutationRate,
                                                                             double bitSwapRate,
                                                                             double preservationRate,
                                                                             int generationAmount,
                                                                             const std::vector<int>& generationSize,
                                                                             const std::vector<int>& trueAmount,
                                                                             const std::string& filename,
                                                                             int runAmount,
                                                                             Seed seed)
        : m_GenerationAmount(generationAmount),
        m_GenerationSize(generationSize.begin(), generationSize.end()),
        m_InitialIndividualFillup(trueAmount.begin(), trueAmount.end()),
        m_TrainingDatasetSizeRate(trainingRate),
        m_Filename(filename),
        m_RestartsNumber(runAmount),
        m_Seed(seed),
        m_GaFactory(seed, mutationRate, bitSwapRate, preservationRate, generationAmount)
    {}

    void GeneticTrainingSetSelectionScenario::execute(libClassifier::OpenCvDataset data)
    {
        for (auto i = 0; i < m_GenerationAmount; i++) {
            for (auto popSize : m_GenerationSize) {
                for (auto trueAmount : m_InitialIndividualFillup) {
                    auto begin = clock();
                    RaportGenerator raportGenerator(m_Filename + "_" + std::to_string(popSize) + "_" + std::to_string(trueAmount));
                    //raportGenerator.write("Data:");
                    //raportGenerator.write(&data);

                    libClassifier::RandomSplitter splitter(m_TrainingDatasetSizeRate, m_Seed);
                    auto splittedDataset = splitter.split(data);
                    auto fitnessFunction = std::make_unique<libClassifier::SVMFitnessFunction>(std::move(splittedDataset), raportGenerator);
                    auto algorithm = m_GaFactory.BuildDefault(std::move(fitnessFunction));

                    Generation initialGeneration(popSize, int(data.size()), trueAmount);
                    auto finalGeneration = algorithm->evolve(std::move(initialGeneration));

                    auto end = clock();
                    auto elapsed_secs = static_cast<double>(end - begin) / CLOCKS_PER_SEC;
                    raportGenerator.write("Time needed for Genetic algorithm: " + std::to_string(elapsed_secs) + " seconds.");
                    raportGenerator.close();
                }
            }
        }
    }

}
