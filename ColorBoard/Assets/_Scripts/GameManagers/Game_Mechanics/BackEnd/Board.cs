﻿using UnityEngine;
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

    public Block[][] _board;

    public Board(int BoardSize)
    {
        this.BoardSize = BoardSize;
        this._board = new Block[this.BoardSize][];

        for (int i = 0; i < BoardSize; ++i)
        {
            this._board[i] = new Block[this.BoardSize];
        }
    }

    public void InitializeBlocks()
    {
        // TODO: Get the amount of players and populate the board correctly based on that
        for(int i = 0; i < BoardSize; ++i)
        {
            for(int j = 0; j < BoardSize; ++j)
            {
                _board[i][j] = new Block(i, j, COLOR.WHITE);
            }
        }
    }

    public void ChangeColor(Block block, COLOR color)
    {
        block.setColor(color);
    }
}
