using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SavePoses : MonoBehaviour {

    private FollowPoses.Pose[] poses;
    private int numPoses;
    private int poseIndex;
	// Use this for initialization
	void Start () {
        poseIndex = 0;
        numPoses = 0;
        poses = new FollowPoses.Pose[100];
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonUp("ConfirmPose"))
        {
            FollowPoses.Pose newPose = new FollowPoses.Pose();
            GameObject player = GameObject.Find("PlayerDoll");
            newPose.leftArmLower = player.transform.Find("LeftArm").Find("Lower").GetComponent<MotorController>().targetAngle;
            newPose.leftArmUpper = player.transform.Find("LeftArm").Find("Upper").GetComponent<MotorController>().targetAngle;
            newPose.rightArmLower = player.transform.Find("RightArm").Find("Lower").GetComponent<MotorController>().targetAngle;
            newPose.rightArmUpper = player.transform.Find("RightArm").Find("Upper").GetComponent<MotorController>().targetAngle;

            newPose.leftLegLower = player.transform.Find("LeftLeg").Find("Lower").GetComponent<MotorController>().targetAngle;
            newPose.leftLegUpper = player.transform.Find("LeftLeg").Find("Upper").GetComponent<MotorController>().targetAngle;
            newPose.rightLegLower = player.transform.Find("RightLeg").Find("Lower").GetComponent<MotorController>().targetAngle;
            newPose.rightLegUpper = player.transform.Find("RightLeg").Find("Upper").GetComponent<MotorController>().targetAngle;

            poses[numPoses] = newPose;
            numPoses++;
        }
        if (Input.GetButtonUp("SaveWalk"))
        {
            string walkName = GameObject.Find("WalkName").GetComponent<InputField>().text;
            string filename = walkName + ".txt";
            if (walkName.Length > 0)
            {
                string[] contents = new string[numPoses];
                for (int i = 0; i < numPoses; i++)
                {
                    string line = poses[i].leftLegLower.ToString();
                    line += " " + poses[i].leftLegUpper.ToString();
                    line += " " + poses[i].rightLegLower.ToString();
                    line += " " + poses[i].rightLegUpper.ToString();
                    line += " " + poses[i].leftArmLower.ToString();
                    line += " " + poses[i].leftArmUpper.ToString();
                    line += " " + poses[i].rightArmLower.ToString();
                    line += " " + poses[i].rightArmUpper.ToString();
                    contents[i] = line;

                }
                Debug.Log(numPoses);
                System.IO.File.WriteAllLines(filename, contents);
                GameObject.Find("SaveText").GetComponent<Text>().enabled = true;
                StartCoroutine(WaitAndHide());
            }
        }

    }

    IEnumerator WaitAndHide()
    {
        yield return new WaitForSeconds(2);
        GameObject.Find("SaveText").GetComponent<Text>().enabled = false;
    }
}
