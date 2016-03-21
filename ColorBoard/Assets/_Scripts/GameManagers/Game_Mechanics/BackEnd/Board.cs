using UnityEngine;
using System.Collections;

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
            GenerateShape();
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
    
    void GenerateShape()
    {
        float chance;
        
        for(int i = 0; i < boardShape.Length; ++i)
        {
            chance = Random.value;
            for(int j = 0; j < boardShape[i].Length; ++j)
            {
                if(chance >= .5)
                {
                    boardShape[i][j] = 1;
                    BlockCount++;
                }
                else
                {
                    boardShape[i][j] = 0;
                }
            }
        }

        boardShape[0][0] = 1;

        ValidateBoard();
        
    }

    bool ValidBlock(int [][] shape, int[] pos)
    {
        int touchCount = 0;
        
        if(pos[0] < shape.Length - 1)
        {
            if(shape[pos[0]+1][pos[1]] == 1)
            {
                ++touchCount;
            }
        }
        if(pos[1] < shape[pos[0]].Length - 1)
        {
            if(shape[pos[0]][pos[1]+1] == 1)
            {
                ++touchCount;
            }
        }

        if(touchCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void ValidateBoard()
    {
        float extraChance = Random.value;
        for (int i = 0; i < boardShape.Length; ++i)
        {
            for (int j = 0; j < boardShape[i].Length; ++j)
            {
                if (boardShape[i][j] == 1)
                {
                    if (!ValidBlock(boardShape, new int[] { i, j }))
                    {
                        if (j < boardShape[i].Length - 1)
                        {
                            if (boardShape[i][j + 1] != 1)
                            {
                                boardShape[i][j + 1] = 1;
                                BlockCount++;
                                break;
                            }
                        }
                        if (i < boardShape.Length - 1)
                        {
                            if (boardShape[i + 1][j] != 1)
                            {
                                boardShape[i + 1][j] = 1;
                                BlockCount++;
                                break;
                            }
                        }
                        
                        
                    }
                }
                else
                {
                    if(extraChance < .2f)
                    {
                        boardShape[i][j] = 1;
                    }
                }
            }
        }

        /*if (!isValid)
        {
            ValidateBoard();
        }*/
    }
}
