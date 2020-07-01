using UnityEngine;
using System.Collections;

public class DestroyOvertime : MonoBehaviour {


    public float lifeTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        lifeTime = lifeTime - Time.deltaTime;

        if(lifeTime <= 0f)
        {
            Destroy(gameObject);
        }

        //Debug.Log(Time.deltaTime);
	}
}
