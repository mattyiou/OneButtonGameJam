using UnityEngine;
using System.Collections;

public class ApperShootAndLeave : Enemy {

	const int PHASE_APPEAR=0;
	const int PHASE_SHOOT=1;
	const int PHASE_LEAVE=2;
    const int PHASE_SHOOTING=3;



	public int place=0; //can be from 1 to 5
	private int phase;
	private float speed;
	public float timeToAppear = 1f;
	public float timeToShoot = 2f;
	public float timeToLeave = 1f;
	private float currentTime = 0f;
    private LineRenderer laser;
    private int playerMask = 1 << 9;
	// Use this for initialization
	void Start () {
        laser = this.GetComponent<LineRenderer>();
		phase = PHASE_APPEAR;
		if (place == 0) {
			place = Random.Range(1,6); //maximum is not included
		}
		if (Random.Range (0, 10) > 5) {
			this.transform.position = new Vector3 (-100, this.transform.position.y, 0);
		} else {
			this.transform.position = new Vector3 (Screen.width + 100, this.transform.position.y, 0);		
		}

	}
	
	// Update is called once per delta seconds
	void FixedUpdate () {
		currentTime = currentTime + Time.fixedDeltaTime;
		float newPosition;
		switch (phase) {
		case PHASE_APPEAR:
			newPosition = Mathf.SmoothDamp(this.transform.position.x,(place)-3.5f,ref speed,timeToAppear);
			this.transform.position = new Vector3(newPosition,this.transform.position.y,this.transform.position.z);
			if (currentTime > timeToAppear*8) {
				phase = PHASE_SHOOT;
				currentTime=0;
			}
			break;
		case PHASE_SHOOT:
			phase=PHASE_SHOOTING;
		    currentTime=0;
            StartCoroutine(FireLaser());
			break;
		case PHASE_LEAVE:
			newPosition = Mathf.SmoothDamp(this.transform.position.x,50,ref speed,timeToLeave);
			this.transform.position = new Vector3(newPosition,this.transform.position.y,this.transform.position.z);
			if (currentTime > timeToLeave*8) {
				Destroy(this.gameObject);
			}

			break;
		}
	}

    IEnumerator FireLaser()
    {
        float finalWidth = 0.45f;
        float curWidth = 0.1f;
        float startWidth = 0.1f;
        float widthSpeed = 1f;
        float finalLength = 10f;
        float curLength = 0f;
        float lengthSpeed = 8;

        Vector3 origin = this.transform.position;
        Vector3 end = this.transform.position;
        laser.SetVertexCount(2);
        laser.SetPosition(0, origin);
        laser.SetPosition(1, end);
        laser.SetWidth(startWidth, startWidth);

        //extend laser
        while (curLength < finalLength)
        {
            LaserRaycast(origin, end + Vector3.down * curLength, startWidth, startWidth);
            curLength += lengthSpeed * Time.deltaTime;
            laser.SetPosition(1, end + Vector3.down * curLength);   
            yield return new WaitForEndOfFrame();
        }
        //extend width
        while (curWidth < finalWidth)
        {
            LaserRaycast(origin, end + Vector3.down * curLength, startWidth, curWidth);
            curWidth += widthSpeed * Time.deltaTime;
            laser.SetWidth(0.1f, curWidth);
            yield return new WaitForEndOfFrame();
        }
        //hold laser position for some time
        yield return new WaitForSeconds(0.2f);
        //retract width to 0
        while (curWidth > 0 && startWidth > 0)
        {
            LaserRaycast(origin, end + Vector3.down * curLength, startWidth, curWidth);
            curWidth -= Time.deltaTime;
            startWidth -= Time.deltaTime;
            if (curWidth < 0f) curWidth = 0f;
            if (startWidth < 0f) startWidth = 0f;
            laser.SetWidth(startWidth, curWidth);
            yield return new WaitForEndOfFrame();
        }
        laser.SetVertexCount(0);
        phase = PHASE_LEAVE;
    }

    bool LaserRaycast(Vector3 start, Vector3 end, float sW, float eW)
    {
        float spacingTop = sW / .1f;
        float spacingBot = eW / .1f;
        float topOffset = sW / spacingTop;
        float botOffset = eW / spacingBot;
        Vector3 top = start;
        Vector3 bot = end;
        //top.x -= topOffset;
        bot.x -= botOffset;

        RaycastHit2D hit;

        for (int i = 0; i < spacingBot; i++)
        {
            hit = Physics2D.Raycast(top, (bot - top), (bot - top).magnitude, playerMask);
            //Debug.DrawRay(top, (bot - top), Color.green, 2f);
            if (hit.collider != null)
            {
                hit.collider.GetComponent<avatarManager>().GetHit(1);
                return true;
            }
            //top.x += spacingTop;
            bot.x += botOffset;
        }
        return false;
    }
}
