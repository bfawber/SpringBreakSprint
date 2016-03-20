using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

    public static PlayerSpawner Instance = null;
    public GameObject Player;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
	
    void Start()
    {
        GameObject player = Instantiate(Player, new Vector3(0f, 2f, 0f), Quaternion.identity) as GameObject;
        player.GetComponent<PlayerSetup>().initialize(1);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
