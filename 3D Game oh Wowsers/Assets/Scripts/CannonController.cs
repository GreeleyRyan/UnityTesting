using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CannonController : MonoBehaviour
{
    //UI properties
    public TextMeshProUGUI currentPowerText;
    public TextMeshProUGUI cannonReadyText;
    public TextMeshProUGUI currentAngleText;
    private Camera mainCamera;

    //cannon properties
    public int speed;
    public float friction;
    public float lerpSpeed;
    private float xDegrees;
    private float yDegrees;
    private Quaternion fromRotation;
    private Quaternion toRotation;
    private bool isCannonReady;

    //canonball properties
    //public ParticleSystem explosion;
    public GameObject cannonBall;
    public Transform frontOfBarrel;
    public float firePower = 50.0f;
    private Rigidbody cannonBallRB;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        isCannonReady = true;
        currentPowerText.text = "Current Power" + 50;
        currentAngleText.text = "Current Angle: " + 0;
        cannonReadyText.text = "Cannon is Ready!";

    }

    // Update is called once per frame
    void Update()
    {
        UpdateAngleText();
        CalculatePower();
        UpdatePowerText();
        
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag == "Cannon")
            {
                if (Input.GetMouseButton(0))
                {
                    CalculateAngle();
                }
            }
        }
        if(Input.GetKey(KeyCode.Space) && isCannonReady)
        {
            FireCannon();
            StartCoroutine(CannonCooldown());
        }

    }
    void FireCannon()
    {
        GameObject cannonBallCopy = Instantiate(cannonBall, frontOfBarrel.position, transform.rotation) as GameObject;
        cannonBallRB = cannonBallCopy.GetComponent<Rigidbody>();
        cannonBallRB.AddForce(transform.forward * firePower, ForceMode.Impulse);
        //Instantiate(explosion, frontOfBarrel.position, frontOfBarrel.rotation);
        isCannonReady = false;
        cannonReadyText.text = "Hang on!";
    }

    IEnumerator CannonCooldown()
    {
        yield return new WaitForSeconds(2);
        cannonReadyText.text = "Ready in: 3";
        yield return new WaitForSeconds(1);
        cannonReadyText.text = "Ready in: 2";
        yield return new WaitForSeconds(1);
        cannonReadyText.text = "Ready in: 1";
        yield return new WaitForSeconds(1);
        isCannonReady = true;
        cannonReadyText.text = "Cannon is Ready!";
    }

    void CalculateAngle()
    {
        xDegrees -= Input.GetAxis("Mouse Y") * speed * friction;
        yDegrees += Input.GetAxis("Mouse X") * speed * friction;

        //limit rotation angles
        if(xDegrees > 0)
        {
            xDegrees = 0;
        }
        else if( xDegrees < -60)
        {
            xDegrees = -60;
        }

        if(yDegrees < -30)
        {
            yDegrees = -30;
        }
        else if(yDegrees > 30)
        {
            yDegrees = 30;
        }

        fromRotation = transform.rotation;
        toRotation = Quaternion.Euler(xDegrees, yDegrees, 0);
        transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);
    }

    void CalculatePower()
    {
        float verticalInput = Input.GetAxis("Vertical");
        firePower += verticalInput;
        if(firePower > 100)
        {
            firePower = 100;
        }
        else if(firePower < 0)
        {
            firePower = 0;
        }
    }

    void UpdatePowerText()
    {

        currentPowerText.text = "Power: " + ((int) firePower);
    }

    void UpdateAngleText()
    {
        
        currentAngleText.text = "Current Angle: " + (-(int) xDegrees);
    }
}
