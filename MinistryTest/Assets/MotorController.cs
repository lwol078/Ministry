using UnityEngine;
using System.Collections;

public class MotorController : MonoBehaviour {

    public int speed;
    public bool floppy;
    public HingeJoint2D hinge;
    public Transform endPt;
    public float targetAngle;
    public string hAxis;
    public string vAxis;
    public string activator;
    private bool activated;
    public bool playerControlled;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float realSpd = 0;
        float x = Input.GetAxis(hAxis);
        float y = Input.GetAxis(vAxis);
        if(playerControlled)
        {
            if (Input.GetButton(activator) || Input.GetAxis(activator) > 0)
            {
                Debug.Log("Activated");
                activated = true;
                floppy = false;
            }
            else
            {
                activated = false;
            }
            if ((x != 0 || y != 0) && activated)
            {
                Vector2 target = new Vector2(x, y);
                targetAngle = Mathf.Atan2(y, x) * 180 / Mathf.PI;
            }
        }
        else
        {
            floppy = false;
        }
        Vector2 current = endPt.position - this.transform.position;
        float currentAngle = Mathf.Atan2(current.y, current.x) * 180 / Mathf.PI;
        float offset = targetAngle - currentAngle;
        if(Mathf.Abs(offset) > 180)
        {
            float sign = -Mathf.Sign(offset);
            float magnitude = 360-Mathf.Abs(offset);
            offset = sign * magnitude;
        }

        //Debug.Log("CURRENT: " + currentAngle + " TARGET: " + targetAngle + "OFFSET" + offset);

        if (Mathf.Abs(offset) > 1)
            realSpd = speed * offset / -90;


        JointMotor2D motor = hinge.motor;
        motor.motorSpeed = realSpd;
        if(!floppy)
            hinge.motor = motor;
	
	}
}
