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
        int startIndex;
        int endIndex;
        
        for(int i = 0; i < boardShape.Length; ++i)
        {
            startIndex = Random.Range(0, boardShape[i].Length - 1);
            endIndex = Random.Range(startIndex, boardShape[i].Length - 1);
            for(int j = 0; j < boardShape[i].Length; ++j)
            {
                
                if(j < startIndex || j > endIndex)
                {
                    boardShape[i][j] = 0;
                }
                else
                {
                    boardShape[i][j] = 1;
                    BlockCount++;
                }
            }
        }

        boardShape[0][0] = 1;
        BlockCount++;       
    }
}
