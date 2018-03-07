using UnityEngine;
using System.Collections;


public class CarController : MonoBehaviour
{
    Rigidbody body;
    float deadZone = 0.1f; //eviter de prendre en compte les petits mouvements
    public float groundedDrag = 3f;
    public float maxVelocity = 50;
    public float hoverForce = 200; //force de lévitation
    public float gravityForce = 1000f; //simulation de la gravité
    public float hoverHeight = 1.2f; //hauteur de lévitation
    public GameObject[] hoverPoints;

    public float forwardAcceleration = 8000f; // force d'Accélération 
    public float reverseAcceleration = 4000f; // force de freinage
    float thrust = 0f;

    public float turnStrength = 1000f; // force de rotation du vehicule
    float turnValue = 0f; //force de rotation appliquée 

    public ParticleSystem[] dustTrails = new ParticleSystem[2]; //particules derrières le vehicule (optionnel mais cool)

    int layerMask; //éviter de prendre en compte le vehicule dans le raycast 

    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.centerOfMass = Vector3.down;

        layerMask = 1 << LayerMask.NameToLayer("Vehicle");
        layerMask = ~layerMask;
    }

    void Update()
    {
        thrust = 0.0f;
        float acceleration = Input.GetAxis("Vertical");
        if (acceleration > deadZone)
            thrust = acceleration * forwardAcceleration;
        else if (acceleration < -deadZone)
            thrust = acceleration * reverseAcceleration;

        // Get turning input
        turnValue = 0.0f;
        float turnAxis = Input.GetAxis("Horizontal");
        if (Mathf.Abs(turnAxis) > deadZone)
            turnValue = turnAxis;
    }

    void FixedUpdate()
    {

        //  Do hover/bounce force
        RaycastHit hit;
        bool onGround = false;
        for (int i = 0; i < hoverPoints.Length; i++)
        {
            var hoverPoint = hoverPoints[i];
            //lancer de rayons pour savoir si le véhicule est au sol 
            if (Physics.Raycast(hoverPoint.transform.position, -Vector3.up, out hit, hoverHeight, layerMask))
            {
                //surélévation du véhicule afin d'éviter les frottement avec le sol
                body.AddForceAtPosition(Vector3.up * hoverForce * (1.0f - (hit.distance / hoverHeight)), hoverPoint.transform.position);
                onGround = true;
            }
            else
            {
                //si le véhicule n'est pas au sol on simule la gravité pour le faire descendre
                //stabilise également le véhicule si il se trouve de travers
                if (transform.position.y > hoverPoint.transform.position.y)
                {
                    body.AddForceAtPosition(hoverPoint.transform.up * gravityForce, hoverPoint.transform.position);
                }
                else
                {
                    body.AddForceAtPosition(hoverPoint.transform.up * -gravityForce, hoverPoint.transform.position);
                }
            }
        }

        //emission d'une trainée si le vehicule est au sol 
        var emissionRate = 0;
        if (onGround)
        {
            body.drag = groundedDrag;
            emissionRate = 10;
        }
        else
        {
            body.drag = 0.1f;
            thrust /= 100f;
            turnValue /= 100f;
        }


        // émettre une trainée derrière le véhicule
        if (dustTrails[0]!=null)
        {
            for (int i = 0; i < dustTrails.Length; i++)
            {
                var emission = dustTrails[i].emission;
                emission.rate = new ParticleSystem.MinMaxCurve(emissionRate);
            }
        }

        // faire avancer/reculer le vehicule
        if (Mathf.Abs(thrust) > 0)
            body.AddForce(transform.forward * thrust);

        // faire tourner le vehicule
        if (turnValue > 0)
        {
            body.AddRelativeTorque(Vector3.up * turnValue * turnStrength);
        }
        else if (turnValue < 0)
        {
            body.AddRelativeTorque(Vector3.up * turnValue * turnStrength);
        }
        
        if (body.velocity.sqrMagnitude > (body.velocity.normalized * maxVelocity).sqrMagnitude)
        {
            body.velocity = body.velocity.normalized * maxVelocity;
        }
    }
}
