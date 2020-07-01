using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject target;
    public float followAhead;

    private Vector3 targetposition;

    public float smoothing;

    public bool followTarget;
	// Use this for initialization
	void Start () {
        followTarget = true;
	}

    // Update is called once per frame
    void Update()
    {

        if (followTarget)
        {
            //transform.position = new Vector3(target.transform.position.x, target.transform.position.y+2, transform.position.z);
            targetposition =
                new Vector3(target.transform.position.x, target.transform.position.y + 2, transform.position.z);

            //this move the target ahead of target
            if (target.transform.localScale.x > 0f)
            {
                targetposition =
                new Vector3(target.transform.position.x + followAhead, target.transform.position.y + 2, transform.position.z);
            }
            else
            {
                targetposition =
                new Vector3(target.transform.position.x - followAhead, target.transform.position.y + 2, transform.position.z);
            }

            //transform.position = targetposition;

            transform.position = Vector3.Lerp(transform.position, targetposition, smoothing * Time.deltaTime);

        }
    }
}
