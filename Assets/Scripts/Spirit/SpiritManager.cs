using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritManager : MonoBehaviour
{
    public static SpiritManager instance;

    public GameObject objectSpirit;
    public GameObject objectBossSpirit;

    public Transform pos1, pos2;

    private GameManager gameManager;
    private void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager == null)
        {
            gameManager = GameManager.instance;
        }
    }
    public bool GenerateSpirit()
    {
        if(gameManager == null)
        {
            Debug.Log("Damn");
        }
        if(gameManager.spiritNum == 4)
        {
            gameManager.spiritNum = 5;
            GameObject currentSpirit = Instantiate(objectBossSpirit);
            return true;
        }
        else
        {
            gameManager.spiritNum++;
            GameObject currentSpirit = Instantiate(objectSpirit);
            return false;
        }
    }
}
