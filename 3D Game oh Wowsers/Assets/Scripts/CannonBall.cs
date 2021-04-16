using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyOnDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyOnDelay()
{
    yield return new WaitForSeconds(5);
    Destroy(gameObject);
}
}
