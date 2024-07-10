using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMove : MonoBehaviour
{
    private Transform PlayerVehicle;
    public int Offset = 150;

    // Start is called before the first frame update
    void Start()
    {
        PlayerVehicle = GameObject.Find("RallyCarforAllterrains").transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(PlayerVehicle.position.x, PlayerVehicle.position.y + Offset, PlayerVehicle.position.z);
    }
}
