using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {
    public static BoardManager Instance = null;

    public int BoardSize = 6;
    public GameObject Gblock;
    public Dictionary<Board.COLOR, Color> ColorTranslator;

    private Board board;

	// Use this for initialization
	void Awake () {

        if (BoardManager.Instance == null)
        {
            ColorTranslator = new Dictionary<Board.COLOR, Color>()
            {
                {Board.COLOR.WHITE, Color.white },
                {Board.COLOR.RED, Color.red },
                {Board.COLOR.BLUE, Color.blue },
                {Board.COLOR.GREEN, Color.green },
                {Board.COLOR.YELLOW, Color.yellow }
            };

            this.board = new Board(this.BoardSize);
            Vector3 spawnPoint = Vector3.zero;
            for (int i = 0; i < BoardSize; ++i)
            {
                for (int j = 0; j < BoardSize; ++j)
                {
                    spawnPoint = new Vector3((float)j, 0f, (float)i);
                    GameObject block = Instantiate(Gblock, spawnPoint, Quaternion.identity) as GameObject;
                    block.GetComponent<GameBlock>().block = new Block(i, j, Board.COLOR.WHITE);
                    board.board[i][j] = block.GetComponent<GameBlock>().block;
                }
            }

            BoardManager.Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
	
	}
}
