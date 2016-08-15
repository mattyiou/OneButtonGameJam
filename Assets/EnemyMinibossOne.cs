using UnityEngine;
using System.Collections;

public class EnemyMinibossOne : MonoBehaviour {

    public enum ATTACK_STATES
    {
        INACTIVE,
        LASER_BIG,
        LASER_SWIPE,
        LASER_PINS
    };

    private ATTACK_STATES attackState;

    public Vector3 bigLaserPos;

    public LineRenderer laserOne;
    public LineRenderer laserTwo;

    public GameObject laserOneOrigin;
    public GameObject laserTwoOrigin;

    private Vector3 laserOneEnd;
    private Vector3 laserTwoEnd;
    private Vector3 originOne;
    private Vector3 originTwo;

    private float startWidth = 0.2f;
    private float endWidth = 0.2f;

    public float speed = 2f;
    private IEnumerator randomMovement;
    private IEnumerator laserSpray;

	// Use this for initialization
	void Start () {
        attackState = ATTACK_STATES.LASER_SWIPE;
        Attack();
	}
	
	// Update is called once per frame
	void Update () {
        //Attack();
	}

    void Attack()
    {
        switch (attackState)
        {
            case ATTACK_STATES.LASER_BIG:
                StartCoroutine(BigLaserAttack());
                break;
            case ATTACK_STATES.LASER_SWIPE:
                StartCoroutine(LaserSwipeOutwars());
                break;
            case ATTACK_STATES.LASER_PINS:
                StartCoroutine(PinLeftLaser());
                StartCoroutine(PinRightLaser());
                break;
            default: break;
        }
    }

    IEnumerator BigLaserAttack()
    {
        float finalLengthOfLaser = 10f;
        float finalWidth = 3f;
        float curLength = 0f;
        startWidth = 0f;
        endWidth = 0f;
        float curTime = Time.deltaTime;
        float timeOffset = 1.5f;
        float lenOffset = Mathf.Exp(curTime + timeOffset);

        laserOne.SetVertexCount(1);
        laserTwo.SetVertexCount(1);
        laserOne.SetPosition(0, laserOneOrigin.transform.position);
        laserTwo.SetPosition(0, laserTwoOrigin.transform.position);

        laserOneEnd = laserOneOrigin.transform.position;
        laserTwoEnd = laserTwoOrigin.transform.position;
        float originY = laserOneOrigin.transform.position.y;
        bool initLaser = false;

        while (curLength < finalLengthOfLaser)
        {
            if ((bigLaserPos - this.transform.position).magnitude >= (speed * Time.deltaTime))
            {
                this.transform.position += (bigLaserPos - this.transform.position).normalized * (speed * Time.deltaTime);
                originY = laserOneOrigin.transform.position.y;
                laserOne.SetPosition(0, laserOneOrigin.transform.position);
                laserTwo.SetPosition(0, laserTwoOrigin.transform.position);
            }
            else
            {
                if (curLength < finalLengthOfLaser)
                {
                    if (!initLaser)
                    {
                        initLaser = true;
                        laserOneEnd = laserOneOrigin.transform.position;
                        laserTwoEnd = laserTwoOrigin.transform.position;
                    }

                    laserOne.SetVertexCount(2);
                    laserTwo.SetVertexCount(2);
                    curLength = Mathf.Exp(curTime + timeOffset) - lenOffset;
                    laserOneEnd.y = originY - curLength;
                    laserTwoEnd.y = originY - curLength;
                    laserOne.SetPosition(1, laserOneEnd);
                    laserTwo.SetPosition(1, laserTwoEnd);
                    startWidth = curLength / 10;
                    laserOne.SetWidth(startWidth, startWidth);
                    laserTwo.SetWidth(startWidth, startWidth);
                }
                curTime += Time.deltaTime;
            }
            yield return new WaitForEndOfFrame();
        }

        while (endWidth < finalWidth)
        {

            curLength = Mathf.Exp(curTime + timeOffset) - lenOffset;
            endWidth = curLength / 10;
            laserOne.SetWidth(startWidth, endWidth);
            laserTwo.SetWidth(startWidth, endWidth);
            curTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        attackState = ATTACK_STATES.INACTIVE;
        curTime = Time.deltaTime;
        originOne = laserOneOrigin.transform.position;
        originTwo = laserTwoOrigin.transform.position;
        yield return new WaitForSeconds(0.4f);
        while (endWidth > 0)
        {
            startWidth -= Time.deltaTime * Mathf.Exp(curTime + 1);
            endWidth -= Time.deltaTime * Mathf.Exp(curTime + 1);
            if (startWidth < 0)
                startWidth = 0f;
            if (endWidth < 0)
                endWidth = 0f;
            laserOne.SetWidth(startWidth, endWidth);
            laserTwo.SetWidth(startWidth, endWidth);
            curTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator LaserSwipeOutwars()
    {
        float finalLengthOfLaser = 10f;
        startWidth = 0.2f;
        endWidth = 0.2f;
        float verticalSpeed = 4f;
        float laserSpeed = 6f;

        laserOneEnd = laserOneOrigin.transform.position;
        laserTwoEnd = laserTwoOrigin.transform.position;
        originOne = laserOneOrigin.transform.position;
        originTwo = laserTwoOrigin.transform.position;

        laserOne.SetVertexCount(2);
        laserTwo.SetVertexCount(2);
        laserOne.SetPosition(0, originOne);
        laserTwo.SetPosition(0, originTwo);
        laserOne.SetPosition(1, originOne);
        laserTwo.SetPosition(1, originTwo);

        laserOne.SetWidth(startWidth, endWidth);
        laserTwo.SetWidth(startWidth, endWidth);

        while (laserTwoEnd.y > originTwo.y - finalLengthOfLaser)
        {
            laserTwoEnd.y -= Time.deltaTime * verticalSpeed;
            laserTwo.SetPosition(1, laserTwoEnd);
            yield return new WaitForEndOfFrame();
        }

        while (laserTwoEnd.x < originTwo.x + finalLengthOfLaser)
        {
            laserTwoEnd.x += Time.deltaTime * laserSpeed;
            laserTwo.SetPosition(1, laserTwoEnd);
            yield return new WaitForEndOfFrame();
        }

        while (startWidth > 0)
        {
            startWidth -= Time.deltaTime;
            if (startWidth < 0)
                startWidth = 0;
            laserTwo.SetWidth(startWidth, startWidth);
            yield return new WaitForEndOfFrame();
        }
        laserTwo.SetVertexCount(0);

        while (laserOneEnd.y > originOne.y - finalLengthOfLaser)
        {
            laserOneEnd.y -= Time.deltaTime * verticalSpeed;
            laserOne.SetPosition(1, laserOneEnd);
            yield return new WaitForEndOfFrame();
        }

        while (laserOneEnd.x > originOne.x - finalLengthOfLaser)
        {
            laserOneEnd.x -= Time.deltaTime * laserSpeed;
            laserOne.SetPosition(1, laserOneEnd);
            yield return new WaitForEndOfFrame();
        }

        while (startWidth > 0)
        {
            startWidth -= Time.deltaTime;
            if (startWidth < 0)
                startWidth = 0;
            laserOne.SetWidth(startWidth, startWidth);
            yield return new WaitForEndOfFrame();
        }
        laserOne.SetVertexCount(0);

        // copy pasta cause i'm a lazy bum

        laserOneEnd = laserOneOrigin.transform.position;
        laserTwoEnd = laserTwoOrigin.transform.position;
        originOne = laserOneOrigin.transform.position;
        originTwo = laserTwoOrigin.transform.position;

        laserOne.SetVertexCount(2);
        laserTwo.SetVertexCount(2);
        laserOne.SetPosition(0, originOne);
        laserTwo.SetPosition(0, originTwo);
        laserOne.SetPosition(1, originOne);
        laserTwo.SetPosition(1, originTwo);
        startWidth = 0.2f;
        laserOne.SetWidth(startWidth, endWidth);
        laserTwo.SetWidth(startWidth, endWidth);

        while (laserTwoEnd.y > originTwo.y - finalLengthOfLaser)
        {
            laserTwoEnd.y -= Time.deltaTime * verticalSpeed;
            laserTwo.SetPosition(1, laserTwoEnd);
            yield return new WaitForEndOfFrame();
        }

        while (laserTwoEnd.x > originTwo.x - finalLengthOfLaser)
        {
            laserTwoEnd.x -= Time.deltaTime * laserSpeed;
            laserTwo.SetPosition(1, laserTwoEnd);
            yield return new WaitForEndOfFrame();
        }

        while (startWidth > 0)
        {
            startWidth -= Time.deltaTime;
            if (startWidth < 0)
                startWidth = 0;
            laserTwo.SetWidth(startWidth, startWidth);
            yield return new WaitForEndOfFrame();
        }
        laserTwo.SetVertexCount(0);

        while (laserOneEnd.y > originOne.y - finalLengthOfLaser)
        {
            laserOneEnd.y -= Time.deltaTime * verticalSpeed;
            laserOne.SetPosition(1, laserOneEnd);
            yield return new WaitForEndOfFrame();
        }

        while (laserOneEnd.x < originOne.x + finalLengthOfLaser)
        {
            laserOneEnd.x += Time.deltaTime * laserSpeed;
            laserOne.SetPosition(1, laserOneEnd);
            yield return new WaitForEndOfFrame();
        }

        while (startWidth > 0)
        {
            startWidth -= Time.deltaTime;
            if (startWidth < 0)
                startWidth = 0;
            laserOne.SetWidth(startWidth, startWidth);
            yield return new WaitForEndOfFrame();
        }
        laserOne.SetVertexCount(0);

        attackState = ATTACK_STATES.INACTIVE;
    }

    IEnumerator PinLeftLaser()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 4));
            while (true)
            {
                // pin left randomly
                // extend laser
                // update origin for x time
                // release laser
                yield return new WaitForEndOfFrame();
            }
        }
    }

    IEnumerator PinRightLaser()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 4));
            while (true)
            {
                // pin left randomly
                // extend laser
                // update origin for x time
                // release laser
                yield return new WaitForEndOfFrame();
            }
        }
    }

    IEnumerator RandomMovement()
    {
        // create bounds for movement
        // find point in rectangle
        // move to point
        // find new point
        yield return new WaitForEndOfFrame();
    }
}
