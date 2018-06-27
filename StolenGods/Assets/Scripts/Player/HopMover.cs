using UnityEngine;
using System.Collections;

public class HopMover : RaycastBoxProjector 
{
    public delegate void HopAction();
    public event HopAction OnHopEnded;
    public event HopAction OnHopStarted;
    public event HopAction OnHopHalfOver;

    public float baseHopHeight;
    public float baseHopDuration;
    public float baseHopSpeed;

    private float currentHopHeight;
    private float currentHopDuration;
    private float currentHopSpeed;

    private bool resetHopHeightBeforeNextHop = true;
    private bool resetHopDurationBeforeNextHop = true;

    private float t = 0;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private bool halfHopSignaled = false;

    private bool isHopping = false;
    public bool IsHopping
    {
        get
        {
            return isHopping;
        }
    }


    #region Coroutine Hops

    public IEnumerator HopByDuration(Vector2 end, float heightMultiplier, float durationMultiplier)
    {
        if (heightMultiplier < 0)
            heightMultiplier = 0f;

        if (durationMultiplier < 0)
            durationMultiplier = 0f;

        t = 0;
        currentHopHeight = baseHopHeight * heightMultiplier;
        currentHopDuration = baseHopDuration * durationMultiplier;
        startPosition = transform.position;
        endPosition = end;
        halfHopSignaled = false;
        isHopping = true;

        if (OnHopStarted != null)
        {
            OnHopStarted();
        }

        while(IsHopping)
        {
            t += Time.deltaTime / currentHopDuration;
            Vector2 frameLocation = GetLocationAtSpecificT(startPosition, endPosition, t);

            CheckIfHopHasEnded();
            CheckIfHopIsHalfOver();

            Vector2 velocity = frameLocation - (Vector2)transform.position;
            transform.Translate(velocity);

            yield return null;
        }
    }

    public IEnumerator HopByDuration(Vector2 end, float heightMultiplier)
    {
        IEnumerator coroutine = HopByDuration(end, heightMultiplier, 1);
        StartCoroutine(coroutine);
        yield return null;
    }

    public IEnumerator HopByDuration(Vector2 end)
    {
        IEnumerator coroutine = HopByDuration(end, 1, 1);
        StartCoroutine(coroutine);
        yield return null;
    }

    #endregion


    public void StartDurationBasedHop(Vector2 end)
    {
        StartDurationBasedHop(end, baseHopHeight, baseHopDuration);
    }

    public void StartDurationBasedHop(Vector2 end, float height)
    {
        StartDurationBasedHop(end, height, baseHopDuration);
    }

    public void StartDurationBasedHop(Vector2 end, float height, float duration)
    {
        t = 0;
        currentHopHeight = height;
        currentHopDuration = duration;
        startPosition = transform.position;
        endPosition = end;
        halfHopSignaled = false;
        isHopping = true;

        if (OnHopStarted != null)
        {
            OnHopStarted();
        }

    }

    /// <summary>
    /// Allows you to modify the duration of the jump from the base duration. Anything over 1 increases, anything under 1 but above 0 decreases.
    /// </summary>
    /// <param name="end"></param>
    /// <param name="durationModifier"></param>
    public void StartDurationBasedHopWithModifiedDuration(Vector2 end, float durationModifier)
    {
        StartDurationBasedHopWithModifiedDuration(end, baseHopHeight, durationModifier);
    }

    public void StartDurationBasedHopWithModifiedDuration(Vector2 position, float height, float durationModifier)
    {
        if (durationModifier <= 0)
        {
            Debug.LogWarning("WARNING: durationModifier must be positive");
            durationModifier *= -1;
        }

        float modifiedDuration = baseHopDuration * durationModifier;
        StartDurationBasedHop(position, height, modifiedDuration);
    }

    public void StartSpeedBasedHop(Vector2 end)
    {
        StartSpeedBasedHop(end, baseHopHeight, baseHopSpeed);
    }

    public void StartSpeedBasedHop(Vector2 end, float height, float speed)
    {
        float distance = Vector2.Distance((Vector2)transform.position, end);
        float duration = distance / speed;

        StartDurationBasedHop(end, height, duration);
    }

    /*public bool CheckIfHopPathClear(Bounds bounds, Vector2 start, Vector2 end, LayerMask jumpObstacleLayer, LayerMask destinationObstacleLayer, float castNumberMultiplier = 1f, float boundsExpansion = 0.0f, bool debugBoxes = false, bool useSpecialColor = false)
    {
        return CheckIfHopPathClear(bounds, start, end, jumpObstacleLayer, destinationObstacleLayer, baseHopHeight, castNumberMultiplier, boundsExpansion, debugBoxes, useSpecialColor);
    }
    */

    public bool CheckIfHopPathClear(Bounds bounds, Vector2 start, Vector2 end, LayerMask jumpObstacleLayer, LayerMask destinationObstacleLayer, float heightMultiplier = 1f, float castNumberMultiplier = 1f, float boundsExpansion = 0.0f, bool debugBoxChecks = false)
    {
        if(heightMultiplier < 0)
            heightMultiplier = 0f;

        currentHopHeight = baseHopHeight * heightMultiplier;

        if(castNumberMultiplier <= 0f)
        {
            castNumberMultiplier = 1f;
        }

        bool isClear = true;

        Vector2 overallDirection = (end - start).ClosestCardinalDirection().normalized;
        //Debug.DrawRay(transform.position, overallDirection, Color.red, 4f);


        float distance = Vector2.Distance(start, end);

        float boundsWidth = Mathf.Abs(bounds.max.x - bounds.min.x);
        float boundsHeight = Mathf.Abs(bounds.max.y - bounds.min.y);

        float boundsProportion = 0f;
        if (overallDirection.x != 0)
        {
            boundsProportion = boundsWidth;

        }
        else
        {
            boundsProportion = boundsHeight;
        }

        boundsWidth += boundsExpansion;
        boundsHeight += boundsExpansion;

        int baseCastNumber = Mathf.RoundToInt(distance / boundsProportion);
        int modifiedCastNumber = Mathf.RoundToInt(baseCastNumber * castNumberMultiplier);
        float tIncrement = 1f / baseCastNumber;

        float timeStep = 0;
        for(int i = 0; i <= baseCastNumber; i++)
        {
            timeStep = i * tIncrement;
            Vector2 checkLocation = GetLocationAtSpecificT(start, end, timeStep);
            SuperDebugger.DrawPlus(checkLocation, Color.yellow, 0.3f, 1f);

            Vector2 checkMin = new Vector2(checkLocation.x - (.5f * boundsWidth), checkLocation.y - (.5f * boundsHeight));
            Vector2 checkMax = new Vector2(checkLocation.x + (.5f * boundsWidth), checkLocation.y + (.5f * boundsHeight));
            
            if(debugBoxChecks)
            {
                SuperDebugger.DrawBox(checkMin, checkMax, Color.black, 1f);
            }


            Collider2D foundCollider = Physics2D.OverlapArea(checkMin, checkMax, jumpObstacleLayer);
            if(foundCollider != null)
            {
                isClear = false;
                if (debugBoxChecks)
                {
                    SuperDebugger.DrawBox(checkMin, checkMax, Color.blue, 1f);
                }
            }

            //Check destination
            if(i == baseCastNumber)
            {
                if(debugBoxChecks)
                {
                    if(isClear)
                    {
                        SuperDebugger.DrawBox(checkMin, checkMax, Color.green, 1f);
                    }
                    else
                    {
                        SuperDebugger.DrawBox(checkMin, checkMax, Color.red, 1f);
                    }
                }
                foundCollider = null;
                foundCollider = Physics2D.OverlapArea(checkMin, checkMax, destinationObstacleLayer);
                if (foundCollider != null)
                {
                    isClear = false;
                }
            }
        }

        currentHopHeight = baseHopHeight;
        
        return isClear;
    }

    public Vector2 GetLocationAtSpecificT(Vector2 start, Vector2 end, float timeStep)
    {
        Vector2 lerpLocation = Vector2.Lerp(start, end, timeStep);
        lerpLocation.y += currentHopHeight * Mathf.Sin(Mathf.Clamp01(timeStep) * Mathf.PI);
        return lerpLocation;
    }

    public void Move()
    {
        t += Time.deltaTime / currentHopDuration;
        Vector2 frameLocation = GetLocationAtSpecificT(startPosition, endPosition, t);

        CheckIfHopHasEnded();
        CheckIfHopIsHalfOver();

        Vector2 velocity = frameLocation - (Vector2)transform.position;
        transform.Translate(velocity);
    }

    private void CheckIfHopHasEnded()
    {
        if (t >= 1)
        {
            isHopping = false;
            if (OnHopEnded != null)
            {
                OnHopEnded();
            }
        }
    }

    private void CheckIfHopIsHalfOver()
    {
        if(!halfHopSignaled)
        {
            if(t >= 0.5f)
            {
                halfHopSignaled = true;

                if (OnHopHalfOver != null)
                {
                    OnHopHalfOver();
                }
            }

        }
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;
    }

}
