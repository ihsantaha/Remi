﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class SphereRaycastController2D : MonoBehaviour
{
    // --------------------------------------------------------------------------------
    // Struct
    // --------------------------------------------------------------------------------

    public struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }




    // --------------------------------------------------------------------------------
    // Fields
    // --------------------------------------------------------------------------------

    public LayerMask collisionMask;
    public CircleCollider2D circleCollider;
    public RaycastOrigins raycastOrigins;

    public const float padding = .015f;
    const  float dstBetweenRays = .1f;
    //[HideInInspector]
    public int horizontalRayCount;
    //[HideInInspector]
    public int verticalRayCount;
    //[HideInInspector]
    public float horizontalRaySpacing;
    //[HideInInspector]
    public float verticalRaySpacing;




    // --------------------------------------------------------------------------------
    // Methods
    // --------------------------------------------------------------------------------

    public virtual void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }


    public virtual void Start()
    {
        CalculateRaySpacing();
    }


    public void CalculateRaySpacing()
    {
        Bounds bounds = circleCollider.bounds;
        bounds.Expand(padding * -2);

        float boundsWidth = bounds.size.x;
        float boundsHeight = bounds.size.y;

        horizontalRayCount = Mathf.RoundToInt(boundsHeight / dstBetweenRays);
        verticalRayCount = Mathf.RoundToInt(boundsWidth / dstBetweenRays);

        horizontalRaySpacing = boundsHeight / (horizontalRayCount - 1);
        verticalRaySpacing = boundsWidth / (verticalRayCount - 1);
    }


    // To be used by child class (Controller2D)
    public void UpdateRaycastOrigins()
    {
        Bounds bounds = circleCollider.bounds;
        bounds.Expand(padding * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }
}