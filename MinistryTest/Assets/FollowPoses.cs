using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;

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
    public int numPoses;
    private  Transform[] limbs = new Transform[4];
    // Use this for initialization
    void Start () {
        LoadPoses(posesFileName);
        poseIndex = 0;
        numPoses = 0;
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

    public void GoToNextPose()
    {
        poseIndex++;
    }

    void LoadPoses(string posesFileName)
    {
        poses = new Pose[100];
        numPoses = 0;

        if (!File.Exists(posesFileName))
        {
            Debug.Log("Trying to open nonexistent Pose");
            return;
        }
        try
        {
            string line;
            StreamReader reader = new StreamReader(posesFileName, Encoding.Default);
            using (reader)
            {
                do
                {
                    line = reader.ReadLine();

                    if (line != null)
                    {
                        string[] angles = line.Split(' ');
                        float[] fAngles = new float[angles.Length];
                        for (int i = 0; i < angles.Length; i++)
                        {
                            fAngles[i] = float.Parse(angles[i]);
                        }
                        Pose pose = new Pose();
                        pose.leftLegLower = fAngles[0];
                        pose.leftLegUpper = fAngles[1];
                        pose.rightLegLower = fAngles[2];
                        pose.rightLegUpper = fAngles[3];
                        pose.leftArmLower = fAngles[4];
                        pose.leftArmUpper = fAngles[5];
                        pose.rightArmLower = fAngles[6];
                        pose.rightArmUpper = fAngles[7];
                        poses[numPoses] = pose;
                        numPoses++;
                    }
                } while (line != null);

                reader.Close();
            }

        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }
}
