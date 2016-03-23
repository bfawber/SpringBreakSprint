using UnityEngine;
using System.Collections;
using System;
using System.Threading;

public class Board {

    public enum COLOR
    {
        RED,
        BLUE,
        GREEN,
        YELLOW,
        WHITE
    };

    public int BoardSize;
    public int BlockCount = 0;

    public Block[][] board;
    public int[][] boardShape;
    public delegate int[][] Generator(int[][] shape);

    public Board(int BoardSize, int Level)
    {
        this.BoardSize = BoardSize;
        this.board = new Block[this.BoardSize][];
        this.BlockCount = 0;
        
        if(Level > 1)
        {
            for (int i = 0; i < BoardSize; ++i)
            {
                this.board[i] = new Block[this.BoardSize];
            }
            this.boardShape = new int[BoardSize][];
            for (int i = 0; i < BoardSize; ++i)
            {
                this.boardShape[i] = new int[this.BoardSize];
            }
            GenerateShape(GeneticGenerator, boardShape);
        }
        else
        {
            for (int i = 0; i < BoardSize; ++i)
            {
                this.board[i] = new Block[this.BoardSize];
                this.BlockCount += BoardSize;
            }
    }

    }

    int[][] RandomLinesGenerator(int[][] shape)
    {
        int startIndex;
        int endIndex;

        for (int i = 0; i < shape.Length; ++i)
        {
            startIndex = UnityEngine.Random.Range(0, shape[i].Length - 1);
            endIndex = UnityEngine.Random.Range(startIndex, shape[i].Length - 1);
            for (int j = 0; j < shape[i].Length; ++j)
            {

                if (j < startIndex || j > endIndex)
                {
                    shape[i][j] = 0;
                }
                else
                {
                    shape[i][j] = 1;
                }
            }
        }

        shape[0][0] = 1;

        return shape;
    }

    void GenerateShape(Generator gen, int[][] shape)
    {
        boardShape = gen(shape);
        BlockCount = CountBlocks(boardShape);
    }

    int CountBlocks(int[][] shape)
    {
        int blockCount = 0;

        for(int i = 0; i < shape.Length; ++i)
        {
            for(int j = 0; j < shape[i].Length; ++j)
            {
                if(shape[i][j] == 1)
                {
                    ++blockCount;
                }
            }
        }

        return blockCount;
    }

    float ScoreLevel(int[][] shape)
    {
        float score = 0f;

        for(int i = 0; i < shape.Length; ++i)
        {
            for(int j = 0; j < shape[i].Length; ++j)
            {
                if(shape[i][j] == 0)
                {
                    score += 1f;
                }
                if (IsAlone(shape, new int[] { i, j }))
                {
                    score -= 5f;
                }
            }
        }

        if(score < 0)
        {
            score = 0f;
        }

        return score;
    }

    bool IsAlone(int[][] shape, int[] pos)
    {
        // Check Up
        if(pos[0] > 0)
        {
            if(shape[pos[0]-1][pos[1]] == 1)
            {
                return false;
            }
        }

        // Check Down
        if(pos[0] < shape.Length - 2)
        {
            if (shape[pos[0] + 1][pos[1]] == 1)
            {
                return false;
            }
        }

        // Check Left
        if(pos[1] > 0)
        {
            if (shape[pos[0]][pos[1] - 1] == 1)
            {
                return false;
            }
        }

        // Check Right
        if(pos[1] < shape[pos[0]].Length - 2)
        {
            if (shape[pos[0]][pos[1] + 1] == 1)
            {
                return false;
            }
        }

        return true;
    }

    int[][] GeneticGenerator(int[][] shape)
    {
        int populationSize = 20;
        float desiredScore = .05f;
        float[] scores = new float[populationSize];
        int[][][] population = new int[populationSize][][];

        // Create Population
        for(int i = 0; i < populationSize; ++i)
        {
            population[i] = RandomLinesGenerator(shape);
        }

        // - - - - - - - - - - Start Genetic Algorithm - - - - - - - - - - - -
        
        // Score
        for (int i = 0; i < populationSize; ++i)
        {
            scores[i] = ScoreLevel(population[i]);
        }

        
        
        SortArraysByScore(population, scores);
        float[] accumulatedScores = AccumulateScores(scores);

        for(int j = 0; j < 1000; ++j)
        {
            //Loop this until all children are made:
            Debug.Log("Accumulated Value = " + accumulatedScores[0]);
            for (int i = population.Length / 2; i < population.Length; ++i)
            {
                // Select
                int[][][] parents = SelectParents(population, accumulatedScores);

                // Crossover
                int[][] child = Crossover(parents[0], parents[1]);

                float mutationChance = UnityEngine.Random.value;

                // Mutate
                if (mutationChance < .15)
                {
                    child = Mutate(child);
                }
                // Add children to population
                population[i] = child;
            }

            // Score
            for (int i = 0; i < populationSize; ++i)
            {
                scores[i] = ScoreLevel(population[i]);
            }


            SortArraysByScore(population, scores);
            accumulatedScores = AccumulateScores(scores);
        }
        // End Loop

        //- - - - - - - - - - - - End Genetic Algorithm - - - - - - - - - - - -

        population[0][0][0] = 1;
        return population[0];
    }

    float SumArray(float[] array)
    {
        float sum = 0f;

        for(int i = 0; i < array.Length; ++i)
        {
            sum += array[i];
        }

        return sum;
    }

    float[] AccumulateScores(float[] scores)
    {
        float[] accumulateScores = new float[scores.Length];

        float totalScore = SumArray(scores);
        float score = 0f;

        for(int i = 0; i < accumulateScores.Length; ++i)
        {
            score += scores[i] / totalScore;
            accumulateScores[i] = score;
        }

        return accumulateScores;
    }

    void SortArraysByScore(int [][][] population, float[] scores)
    {
        int[][][] sortedPopulation = new int[population.Length][][];
        float[] sortedScores = new float[scores.Length];
        int[][][] c = null;
        float max = -1;
        int index = 0;

        for(int i = 0; i < scores.Length; ++i)
        {
            max = -1;
            index = 0;
            for(int j = 0; j < scores.Length; ++j)
            {
                if(scores[j] != -1)
                {
                    if(max < scores[j])
                    {
                        max = scores[j];
                        index = j;
                    }
                }
            }

            sortedPopulation[i] = population[index];
            sortedScores[i] = scores[index];
            scores[index] = -1;
        }
        population = sortedPopulation;
        scores = sortedScores;
    }

    int[][][] SelectParents(int[][][] population, float[] accumulatedScores)
    {
        int[][][] parents = new int[2][][];
        int parent1Index = -1;
        

        for(int i = 0; i < 2; ++i)
        {
            float chance = UnityEngine.Random.value;
            for(int j = 0; j < population.Length; ++j)
            {
                float score = accumulatedScores[j];
                if(parent1Index != -1)
                {
                    if(j != parent1Index)
                    {
                        if(score > chance)
                        {
                            parents[i] = population[j];
                            break;
                        }
                    }
                }
                else if(score > chance)
                {
                    parents[i] = population[j];
                    parent1Index = j;
                }
                if(j == population.Length - 1)
                {
                    parents[i] = population[j];
                }
            }
        }

        return parents;
    }

    int[][] Crossover(int [][] parent1, int[][] parent2)
    {
        int[][] child = parent1;
        float chance;
        for(int i = 0; i < parent1.Length; ++i)
        {
            for(int j = 0; j < parent1[i].Length; ++j)
            {
                chance = UnityEngine.Random.value;
                if(chance < .5)
                {
                    child[i][j] = parent2[i][j];
                }
            }
        }

        return child;
    }

    int[][] Mutate(int[][] shape)
    {
        float chance = UnityEngine.Random.value;
        int[][] child = shape;
        int posX = UnityEngine.Random.Range(0, shape.Length - 1);
        int posY = UnityEngine.Random.Range(0, shape[posX].Length - 1);
        for(int i = 0; i < UnityEngine.Random.Range(10, shape.Length - 1); ++i)
        {
            child[posX][posY] = 1 - child[posX][posY];
            posX = UnityEngine.Random.Range(0, shape.Length - 1);
            posY = UnityEngine.Random.Range(0, shape[posX].Length - 1);
            chance = UnityEngine.Random.value;
        }

        return child;
    }

    IEnumerable geneticRunner(int[][] shape)
    {
        yield return GeneticGenerator(shape);
    }
}
