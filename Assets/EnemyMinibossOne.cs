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

    public float speed = 1.3f;
    private IEnumerator randomMovement;
    private IEnumerator laserPinLeft;
    private IEnumerator laserPinRight;

	// Use this for initialization
	void Start () {     
        randomMovement = RandomMovement();
        StartCoroutine(randomMovement);
        attackState = ATTACK_STATES.INACTIVE;
        laserPinRight = PinRightLaser();
        laserPinLeft = PinLeftLaser();
        StartCoroutine(Brain());
        //Attack();
	}
	
	// Update is called once per frame
	void Update () {
        //Attack();
        originOne = laserOneOrigin.transform.position;
        originTwo = laserTwoOrigin.transform.position;
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
                StartCoroutine(laserPinLeft);
                StartCoroutine(laserPinRight);
                break;
            default: break;
        }
    }

    IEnumerator Brain()
    {
        int i;
        int j;
        while (true)
        {
            i = Random.Range(1, 7);
            if (i >= 3)
            {
                StartCoroutine(laserPinLeft);
                StartCoroutine(laserPinRight);
                yield return new WaitForSeconds(Random.Range(10, 15));
                StopCoroutine(laserPinLeft);
                StopCoroutine(laserPinRight);
            }
            else
            {
                StopCoroutine(randomMovement);
                if (i == 1)
                    yield return StartCoroutine(BigLaserAttack(true));
                else if (i == 2)
                    yield return StartCoroutine(LaserSwipeOutwars());
                StartCoroutine(randomMovement);
            }
        }

    }

    IEnumerator BigLaserAttack(bool reposition = false)
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
            if (reposition && (bigLaserPos - this.transform.position).magnitude >= (speed * Time.deltaTime))
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
        Vector3 pin = Vector3.zero;
        pin.y = -5.1f;
        float rangeLow = -2.5f;
        float rangeHigh = 2.5f;  

        while (true)
        {
            laserOneEnd = originOne;
            float startWidthOne = 0.13f;
            laserOne.SetVertexCount(2);
            laserOne.SetPosition(0, originOne);
            laserOne.SetPosition(1, laserOneEnd);
            laserOne.SetWidth(startWidthOne, startWidthOne);
            yield return new WaitForSeconds(Random.Range(3, 7));
            // pin left randomly
            pin.x = Random.Range(rangeLow, rangeHigh);
            while ((pin - laserOneEnd).magnitude > Time.deltaTime * 5f)
            {
                // extend laser
                laserOneEnd += (pin - laserOneEnd).normalized * Time.deltaTime * 5f;
                laserOne.SetPosition(0, originOne);
                laserOne.SetPosition(1, laserOneEnd);
                yield return new WaitForEndOfFrame();
            }
            laserOneEnd = pin;
            // wait x time
            float waitTime = Random.Range(3, 6);
            while (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
                laserOne.SetPosition(0, originOne);
                laserOne.SetPosition(1, laserOneEnd);
                yield return new WaitForEndOfFrame();
            }
            // release laser
            while (startWidthOne > 0)
            {
                startWidthOne -= Time.deltaTime;
                if (startWidthOne < 0)
                    startWidthOne = 0;
                laserOne.SetPosition(0, originOne);
                laserOne.SetPosition(1, laserOneEnd);
                laserOne.SetWidth(startWidthOne, startWidthOne);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    IEnumerator PinRightLaser()
    {
        Vector3 pin = Vector3.zero;
        pin.y = -5.1f;
        float rangeLow = -2.5f;
        float rangeHigh = 2.5f;
        laserTwoEnd = originTwo;
        

        while (true)
        {
            laserTwoEnd = originTwo;
            float startWidthTwo = 0.13f;
            laserTwo.SetVertexCount(2);
            laserTwo.SetPosition(0, originTwo);
            laserTwo.SetPosition(1, laserTwoEnd);
            laserTwo.SetWidth(startWidthTwo, startWidthTwo);
            yield return new WaitForSeconds(Random.Range(3, 7));          
            // pin left randomly
            pin.x = Random.Range(rangeLow, rangeHigh);
            while ((pin - laserTwoEnd).magnitude > Time.deltaTime * 5f)
            {
                // extend laser
                laserTwoEnd += (pin - laserTwoEnd).normalized * Time.deltaTime * 5f;
                laserTwo.SetPosition(0, originTwo);
                laserTwo.SetPosition(1, laserTwoEnd);
                yield return new WaitForEndOfFrame();
            }
            laserTwoEnd = pin;
            // wait x time
            float waitTime = Random.Range(3, 6);
            while (waitTime >= 0)
            {
                waitTime -= Time.deltaTime;
                laserTwo.SetPosition(0, originTwo);
                laserTwo.SetPosition(1, laserTwoEnd);
                yield return new WaitForEndOfFrame();
            }
            // release laser
            while (startWidthTwo > 0)
            {
                startWidthTwo -= Time.deltaTime;
                if (startWidthTwo < 0)
                    startWidthTwo = 0;
                laserTwo.SetPosition(0, originTwo);
                laserTwo.SetPosition(1, laserTwoEnd);
                laserTwo.SetWidth(startWidthTwo, startWidthTwo);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    IEnumerator RandomMovement()
    {
        Rect bounds = new Rect(-2.7f, 4.2f, 5.4f, -2.4f);
        float smoothT = 0;
        Vector3 destination = new Vector3(Random.Range(bounds.x, bounds.x + bounds.width),
            Random.Range(bounds.y, bounds.y + bounds.height), 0f);
        while (true)
        {
            if ((destination - this.transform.position).magnitude <= Time.deltaTime * speed)
            {
                destination = new Vector3(Random.Range(bounds.x, bounds.x + bounds.width),
                    Random.Range(bounds.y, bounds.y + bounds.height), 0f);
                smoothT = 0;
            }
            smoothT += Time.deltaTime;
            this.transform.position += (destination - this.transform.position).normalized * speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    //Vector3 SmoothStepV3(Vector3 from, Vector3 to, float spd)
    //{
    //    Vector3 dir = (to - from).normalized;
    //    Vector3 newV3 = Vector3.zero;
    //    newV3.x = Mathf.SmoothStep(from.x, to.x, spd * dir.x);
    //    newV3.y = Mathf.SmoothStep(from.y, to.y, spd * dir.y);
    //    newV3.z = Mathf.SmoothStep(from.z, to.z, spd * dir.z);
    //    return newV3;
    //}
}
