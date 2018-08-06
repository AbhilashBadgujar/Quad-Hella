using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField] float speed;
    [SerializeField] float SensitvityX;
    [SerializeField] float thrustFroce;


    [Header("Spring setting")]
    [SerializeField]
    JointDriveMode jointMode = JointDriveMode.Position;

    [SerializeField]
    float springJoint = 20f;

    [SerializeField]
    float springMaxForce = 40f;


    ConfigurableJoint joint;
    PlayerMotor motor;

    void Start(){
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();

        SetJointSetting(springJoint);
    }

    void Update(){
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 moveHoriontal = transform.right * xMov;
        Vector3 moveVertical = transform.forward * zMov;

        Vector3 velocity = (moveHoriontal + moveVertical).normalized * speed;

        motor.Move(velocity);

        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, yRot, 0f) * SensitvityX;

        motor.Rotation(_rotation);

        float xRot = Input.GetAxisRaw("Mouse Y");

        float _camrotation = xRot * SensitvityX;

        motor.CamRotation(_camrotation);

        Vector3 _thrustFroce = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            _thrustFroce = Vector3.up * thrustFroce;
            SetJointSetting(0f);
        }
        else
        {
            SetJointSetting(springJoint);
        }

        motor.ApplyForce(_thrustFroce);
    }

    void SetJointSetting(float _jointSpring)
    {
        joint.yDrive = new JointDrive { mode = jointMode, positionSpring = _jointSpring,  maximumForce = springMaxForce};
    }
}
