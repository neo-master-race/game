using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;


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

    public float vitesse = 0f; // force d'Accélération 
    public float forwardAcceleration = 8000f; // force d'Accélération 
    private float currentAcceleration = 1f;
    public float reverseAcceleration = 4000f; // force de freinage
    float thrust = 0f;

    public float turnStrength = 1000f; // force de rotation du vehicule
    float turnValue = 0f; //force de rotation appliquée 

    public ParticleSystem[] dustTrails = new ParticleSystem[0]; //particules derrières le vehicule (optionnel mais cool)

    //temporaires: boutons pour tester sur Android
    public GameObject buttonForward;
    public GameObject buttonBackward;

    int layerMask; //éviter de prendre en compte le vehicule dans le raycast 

    public float current_speed=8000f;

    public bool isLocalPlayer;
    private int limiter = 0;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.centerOfMass = Vector3.down;

        layerMask = 1 << LayerMask.NameToLayer("Vehicle");
        layerMask = ~layerMask;

        if (Application.platform == RuntimePlatform.Android)
        {
            //Pour que l'orientation de la tablette ne change pas
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
    }

    public void boost(float multiplicator)
    {
        if(currentAcceleration<3f)
        {
            StartCoroutine(accelerationMultiplicator(multiplicator));
            currentAcceleration += multiplicator;
        }
    }

    IEnumerator accelerationMultiplicator(float multiplicator)
    {
        yield return new WaitForSeconds(3);
        currentAcceleration -= multiplicator;
        if (currentAcceleration < 1f)
            currentAcceleration = 1f;
    }

    void Update()
    {
        if (isLocalPlayer) {
            float acceleration = 0.0f;

            if (Application.platform == RuntimePlatform.Android)
            {
                float padRotation = Input.acceleration.x * 2.0f;

                if (buttonForward.GetComponent<CustomButton>().down)
                {
                    acceleration = 1.0f;
                }
                else if (buttonBackward.GetComponent<CustomButton>().down)
                {
                    acceleration = -1.0f;
                }
                /* if (MaxRotation < Mathf.Abs(padRotation))
                 {
                     padRotation = (0.0f < padRotation) ? MaxRotation : -(MaxRotation);
                 }*/
                /*
                 * Zone morte. Si la rotation est inférieure à MinRotation, elle est nulle. 
                 */
                float MinRotation = 0.1f;
                if (Mathf.Abs(padRotation) < MinRotation)
                {
                    padRotation = (0.0f < padRotation) ? MinRotation : -MinRotation;
                }
                padRotation += (0.0f < padRotation) ? -MinRotation : MinRotation;
                turnValue = padRotation;
            }
            else
            {
                acceleration = Input.GetAxis("Vertical");
                // Get turning input
                turnValue = 0.0f;
                float turnAxis = Input.GetAxis("Horizontal");
                if (Mathf.Abs(turnAxis) > deadZone)
                    turnValue = turnAxis;
            }

            


            thrust = 0.0f;
            if (acceleration > deadZone)
                thrust = acceleration * forwardAcceleration * currentAcceleration;
            else if (acceleration < -deadZone)
            {
                thrust = acceleration * reverseAcceleration * currentAcceleration;
                turnValue *= -1f;
            }
            vitesse = thrust;// + Mathf.Abs(turnValue * turnStrength);


            // limit the message per second rate
            limiter += 1;
            limiter = limiter % 6;
            // if the player moved, send his new position
            if (isLocalPlayer && limiter == 0 && vitesse != 0)
            {
                updatePlayerPosition();
            }
        }
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
                body.AddForceAtPosition(Vector3.up * hoverForce * (0.9f - (hit.distance / hoverHeight)), hoverPoint.transform.position);
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
        if (dustTrails.Length>0)
        {
            for (int i = 0; i < dustTrails.Length; i++)
            {
                var emission = dustTrails[i].emission;
    
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

    private void updatePlayerPosition()
    {
        // vectors that we need to send
        Protocol.Vector vecPosition = new Protocol.Vector{
            X = transform.position.x,
            Y = transform.position.y,
            Z = transform.position.z
        };
        Protocol.Vector vecRotation = new Protocol.Vector{
            X = transform.eulerAngles.x,
            Y = transform.eulerAngles.y,
            Z = transform.eulerAngles.z
        };
        Protocol.Vector vecScale = new Protocol.Vector{
            X = transform.localScale.x,
            Y = transform.localScale.y,
            Z = transform.localScale.z
        };

        GameObject
            .Find("Network")
            .GetComponent<Network>()
            .updatePlayerPosition(
                vecPosition,
                vecRotation,
                vecScale
            );
     }
}
