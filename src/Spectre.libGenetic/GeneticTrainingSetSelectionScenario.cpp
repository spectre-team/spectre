/*
* GeneticTrainingSetSelectionScenario.cpp
* Executes whole genetic algorithm and returns results
*
Copyright 2017 Spectre Team

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
                                                                             unsigned int generationAmount,
                                                                             const std::vector<unsigned int>& generationSize,
                                                                             const std::vector<unsigned int>& trueAmount,
                                                                             const std::string& filename,
                                                                             unsigned int runAmount,
                                                                             Seed seed) :
        m_GenerationSizes(generationSize.begin(), generationSize.end()),
        m_InitialIndividualFillups(trueAmount.begin(), trueAmount.end()),
        m_TrainingDatasetSizeRate(trainingRate),
        m_Filename(filename),
        m_RestartsNumber(runAmount),
        m_Seed(seed),
        m_GaFactory(mutationRate, bitSwapRate, preservationRate, generationAmount)
    {}

    void GeneticTrainingSetSelectionScenario::execute(libClassifier::OpenCvDataset data) const
    {
        for (auto runNumber = 0u; runNumber < m_RestartsNumber; runNumber++)
        {
            for (auto popSize : m_GenerationSizes)
            {
                for (auto trueAmount : m_InitialIndividualFillups)
                {
                    auto begin = clock();
                    RaportGenerator raportGenerator(m_Filename + "_" + std::to_string(popSize) + "_" + std::to_string(trueAmount));
                    raportGenerator.write("Genetic algorithm " + std::to_string(runNumber) + " run with " + std::to_string(popSize) + " population size and " + std::to_string(trueAmount) + "for true amount in individuals in initial generation.");
                    //raportGenerator.write("Data:");
                    //raportGenerator.write(&data);

                    libClassifier::RandomSplitter splitter(m_TrainingDatasetSizeRate, m_Seed + runNumber);
                    auto splittedDataset = splitter.split(data);
                    auto fitnessFunction = std::make_unique<libClassifier::SVMFitnessFunction>(std::move(splittedDataset), raportGenerator);
                    auto algorithm = m_GaFactory.BuildDefault(std::move(fitnessFunction), m_Seed + runNumber);

                    Generation initialGeneration(popSize, splittedDataset.trainingSet.size(), trueAmount);
                    auto finalGeneration = algorithm->evolve(std::move(initialGeneration));

                    auto end = clock();
                    auto elapsed_secs = static_cast<double>(end - begin) / CLOCKS_PER_SEC;
                    raportGenerator.write("Time needed for Genetic algorithm - " + std::to_string(runNumber) + " number of execution for parameters " + std::to_string(popSize) + " population size and " +
                        std::to_string(trueAmount) + "for true amount in individuals in initial generation is: " + std::to_string(elapsed_secs) + " seconds.");
                    raportGenerator.close();
                }
            }
        }
    }

}