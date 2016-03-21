using UnityEngine;
using System.Collections;

public class PlayerSetup : MonoBehaviour {
    
    public Board.COLOR PlayerColor;
    
    public void initialize(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                PlayerColor = Board.COLOR.RED;
                break;
            case 2:
                PlayerColor = Board.COLOR.BLUE;
                break;
            case 3:
                PlayerColor = Board.COLOR.GREEN;
                break;
            case 4:
                PlayerColor = Board.COLOR.YELLOW;
                break;
            default:
                PlayerColor = Board.COLOR.WHITE;
                break;
        }

        this.gameObject.GetComponent<MeshRenderer>().material.color =
            BoardManager.Instance.ColorTranslator[this.PlayerColor];
    }
	
}
