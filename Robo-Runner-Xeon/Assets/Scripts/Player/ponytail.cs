using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ponytail : MonoBehaviour
{
    public int length;
    public LineRenderer lineRend;
    public Vector3[] segmentPositions;
    private Vector3[] segmentV;

    public SpriteRenderer sprite;
    private bool currentFacing;

    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;

    public float wiggleSpeed;
    public float wiggleMagnitude;
    public Transform wiggleDir;

    private void Start()
    {
        lineRend.positionCount = length;
        segmentPositions = new Vector3[length];
        segmentV = new Vector3[length];

        currentFacing = sprite.flipX;
    }

    private void Update()
    {
        bool isFacingLeft = sprite.flipX;

        if (isFacingLeft != currentFacing)
        {
            Vector3 localPos = wiggleDir.localPosition;
            wiggleDir.localPosition = new Vector3(-localPos.x, localPos.y, 1);
            currentFacing = isFacingLeft;
        }
        
        wiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);

        segmentPositions[0] = targetDir.position;
        for (int i = 1; i < segmentPositions.Length; i++)
        {
            segmentPositions[i] = Vector3.SmoothDamp(segmentPositions[i], segmentPositions[i-1] + targetDir.right * targetDist, ref segmentV[i], smoothSpeed);
        }
        lineRend.SetPositions(segmentPositions);
    }

}
