using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using BeardedManStudios.Forge.Networking.Generated;

public class MirrorController : MonoBehaviour
{
    public Transform RightMirror;
    public Transform LeftMirror;
    public Transform RearMirror;
    public int speed = 10;

    private SerialInputGear SerialScript;
    public bool Serial = false;
    int MirrorInput = 0;
    public bool RightMirr = false;

    public int Index = 0;
    public RawImage MirrorImage;
    public RenderTexture RearMirrorT;
    public RenderTexture RevCamT;
    public GameObject RevCamCanvas;

    // Start is called before the first frame update
    void Start()
    {
        SerialScript = gameObject.GetComponentInParent<SerialInputGear>();
    }

    // Update is called once per frame
    void Update()
    {
        Serial = (SerialScript.Serial) ? true : false;

        Debug.Log("R rot: " + RightMirror.rotation);
        Debug.Log("L rot: " + LeftMirror.rotation);

        //if (networkObject.IsServer)
        //{
        //    //if (Serial)
        //    //{
        //    //    MirrorInput = SerialScript.MirrorInput;
        //    //    networkObject.MirrorInput = MirrorInput;
        //    //    RotateMirrSerial();
        //    //}
        //    //else
        //    //{
        //    //    RotateMirror(RightMirror);
        //    //    RotateMirror(LeftMirror);
        //    //}
        //    if (Input.GetKeyDown(KeyCode.G))
        //    {
        //        RightMirr = !RightMirr;
        //    }

        //    if (Serial)
        //    {
        //        RotateMirrorSerialR(RightMirror);
        //        //networkObject.RightMirror = 1;
        //        RotateMirrorSerialL(LeftMirror);
        //        //networkObject.RightMirror = 0;

        //        //if (RightMirr)
        //        //{
        //        //    RotateMirrorSerialR(RightMirror);
        //        //    networkObject.RightMirror = 1;
        //        //}
        //        //else
        //        //{
        //        //    RotateMirrorSerialL(LeftMirror);
        //        //    networkObject.RightMirror = 0;
        //        //}
        //    }
        //    else
        //    {
        //        if (RightMirr)
        //        {
        //            RotateMirrorR(RightMirror);
        //            networkObject.RightMirror = 1;
        //        }
        //        else
        //        {
        //            RotateMirrorL(LeftMirror);
        //            networkObject.RightMirror = 0;
        //        }
        //    }

        //    Index = int.Parse(GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIScript>().GearVal);

        //    if (Index == 6)
        //    {
        //        MirrorImage.texture = RevCamT;
        //        RevCamCanvas.SetActive(true);
        //        networkObject.RevCam = true;
        //    }
        //    else
        //    {
        //        MirrorImage.texture = RearMirrorT;
        //        RevCamCanvas.SetActive(false);
        //        networkObject.RevCam = false;
        //    }
        //}
        //else
        //{
        //    //RotateMirrNet();
        //    RotateMirrRKeyNet(RightMirror);
        //    RotateMirrLKeyNet(LeftMirror);

        //    //if (networkObject.RightMirror == 1)
        //    //{
        //    //    RotateMirrRKeyNet(RightMirror);
        //    //}
        //    //else
        //    //{
        //    //    RotateMirrLKeyNet(LeftMirror);
        //    //}

            

        //    if (networkObject.RevCam)
        //    {
        //        MirrorImage.texture = RevCamT;
        //        RevCamCanvas.SetActive(true);
        //    }
        //    else
        //    {
        //        MirrorImage.texture = RearMirrorT;
        //        RevCamCanvas.SetActive(false);
        //    }
        //}
    }

    //void RotateMirrRKeyNet(Transform Mirror)
    //{
    //    if (networkObject.UpR == 1 && Mirror.rotation.y > -0.1f)
    //    {
    //        Mirror.Rotate(Vector3.up * speed * Time.deltaTime);
    //    }
    //    if (networkObject.DownR == 1 && Mirror.rotation.y < 0.1f)
    //    {
    //        Mirror.Rotate(Vector3.down * speed * Time.deltaTime);
    //    }
    //    if (networkObject.RightR == 1 && Mirror.rotation.x > -0.1f)
    //    {
    //        Mirror.Rotate(Vector3.right * speed * Time.deltaTime);
    //    }
    //    if (networkObject.LeftR == 1 && Mirror.rotation.x < 0.1f)
    //    {
    //        Mirror.Rotate(Vector3.left * speed * Time.deltaTime);
    //    }
    //}

    //void RotateMirrLKeyNet(Transform Mirror)
    //{
    //    if (networkObject.DownL == 1 && Mirror.rotation.y > -0.1f)
    //    {
    //        Mirror.Rotate(Vector3.down * speed * Time.deltaTime);
    //    }
    //    if (networkObject.UpL == 1 && Mirror.rotation.y < 0.1f)
    //    {
    //        Mirror.Rotate(Vector3.up * speed * Time.deltaTime);
    //    }
    //    if (networkObject.LeftL == 1 && Mirror.rotation.x > -0.1f)
    //    {
    //        Mirror.Rotate(Vector3.left * speed * Time.deltaTime);
    //    }
    //    if (networkObject.RightL == 1 && Mirror.rotation.x < 0.1f)
    //    {
    //        Mirror.Rotate(Vector3.right * speed * Time.deltaTime);
    //    }
    //}

    //void RotateMirrorR(Transform Mirror)
    //{
    //    //Mirror.eulerAngles = new Vector3(0, Time.deltaTime*100, 0);
    //    //Mirror.rotation = new Quaternion(0, -0.7f, 0, 0);

    //    if (Input.GetKey(KeyCode.J) && Mirror.rotation.y > -0.1f)
    //    {
    //        Mirror.Rotate(Vector3.up * speed * Time.deltaTime);
    //        networkObject.UpR = 1;
    //        //networkObject.Down = 0;
    //        //networkObject.Right = 0;
    //        //networkObject.Left = 0;
    //    }
    //    else
    //    {
    //        networkObject.UpR = 0;
    //    }

    //    if (Input.GetKey(KeyCode.K) && Mirror.rotation.y < 0.1f)
    //    {
    //        Mirror.Rotate(Vector3.down * speed * Time.deltaTime);
    //        //networkObject.Up = 0;
    //        networkObject.DownR = 1;
    //        //networkObject.Right = 0;
    //        //networkObject.Left = 0;
    //    }
    //    else
    //    {
    //        networkObject.DownR = 0;
    //    }
            
    //    if (Input.GetKey(KeyCode.B) && Mirror.rotation.x > -0.1f)
    //    {
    //        Mirror.Rotate(Vector3.right * speed * Time.deltaTime);
    //        //networkObject.Up = 0;
    //        //networkObject.Down = 0;
    //        networkObject.RightR = 1;
    //        //networkObject.Left = 0;
    //    }
    //    else
    //    {
    //        networkObject.RightR = 0;
    //    }

    //    if (Input.GetKey(KeyCode.M) && Mirror.rotation.x < 0.1f)
    //    {
    //        Mirror.Rotate(Vector3.left * speed * Time.deltaTime);
    //        //networkObject.Up = 0;
    //        //networkObject.Down = 0;
    //        //networkObject.Right = 0;
    //        networkObject.LeftR = 1;
    //    }
    //    else
    //    {
    //        networkObject.LeftR = 0;
    //    }
            
    //}

    //void RotateMirrorL(Transform Mirror)
    //{
    //    //Mirror.eulerAngles = new Vector3(0, Time.deltaTime*100, 0);
    //    //Mirror.rotation = new Quaternion(0, -0.7f, 0, 0);

    //    if (Input.GetKey(KeyCode.J) && Mirror.rotation.y > -0.1f)
    //    {
    //        Mirror.Rotate(Vector3.down * speed * Time.deltaTime);
    //        //networkObject.Up = 0;
    //        networkObject.DownL = 1;
    //        //networkObject.Right = 0;
    //        //networkObject.Left = 0;
    //    }
    //    else
    //    {
    //        networkObject.DownL = 0;
    //    }

    //    if (Input.GetKey(KeyCode.K) && Mirror.rotation.y < 0.1f)
    //    {
    //        Mirror.Rotate(Vector3.up * speed * Time.deltaTime);
    //        networkObject.UpL = 1;
    //        //networkObject.Down = 0;
    //        //networkObject.Right = 0;
    //        //networkObject.Left = 0;
    //    }
    //    else
    //    {
    //        networkObject.UpL = 0;
    //    }

    //    if (Input.GetKey(KeyCode.B) && Mirror.rotation.x > -0.1f)
    //    {
    //        Mirror.Rotate(Vector3.left * speed * Time.deltaTime);
    //        //networkObject.Up = 0;
    //        //networkObject.Down = 0;
    //        //networkObject.Right = 0;
    //        networkObject.LeftL = 1;
    //    }
    //    else
    //    {
    //        networkObject.LeftL = 0;
    //    }

    //    if (Input.GetKey(KeyCode.M) && Mirror.rotation.x < 0.1f)
    //    {
    //        Mirror.Rotate(Vector3.right * speed * Time.deltaTime);
    //        //networkObject.Up = 0;
    //        //networkObject.Down = 0;
    //        networkObject.RightL = 1;
    //        //networkObject.Left = 0;
    //    }
    //    else
    //    {
    //        networkObject.RightL = 0;
    //    }

    //}

    //void RotateMirrorSerialR(Transform Mirror)
    //{
    //    //Mirror.eulerAngles = new Vector3(0, Time.deltaTime*100, 0);
    //    //Mirror.rotation = new Quaternion(0, -0.7f, 0, 0);

    //    if (SerialScript.MirrorLL == 1 && Mirror.rotation.y > -0.1f)
    //    {
    //        Mirror.Rotate(Vector3.up * speed * Time.deltaTime);
    //        networkObject.UpR = 1;
    //        //networkObject.Down = 0;
    //        //networkObject.Right = 0;
    //        //networkObject.Left = 0;
    //    }
    //    else
    //    {
    //        networkObject.UpR = 0;
    //    }

    //    if (SerialScript.MirrorLR == 1 && Mirror.rotation.y < 0.1f)
    //    {
    //        Mirror.Rotate(Vector3.down * speed * Time.deltaTime);
    //        //networkObject.Up = 0;
    //        networkObject.DownR = 1;
    //        //networkObject.Right = 0;
    //        //networkObject.Left = 0;
    //    }
    //    else
    //    {
    //        networkObject.DownR = 0;
    //    }

    //    if (SerialScript.MirrorLD == 1 && Mirror.rotation.x > -0.1f)
    //    {
    //        Mirror.Rotate(Vector3.right * speed * Time.deltaTime);
    //        //networkObject.Up = 0;
    //        //networkObject.Down = 0;
    //        networkObject.RightR = 1;
    //        //networkObject.Left = 0;
    //    }
    //    else
    //    {
    //        networkObject.RightR = 0;
    //    }

    //    if (SerialScript.MirrorLU == 1 && Mirror.rotation.x < 0.1f)
    //    {
    //        Mirror.Rotate(Vector3.left * speed * Time.deltaTime);
    //        //networkObject.Up = 0;
    //        //networkObject.Down = 0;
    //        //networkObject.Right = 0;
    //        networkObject.LeftR = 1;
    //    }
    //    else
    //    {
    //        networkObject.LeftR = 0;
    //    }

    //}

    //void RotateMirrorSerialL(Transform Mirror)
    //{
    //    //Mirror.eulerAngles = new Vector3(0, Time.deltaTime*100, 0);
    //    //Mirror.rotation = new Quaternion(0, -0.7f, 0, 0);

    //    if (SerialScript.MirrorRR == 1 && Mirror.rotation.y > -0.1f)
    //    {
    //        Mirror.Rotate(Vector3.down * speed * Time.deltaTime);
    //        //networkObject.Up = 0;
    //        networkObject.DownL = 1;
    //        //networkObject.Right = 0;
    //        //networkObject.Left = 0;
    //    }
    //    else
    //    {
    //        networkObject.DownL = 0;
    //    }

    //    if (SerialScript.MirrorRL == 1 && Mirror.rotation.y < 0.1f)
    //    {
    //        Mirror.Rotate(Vector3.up * speed * Time.deltaTime);
    //        networkObject.UpL = 1;
    //        //networkObject.Down = 0;
    //        //networkObject.Right = 0;
    //        //networkObject.Left = 0;
    //    }
    //    else
    //    {
    //        networkObject.UpL = 0;
    //    }

    //    if (SerialScript.MirrorRU == 1 && Mirror.rotation.x > -0.1f)
    //    {
    //        Mirror.Rotate(Vector3.left * speed * Time.deltaTime);
    //        //networkObject.Up = 0;
    //        //networkObject.Down = 0;
    //        //networkObject.Right = 0;
    //        networkObject.LeftL = 1;
    //    }
    //    else
    //    {
    //        networkObject.LeftL = 0;
    //    }

    //    if (SerialScript.MirrorRD == 1 && Mirror.rotation.x < 0.1f)
    //    {
    //        Mirror.Rotate(Vector3.right * speed * Time.deltaTime);
    //        //networkObject.Up = 0;
    //        //networkObject.Down = 0;
    //        networkObject.RightL = 1;
    //        //networkObject.Left = 0;
    //    }
    //    else
    //    {
    //        networkObject.RightL = 0;
    //    }

    //}

    void RotateMirrSerial()
    {
        // Left Mirror
        if (MirrorInput == 1 && LeftMirror.rotation.x > -0.1f)
            LeftMirror.Rotate(Vector3.left * speed * Time.deltaTime); // Up
        if (MirrorInput == 2 && LeftMirror.rotation.x < 0.1f)
            LeftMirror.Rotate(Vector3.right * speed * Time.deltaTime); // Down
        if (MirrorInput == 3 && LeftMirror.rotation.y > -0.1f)
            LeftMirror.Rotate(Vector3.up * speed * Time.deltaTime); // Left
        if (MirrorInput == 4 && LeftMirror.rotation.y < 0.1f)
            LeftMirror.Rotate(Vector3.down * speed * Time.deltaTime); // Right
        // Right Mirror
        if (MirrorInput == 5 && RightMirror.rotation.x > -0.1f)
            RightMirror.Rotate(Vector3.left * speed * Time.deltaTime); // Up
        if (MirrorInput == 6 && RightMirror.rotation.x < 0.1f)
            RightMirror.Rotate(Vector3.right * speed * Time.deltaTime); // Down
        if (MirrorInput == 7 && RightMirror.rotation.y > -0.1f)
            RightMirror.Rotate(Vector3.up * speed * Time.deltaTime); // Left
        if (MirrorInput == 8 && RightMirror.rotation.y < 0.1f)
            RightMirror.Rotate(Vector3.down * speed * Time.deltaTime); // Right

        //// Right Mirror
        //if (SerialScript.MirrorInput == 0)
        //{
        //    //Left Right
        //    if (SerialScript.MirrLeft == 1 && RightMirror.rotation.y > -0.1f)
        //        RightMirror.Rotate(Vector3.up * speed * Time.deltaTime);
        //    if (SerialScript.MirrRight == 1 && RightMirror.rotation.y < 0.1f)
        //        RightMirror.Rotate(Vector3.down * speed * Time.deltaTime);

        //    // Up Down
        //    if (SerialScript.MirrUp == 1 && RightMirror.rotation.x > -0.1f)
        //        RightMirror.Rotate(Vector3.left * speed * Time.deltaTime);
        //    if (SerialScript.MirrDown == 1 && RightMirror.rotation.x < 0.1f)
        //        RightMirror.Rotate(Vector3.right * speed * Time.deltaTime);
        //}
        //// Left Mirror
        //else
        //{
        //    //Left Right
        //    if (SerialScript.MirrLeft == 1 && LeftMirror.rotation.y > -0.1f)
        //        LeftMirror.Rotate(Vector3.up * speed * Time.deltaTime);
        //    if (SerialScript.MirrRight == 1 && LeftMirror.rotation.y < 0.1f)
        //        LeftMirror.Rotate(Vector3.down * speed * Time.deltaTime);

        //    // Up Down
        //    if (SerialScript.MirrUp == 1 && LeftMirror.rotation.x > -0.1f)
        //        LeftMirror.Rotate(Vector3.left * speed * Time.deltaTime);
        //    if (SerialScript.MirrDown == 1 && LeftMirror.rotation.x < 0.1f)
        //        LeftMirror.Rotate(Vector3.right * speed * Time.deltaTime);
        //}
        
    }

    //void RotateMirrNet()
    //{
    //    // Left Mirror
    //    if (networkObject.MirrorInput == 1 && LeftMirror.rotation.x > -0.1f)
    //        LeftMirror.Rotate(Vector3.left * speed * Time.deltaTime); // Up
    //    if (networkObject.MirrorInput == 2 && LeftMirror.rotation.x < 0.1f)
    //        LeftMirror.Rotate(Vector3.right * speed * Time.deltaTime); // Down
    //    if (networkObject.MirrorInput == 3 && LeftMirror.rotation.y > -0.1f)
    //        LeftMirror.Rotate(Vector3.up * speed * Time.deltaTime); // Left
    //    if (networkObject.MirrorInput == 4 && LeftMirror.rotation.y < 0.1f)
    //        LeftMirror.Rotate(Vector3.down * speed * Time.deltaTime); // Right
    //    // Right Mirror
    //    if (networkObject.MirrorInput == 5 && RightMirror.rotation.x > -0.1f)
    //        RightMirror.Rotate(Vector3.left * speed * Time.deltaTime); // Up
    //    if (networkObject.MirrorInput == 6 && RightMirror.rotation.x < 0.1f)
    //        RightMirror.Rotate(Vector3.right * speed * Time.deltaTime); // Down
    //    if (networkObject.MirrorInput == 7 && RightMirror.rotation.y > -0.1f)
    //        RightMirror.Rotate(Vector3.up * speed * Time.deltaTime); // Left
    //    if (networkObject.MirrorInput == 8 && RightMirror.rotation.y < 0.1f)
    //        RightMirror.Rotate(Vector3.down * speed * Time.deltaTime); // Right
    //}
}
