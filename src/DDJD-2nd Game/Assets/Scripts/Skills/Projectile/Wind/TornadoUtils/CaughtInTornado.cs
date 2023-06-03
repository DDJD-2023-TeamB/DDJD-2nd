using UnityEngine;

public class CaughtInTornado : MonoBehaviour
{
    private Tornado tornadoReference;
    private SpringJoint spring;

    [HideInInspector]
    public Rigidbody rigid;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Lift spring so objects are pulled upwards
        Vector3 newPosition = spring.connectedAnchor;
        newPosition.y = transform.position.y;
        spring.connectedAnchor = newPosition;
    }

    void FixedUpdate()
    {
        if (!enabled)
        {
            //Remove this component
            Destroy(this);
            return;
        }
        //Rotate object around tornado center
        Vector3 direction = transform.position - tornadoReference.transform.position;
        //Project
        Vector3 projection = Vector3.ProjectOnPlane(direction, tornadoReference.RotationAxis);
        projection.Normalize();
        Vector3 normal = Quaternion.AngleAxis(130, tornadoReference.RotationAxis) * projection;
        normal = Quaternion.AngleAxis(tornadoReference.Lift, projection) * normal;
        rigid.AddForce(normal * tornadoReference.TornadoStrength, ForceMode.Force);
    }

    //Call this when tornadoReference already exists
    public void Init(Tornado tornadoRef, Rigidbody tornadoRigidbody, float springForce)
    {
        //Make sure this is enabled (for reentrance)
        enabled = true;

        //Save tornado reference
        tornadoReference = tornadoRef;

        //Initialize the spring
        spring = gameObject.AddComponent<SpringJoint>();
        spring.spring = springForce;
        spring.connectedBody = tornadoRigidbody;

        spring.autoConfigureConnectedAnchor = false;

        //Set initial position of the caught object relative to its position and the tornado
        Vector3 initialPosition = Vector3.zero;
        initialPosition.y = transform.position.y;
        spring.connectedAnchor = initialPosition;
    }

    public void Release()
    {
        enabled = false;
        Destroy(spring);
    }
}
