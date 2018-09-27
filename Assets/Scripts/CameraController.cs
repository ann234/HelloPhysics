using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float speedX = -20.0f;
    public float speedY = -20.0f;
    public float speedZ = 100.0f;

    private Vector3 minXYZ;
    private Vector3 maxXYZ;
    private float xPerZ;
    private float minYPerZ, maxYPerZ;

    //Camera position
    private Vector3 camera_pos;

    void Awake()
    {
        minXYZ.z = -9.0f;
        maxXYZ.z = -3.0f;

        float subZ = Mathf.Abs(maxXYZ.z - minXYZ.z);

        xPerZ = 5.0f / subZ;
        minYPerZ = 1.5f / subZ;
        maxYPerZ = 3.0f / subZ;
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        camera_pos = Camera.main.transform.position;

        //Change min,max camera x,y position dynamically
        minXYZ.x = -xPerZ * (camera_pos.z + 9);
        maxXYZ.x = xPerZ * (camera_pos.z + 9);
        minXYZ.y = 4.5f - minYPerZ * (camera_pos.z + 9);
        maxXYZ.y = 7.0f + maxYPerZ * (camera_pos.z + 9);

        if (Global_Variable.curCtrlMode == ControlMode.CONTROL_MODE_CAMERA)
        {
            StartCoroutine("moveCamera");
        }
    }

    IEnumerator moveCamera()
    {
        camera_pos = Camera.main.transform.position;

        if (Input.GetMouseButton(2))
        {
            float xAxisValue = Input.GetAxis("Mouse X") * speedX * Time.deltaTime;
            float yAxisValue = Input.GetAxis("Mouse Y") * speedY * Time.deltaTime;
            camera_pos += new Vector3(xAxisValue, yAxisValue, 0.0f);
            camera_pos.x = Mathf.Clamp(camera_pos.x, minXYZ.x, maxXYZ.x); //camera_pos.x의 값이 min과 max사이의 값 밖으로 나가지 못하게 한다.
            camera_pos.y = Mathf.Clamp(camera_pos.y, minXYZ.y, maxXYZ.y);

            transform.position = camera_pos;

            yield return null;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float zValue = Input.GetAxis("Mouse ScrollWheel") * speedZ * Time.deltaTime;
            camera_pos += new Vector3(0, 0, zValue);

            //if (Input.GetAxis("Mouse ScrollWheel") > 0)
            //    zValue++;
            //else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            //    zValue--;

            camera_pos.z = Mathf.Clamp(camera_pos.z, minXYZ.z, maxXYZ.z);

            transform.position = camera_pos;

            yield return null;
        }
    }
}
