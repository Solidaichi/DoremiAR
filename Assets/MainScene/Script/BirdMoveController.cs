﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]


public class BirdMoveController : MonoBehaviour
{
    [SerializeField] float idleSpeed, turnSpeed, switchSeconds, idleRatio;
    [SerializeField] Vector2 animSpeedMinMax, moveSpeedMinMax, changeAnimEveryFromTo, changeTargetEveryFromto;
    [SerializeField] Vector2 radiusMinMax;
    [SerializeField] Vector2 yMinMax;
    [SerializeField] Transform homeTarget, flyingTarget;

    [SerializeField]  public bool returnToBase = false;
    [SerializeField] public float randomBaseOffset = 5, delayStart = 0f;

    private Animator animator;
    private Rigidbody rigidbody;
    private Vector3 rotateTarget, position, direction, velocity, randomizedBase; 
    private Quaternion lookRotation;

    public float changeTarget = 0f, changeAnim = 0f, timeSinceTarget = 0f, timeSinceAnim = 0f, prevAnim, currentAnim = 0f, prevSpeed, speed, zturn, prevz, turnSpeedBackup;
    public float distanceFromBase, distanceFromTarget;

      

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        turnSpeedBackup = turnSpeed;
        direction = Quaternion.Euler(transform.eulerAngles) * (Vector3.forward);

        if (delayStart < 0f) { rigidbody.velocity = idleSpeed * direction; }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (delayStart > 0f)
        {
            delayStart -= Time.fixedDeltaTime;
            return;
        }

        distanceFromBase = Vector3.Magnitude(randomizedBase - rigidbody.position);
        distanceFromTarget = Vector3.Magnitude(flyingTarget.position - rigidbody.position);

        if (changeAnim < 0f)
        {
            prevAnim = currentAnim;
            currentAnim = ChangeAnim(currentAnim);
            changeAnim = Random.Range(changeAnimEveryFromTo.x, changeAnimEveryFromTo.y);
            timeSinceAnim = 0f;
            prevSpeed = speed;

            if (currentAnim == 0)
            { 
                speed = idleSpeed;
            } else { 
                speed = Mathf.Lerp(moveSpeedMinMax.x, moveSpeedMinMax.y, (currentAnim - animSpeedMinMax.x) / (animSpeedMinMax.y - animSpeedMinMax.x)); 
            }          
        }

        if (changeTarget < 0f)
        {
            rotateTarget = ChangeDirection(rigidbody.transform.position);

            if (returnToBase)
            {
                changeTarget = 2.0f;
            }else
            {
                changeTarget = Random.Range(changeTargetEveryFromto.x, changeTargetEveryFromto.y);
            }
            timeSinceTarget = 0f;
        }

        changeAnim -= Time.fixedDeltaTime;
        changeTarget -= Time.fixedDeltaTime;
        timeSinceTarget += Time.fixedDeltaTime;
        timeSinceAnim += Time.fixedDeltaTime;

        if (rotateTarget != Vector3.zero)
        {
            lookRotation = Quaternion.LookRotation(rotateTarget, Vector3.up);
        }

        Vector3 rotation = Quaternion.RotateTowards(rigidbody.transform.rotation, lookRotation, turnSpeed * Time.fixedDeltaTime).eulerAngles;
        rigidbody.transform.eulerAngles = rotation;

        direction = Quaternion.Euler(transform.eulerAngles) * Vector3.forward;
        rigidbody.velocity = Mathf.Lerp(prevSpeed, speed, Mathf.Clamp(timeSinceAnim / switchSeconds, 0f, 1f)) * direction;
    }

    Vector3 ChangeDirection(Vector3 currentPosition)
    {
        Vector3 newDir;
        float angleXZ = Random.Range(-Mathf.PI, Mathf.PI);
        float angleY = Random.Range(-Mathf.PI / 48f, Mathf.PI / 48f);

        newDir = Mathf.Sin(angleXZ) * Vector3.forward + Mathf.Cos(angleXZ) * Vector3.right + Mathf.Sin(angleY) * Vector3.up;

        return Vector3.forward;
    }

    float ChangeAnim(float currentAnim)
    {
        float newState;
        if (Random.Range(0f, 1f) < idleRatio)
        {
            newState = 0f;
        } else
        {
            newState = Random.Range(animSpeedMinMax.x, animSpeedMinMax.y);
        }

        if (newState != currentAnim)
        {
            animator.SetFloat("flySpeed", newState);
            if (newState == 0)
            {
                animator.speed = 1f;
            }else
            {
                animator.speed = newState;
            }
        }

        return newState;
    }
}
