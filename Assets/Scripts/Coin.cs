using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    private LevelManager thelevelManager;

    public int coinValue;

    // Use this for initialization
	void Start () {
        thelevelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            thelevelManager.AddCoins(coinValue);

            gameObject.SetActive(false);
        }
    }
}
