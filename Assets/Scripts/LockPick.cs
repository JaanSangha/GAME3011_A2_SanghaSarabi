using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPick : MonoBehaviour
{
    public Transform innerLock;
    public Transform pickTransform;
    public Camera camera;

    private float pickAngle;
    private float openAngle;
    private Vector2 unlockRange;
    private float keyPressedTime = 0;
    private bool canMovePick = true;

    public float lockRange = 5;
    public float maxAngle = 90;
    public float lockSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        ResetLock();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = pickTransform.position;
        if(canMovePick)
        {
            Vector3 dir = Input.mousePosition - camera.WorldToScreenPoint(transform.position);

            pickAngle = Vector3.Angle(dir, Vector3.up);

            Vector3 cross = Vector3.Cross(Vector3.up, dir);
            if(cross.z < 0)
            {
                pickAngle = -pickAngle;
            }

            pickAngle = Mathf.Clamp(pickAngle, -maxAngle, maxAngle);

            Quaternion pinRotation = Quaternion.AngleAxis(pickAngle, Vector3.forward);
            transform.rotation = pinRotation;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            canMovePick = false;
            keyPressedTime = 1;
            print("Current angle: " + pickAngle + "Needed angle: " + openAngle);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            canMovePick = true;
            keyPressedTime = 0;
        }

        float percentage = Mathf.Round(100 - Mathf.Abs(((pickAngle - openAngle) / 100) * 100));
        float lockRotation = ((percentage / 100) * maxAngle) * keyPressedTime;
        float maxRotation = (percentage / 100) * maxAngle;

        float lockLerp = Mathf.Lerp(innerLock.eulerAngles.z, lockRotation, Time.deltaTime * lockSpeed);
        innerLock.eulerAngles = new Vector3(0, 0, lockLerp);

        if (lockLerp >= maxRotation - 1)
        {
            if (pickAngle < unlockRange.y && pickAngle > unlockRange.x)
            {
                Debug.Log("Unlocked!");
                ResetLock();

                canMovePick = true;
                keyPressedTime = 0;
            }
            else
            {
                float randomRotation = Random.insideUnitCircle.x;
                transform.eulerAngles += new Vector3(0, 0, Random.Range(-randomRotation, randomRotation));
            }
        }
    }

    void ResetLock()
    {
        openAngle = Random.Range(-maxAngle + lockRange, maxAngle - lockRange);
        unlockRange = new Vector2(openAngle - lockRange, openAngle + lockRange);
    }
}
