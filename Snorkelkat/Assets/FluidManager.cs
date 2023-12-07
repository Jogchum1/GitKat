using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidManager : MonoBehaviour
{
    public List<GameObject> fluidParticles = new List<GameObject>();
    public float speed;
	public float moveToCenterStrength;//factor by which boid will try toward center Higher it is, higher the turn rate to move to the center
	public float localBoidsDistance;//effective distance to calculate the center
    Vector2 direction;
	public Vector2 positionSum; //calculate sum of position of nearby boids and get count of boid

    // Start is called before the first frame update
    void Start()
    {
		FluidParticle[] particles = GetComponentsInChildren<FluidParticle>();
        foreach (FluidParticle particle in particles)
        {
			fluidParticles.Add(particle.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
		
		MoveToCenter();
    }

	void MoveToCenter()
	{
		int count = 0;
		Debug.Log(count);

		foreach (GameObject particle in fluidParticles)
		{
			particle.transform.Translate(direction * (speed * Time.deltaTime));
			float distance = Vector2.Distance(particle.transform.position, transform.position);
			if (distance <= localBoidsDistance)
			{
				positionSum += (Vector2)particle.transform.position;
				count++;
			}
		}

		if (count == 0)
		{
			return;
		}

		//get average position of boids
		Vector2 positionAverage = positionSum / count;
		positionAverage = positionAverage.normalized;
		Vector2 faceDirection = (positionAverage - (Vector2)transform.position).normalized;

		//move boid toward center
		float deltaTimeStrength = moveToCenterStrength * Time.deltaTime;
		direction = direction + deltaTimeStrength * faceDirection / (deltaTimeStrength + 1);
		direction = direction.normalized;
	}
}
