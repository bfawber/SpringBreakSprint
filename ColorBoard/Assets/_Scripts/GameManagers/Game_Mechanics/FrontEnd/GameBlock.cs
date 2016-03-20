using UnityEngine;
using System.Collections;

public class GameBlock : MonoBehaviour {

    public Block block;

    private MeshRenderer renderer;

    void Start()
    {
        this.renderer = GetComponent<MeshRenderer>();
        this.renderer.material.color = BoardManager.Instance.ColorTranslator[block.getColor()];
        
    }

    void OnCollisionEnter(Collision obj)
    {
        block.setColor(obj.gameObject.GetComponent<PlayerSetup>().PlayerColor);
        this.renderer.material.color = BoardManager.Instance.ColorTranslator[block.getColor()];
    }
	
}
