using UnityEngine;

public class GameBlock : MonoBehaviour {

    public Block block;

    private MeshRenderer renderer;

    void Start()
    {
        this.renderer = GetComponent<MeshRenderer>();
        this.renderer.material.color = BoardManager.Instance.ColorTranslator[block.getColor()];
        this.gameObject.tag = "floor";
        
    }

    void OnCollisionEnter(Collision obj)
    {
        if (this.block.getColor() == Board.COLOR.WHITE)
        {
            block.setColor(obj.gameObject.GetComponent<PlayerSetup>().PlayerColor);
            this.renderer.material.color = BoardManager.Instance.ColorTranslator[block.getColor()];
            BoardManager.Instance.ColorCount++;
            if (BoardManager.Instance.ColorCount >= BoardManager.Instance.Goal)
            {
                int colorChange = (int)Random.Range(1f, 4f);
                obj.gameObject.GetComponent<PlayerSetup>().initialize(colorChange);
                BoardManager.Instance.LevelComplete();
            }
        }
    }
	
}
