using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {
    public static BoardManager Instance = null;

    public int BoardSize = 6;
    public int ColorCount = 0;
    public int Level = 1;
    public int Goal = 0;
    public GameObject Gblock;
    public Dictionary<Board.COLOR, Color> ColorTranslator;

    public float IncreaseBoardSizeChance = 0f;
    public float RiseChance = 0f;

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
            CreateBoard(this.Level);
            BoardManager.Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
	
	}

    public void CreateBoard(int lvl)
    {
        this.board = new Board(this.BoardSize, lvl);
        Vector3 spawnPoint = Vector3.zero;
        GameObject Parent = new GameObject();
        Parent.name = "Blocks Parent";
        for (int i = 0; i < this.board.board.Length; ++i)
        {
            for (int j = 0; j < this.board.board[i].Length; ++j)
            {
                spawnPoint = new Vector3((float)j, 0f, (float)i);
                if (board.boardShape != null)
                {
                    if (board.boardShape[i][j] == 1)
                    {
                        GameObject block = Instantiate(Gblock, spawnPoint, Quaternion.identity) as GameObject;
                        block.GetComponent<GameBlock>().block = new Block(i, j, Board.COLOR.WHITE);
                        board.board[i][j] = block.GetComponent<GameBlock>().block;
                        block.transform.parent = Parent.transform;
                    }
                }
                else
                {
                    GameObject block = Instantiate(Gblock, spawnPoint, Quaternion.identity) as GameObject;
                    block.GetComponent<GameBlock>().block = new Block(i, j, Board.COLOR.WHITE);
                    board.board[i][j] = block.GetComponent<GameBlock>().block;
                    block.transform.parent = Parent.transform;
                }
            }
        }

        this.Goal = board.BlockCount;
    }

    public void LevelComplete()
    {
        Time.timeScale = 0f;
        this.Level++;
        Debug.Log("Color Count = " + ColorCount + " \n BoardSize^2 = " + BoardSize * BoardSize);
        this.ColorCount = 0;
        float chance = Random.value;
       
        if(IncreaseBoardSizeChance > chance)
        {
            this.BoardSize += 2;
        }
        else
        {
            this.IncreaseBoardSizeChance += .1f;
        }



        Destroy(GameObject.Find("Blocks Parent"));


        this.Goal = 0;
        CreateBoard(this.Level);
        Time.timeScale = 1f;
        
        
    }
}
