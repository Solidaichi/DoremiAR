using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public Simulation simulation { get; set; }
    public Param param { get; set; }
    public Vector3 pos { get; private set; }
    public Vector3 velocity { get; private set; }
    Vector3 accel = Vector3.zero;
    List<Boid> neighbors = new List<Boid>();

    void Start()
    {
        //Debug.Log(simulation);
        pos = transform.position;
        velocity = transform.forward * param.initSpeed;
    }

    void Update()
    {
        // 近隣の個体を探して neighbors リストを更新
        //UpdateNeighbors();

        // 壁に当たりそうになったら向きを変える
        UpdateWalls();

        // 近隣の個体から離れる
        //UpdateSeparation();

        // 近隣の個体と速度を合わせる
        //UpdateAlignment();

        // 近隣の個体の中心に移動する
        //UpdateCohesion();

        // 上記 4 つの結果更新された accel を velocity に反映して位置を動かす
        UpdateMove();
    }


    void UpdateMove()
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
        Debug.Log("UpdateMove");
    }

    /*private void UpdateAlignment()
    {
        throw new NotImplementedException();
    }

    private void UpdateSeparation()
    {
        throw new NotImplementedException();
    }*/

    private void UpdateWalls()
    {
        if (!simulation) { return;  }

        var scale = param.wallScale * 0.5f;
        accel +=
            CalcAccelAgainstWall(-scale - pos.x, Vector3.right) +
            CalcAccelAgainstWall(-scale - pos.y, Vector3.up) +
            CalcAccelAgainstWall(-scale - pos.z, Vector3.forward) +
            CalcAccelAgainstWall(+scale - pos.x, Vector3.left) +
            CalcAccelAgainstWall(+scale - pos.y, Vector3.down) +
            CalcAccelAgainstWall(+scale - pos.z, Vector3.back);
    }

    Vector3 CalcAccelAgainstWall(float distance, Vector3 dir)
    {
        if (distance < param.wallDistance)
        {
            return dir * (param.wallWeight / Mathf.Abs(distance / param.wallDistance));
        }
        return Vector3.zero;
    }

    /*private void UpdateNeighbors()
    {
        throw new NotImplementedException();
    }*/
}
