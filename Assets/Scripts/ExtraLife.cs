using UnityEngine;
using System.Collections;

public class ExtraLife : MonoBehaviour {

    public int livesToGive;

    private LevelManager theLevelmanager;

	// Use this for initialization
	void Start () {
        theLevelmanager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            theLevelmanager.AddLives(livesToGive);
            gameObject.SetActive(false);
        }
    }
}
