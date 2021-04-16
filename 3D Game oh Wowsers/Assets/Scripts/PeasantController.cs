using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //checks if the gameObject has been knocked over. Destroys the gameObject on delay if it has to prevent instant destory call
        Debug.Log("Current Angles: " + gameObject.transform.localEulerAngles.x + " " + gameObject.transform.localEulerAngles.y + " " + gameObject.transform.localEulerAngles.z);
        if (gameObject.transform.localEulerAngles.x < 300 && !(gameObject.transform.localEulerAngles.x < 60))
        {
            StartCoroutine(DestroyOnDelay());
        }
    }

    IEnumerator DestroyOnDelay()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
