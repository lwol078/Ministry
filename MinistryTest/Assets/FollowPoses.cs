using UnityEngine;
using System.Collections;
using System.IO;

public class FollowPoses : MonoBehaviour {

    struct Pose
    {
        public float rightLegUpper;
        public float rightLegLower;
        public float leftLegUpper;
        public float leftLegLower;
        public float rightArmUpper;
        public float rightArmLower;
        public float leftArmUpper;
        public float leftArmLower;
    }

    public string posesFileName;
    private Pose[] poses;
    private int poseIndex;
    private  Transform[] limbs = new Transform[4];
    // Use this for initialization
    void Start () {
        if(!File.Exists(posesFileName))
        {
            Debug.Log("Trying to open nonexistent Pose");
            return;
        }
        FileStream posesFile = File.Open(posesFileName,FileMode.Open);
        
        poses = new Pose[100];
        poses[0] = new Pose();
        poses[0].leftArmLower = 20;
        poses[0].leftArmUpper = 40;
        poses[0].rightArmLower = 60;
        poses[0].rightArmUpper = 100;
        poseIndex = 0;

        limbs[0] = transform.Find("RightLeg");
        limbs[1] = transform.Find("LeftLeg");
        limbs[2] = transform.Find("RightArm");
        limbs[3] = transform.Find("LeftArm");
        foreach (Transform limb in limbs)
        {
            Transform upper = limb.Find("Upper");
            Transform lower = limb.Find("Lower");
            upper.GetComponent<MotorController>().playerControlled = false;
            lower.GetComponent<MotorController>().playerControlled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        limbs[0].Find("Upper").GetComponent<MotorController>().targetAngle = poses[poseIndex].rightLegUpper;
        limbs[0].Find("Lower").GetComponent<MotorController>().targetAngle = poses[poseIndex].rightLegLower;
        limbs[1].Find("Upper").GetComponent<MotorController>().targetAngle = poses[poseIndex].leftLegUpper;
        limbs[1].Find("Lower").GetComponent<MotorController>().targetAngle = poses[poseIndex].leftLegLower;

        limbs[2].Find("Upper").GetComponent<MotorController>().targetAngle = poses[poseIndex].rightArmUpper;
        limbs[2].Find("Lower").GetComponent<MotorController>().targetAngle = poses[poseIndex].rightArmLower;
        limbs[3].Find("Upper").GetComponent<MotorController>().targetAngle = poses[poseIndex].leftArmUpper;
        limbs[3].Find("Lower").GetComponent<MotorController>().targetAngle = poses[poseIndex].leftArmLower;
    }

    void GoToNextPose()
    {
        poseIndex++;
    }
}
