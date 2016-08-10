using UnityEngine;
using System.Collections;
// quite the redundant class now
public class TapManager : MonoBehaviour {

    private int refreshRate;
    private int frameCounter;
    private int tapCounter;

    public avatarManager avatar;

	// Use this for initialization
	void Start () {
        refreshRate = 20;
        frameCounter = 0;
        tapCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
        frameCounter++;
	    if (Input.GetKeyUp(KeyCode.E))
        {
            tapCounter++;
        }
        if (frameCounter == refreshRate)
        {
            frameCounter = 0;
            SignalAvatar();
            tapCounter = 0;
        }
    }

    private void SignalAvatar()
    {
        //avatar.RecieveTaps(tapCounter, refreshRate);
    }
}
