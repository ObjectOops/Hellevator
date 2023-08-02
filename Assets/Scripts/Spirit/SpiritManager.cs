using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritManager : MonoBehaviour
{
    public static SpiritManager instance;

    //public GameObject objectSpirit;
    //public GameObject objectBossSpirit;

    public GameObject[] objectSpirit;
    [HideInInspector]
    public bool[] spiritChosen;
    public GameObject[] objectBossSpirit;

    public Transform pos1, pos2;

    private GameManager gameManager;

    public int temp;
    private void Start()
    {
        instance = this;
        spiritChosen = new bool[objectSpirit.Length];
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
        if(gameManager.spiritNum == 4)
        {
            gameManager.spiritNum = 5;
            //GameObject currentSpirit = Instantiate(objectBossSpirit);
            GameObject currentSpirit = Instantiate(objectBossSpirit[gameManager.day - 1]);
            currentSpirit.transform.position = pos1.position;
            currentSpirit.SetActive(true);
            return true;
        }
        else
        {
            gameManager.spiritNum++;
            //GameObject currentSpirit = Instantiate(objectSpirit);
            while(true)
            {
                int chosen = Random.Range(0, objectSpirit.Length);
                if (spiritChosen[chosen] == false)
                {
                    GameObject currentSpirit = Instantiate(objectSpirit[chosen]);
                    currentSpirit.transform.position = pos1.position;
                    spiritChosen[chosen] = true;
                    currentSpirit.SetActive(true);
                    break;
                }
            }
            return false;
        }
    }

}
