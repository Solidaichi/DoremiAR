using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public Simulation simulation { get; set; }
    public Param param { get; set; }
    public Vector3 pos { get; set; }
    public Vector3 velocity { get; private set; }
    public Vector3 accel = Vector3.zero;
    List<Boid> neighbors = new List<Boid>();


    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        velocity = transform.forward * param.initSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // 近隣の個体を探してneighborsリストを更新
        /*UpdateNeighbors();

        // 壁に当たりそうになったら向きを変える
        UpdateWalls();

        // 近隣の個体から離れる
        UpdateSeparation();

        // 近隣の個体と速度を合わせる
        UpdateAlignment();
        */
        //　上記4つの結果更新されたaccelをvelocityに反映して位置を動かす
        UpdateMove();

    }

    private void UpdateMove()
    {
        var dt = Time.deltaTime;

        velocity += accel * dt;
        var dir = velocity.normalized;
        var speed = velocity.magnitude;
        velocity = Mathf.Clamp(speed, param.minSpeed, param.maxSpeed) * dir;
        pos += velocity * dt;

        var rot = Quaternion.LookRotation(velocity);
        transform.SetPositionAndRotation(pos, rot);

        accel = Vector3.zero;
    }

    /*private void UpdateAlignment()
    {
        throw new NotImplementedException();
    }

    private void UpdateSeparation()
    {
        throw new NotImplementedException();
    }

    private void UpdateWalls()
    {
        throw new NotImplementedException();
    }

    private void UpdateNeighbors()
    {
        throw new NotImplementedException();
    }*/
}
