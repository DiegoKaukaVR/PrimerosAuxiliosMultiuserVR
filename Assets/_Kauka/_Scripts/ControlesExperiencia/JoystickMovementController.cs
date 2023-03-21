using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class JoystickMovementController : MonoBehaviour
{
    //si se usa movimiento joystick
    [Tooltip("Activar si se quiere mover con joystick y desactivar enableTP del Locomotion Controller")]
    public bool enableMovementJoy;

    // que device (dispositivo) estariamos usando (inspector)
    public XRNode leftHand;
    Vector2 inputAxis;

    //variables movimiento
    public float alturaRig = 0.1f;
    public float speed = 1;
    float gravity = -9.81f;
    float speedCaida;
    public LayerMask groundLayer;
    XROrigin rig;
    CharacterController charaController;
    // Start is called before the first frame update
    void Start()
    {
        charaController = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
    }

    // Update is called once per frame
    void Update()
    {
        // manera mas corta de coger un device mediante inspector
        // definir un device - manera de acceder a los devices - que device coger
        if (enableMovementJoy)
        {
            InputDevice deviceLeftHand = InputDevices.GetDeviceAtXRNode(leftHand);
            deviceLeftHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
        }
    }

    private void FixedUpdate()
    {
        ColliderFollowHeadDevice();

        if (enableMovementJoy)
        {
            Quaternion head = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);
            Vector3 direction = head * new Vector3(inputAxis.x, 0, inputAxis.y);
            charaController.Move(speed * Time.fixedDeltaTime * direction);

            //gravedad
            bool isGround = CheckIfGround();
            if (isGround)
            {
                speedCaida = 0;
            }
            else
            {
                speedCaida += gravity * Time.fixedDeltaTime;
            }
            charaController.Move(Vector3.up* speedCaida * Time.fixedDeltaTime);
        }
    }
    bool CheckIfGround()
    {
        //desde que posicion sale el rayo
        Vector3 rayStart = transform.TransformPoint(charaController.center);
        // la longitud del rayo sea igual a la altura del characterContoller desde el centro pero un 0.01 mayor
        float rayLength = charaController.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, charaController.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }
    void ColliderFollowHeadDevice()
    {
        charaController.height = rig.CameraInOriginSpaceHeight + alturaRig;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.Camera.transform.position);
        charaController.center = new Vector3(capsuleCenter.x, charaController.height/2 + charaController.skinWidth, capsuleCenter.z);
    }
}
