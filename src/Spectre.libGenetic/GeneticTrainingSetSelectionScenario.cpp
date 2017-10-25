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

namespace Spectre::libGenetic {

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
        : mGenerationAmount(generationAmount),
        mGenerationSize(generationSize.begin(), generationSize.end()),
        mTrueAmount(trueAmount.begin(), trueAmount.end()),
        mTrainingRate(trainingRate),
        mMutationRate(mutationRate),
        mBitSwapRate(bitSwapRate),
        mPreservationRate(preservationRate),
        mFilename(filename),
        mRunAmount(runAmount),
        mSeed(seed)
    {}

    void GeneticTrainingSetSelectionScenario::execute(libClassifier::OpenCvDataset data)
    {
        for (auto i = 0; i < mGenerationAmount; i++) {
            for (int popSize : mGenerationSize) {
                for (int trueAmount : mTrueAmount) {
                    clock_t begin = clock();
                    RaportGenerator raportGenerator(mFilename + "_" + std::to_string(popSize) + "_" + std::to_string(trueAmount));
                    //raportGenerator.write("Data:");
                    //raportGenerator.write(&data);

                    libClassifier::RandomSplitter splitter(mTrainingRate, mSeed);
                    libClassifier::SplittedOpenCvDataset splittedDataset = splitter.split(data);
                    auto fitnessFunction = std::make_unique<libClassifier::SVMFitnessFunction>(std::move(splittedDataset), raportGenerator);
                    std::unique_ptr<CrossoverOperator> crossoverOperator = std::make_unique<CrossoverOperator>(mSeed);
                    std::unique_ptr<MutationOperator> mutationOperator = std::make_unique<MutationOperator>(mMutationRate, mBitSwapRate, mSeed);
                    std::unique_ptr<ParentSelectionStrategy> parentSelectionStrategy = std::make_unique<ParentSelectionStrategy>(mSeed);
                    std::unique_ptr<IndividualsBuilderStrategy> individualsBuilderStrategy = std::make_unique<IndividualsBuilderStrategy>(std::move(crossoverOperator), std::move(mutationOperator), std::move(parentSelectionStrategy));

                    std::unique_ptr<PreservationStrategy> preservationStrategy = std::make_unique<PreservationStrategy>(std::move(mPreservationRate));

                    std::unique_ptr<OffspringGenerator> offspringGenerator = std::make_unique<OffspringGenerator>(std::move(individualsBuilderStrategy), std::move(preservationStrategy));

                    std::unique_ptr<Scorer> scorer = std::make_unique<Scorer>(std::move(fitnessFunction));
                    std::unique_ptr<StopCondition> stopCondition = std::make_unique<StopCondition>(std::move(mGenerationAmount));

                    std::unique_ptr<GeneticAlgorithm> algorithm = std::make_unique<GeneticAlgorithm>(std::move(offspringGenerator), std::move(scorer), std::move(stopCondition));

                    Generation initialGeneration(popSize, int(data.size()), trueAmount);
                    Generation finalGeneration = algorithm->evolve(std::move(initialGeneration));

                    clock_t end = clock();
                    double elapsed_secs = double(end - begin) / CLOCKS_PER_SEC;
                    raportGenerator.write("Time needed for Genetic algorithm: " + std::to_string(elapsed_secs) + " seconds.");
                    raportGenerator.close();
                }
            }
        }
    }

}
