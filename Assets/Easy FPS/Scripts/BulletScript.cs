using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	[Tooltip("Furthest distance bullet will look for target")]
	public float maxDistance = 1000000;
	RaycastHit hit;

	[Tooltip("Prefab of wall damange hit. The object needs 'LevelPart' tag to create decal on it.")]
	public GameObject decalHitWall;

	[Tooltip("Decal will need to be slightly in front of the wall so it doesnt cause rendering problems")]
	public float floatInfrontOfWall;

	[Tooltip("Blood prefab particle this bullet will create upon hitting enemy")]
	public GameObject bloodEffect;

	[Tooltip("Put Weapon layer and Player layer to ignore bullet raycast.")]
	public LayerMask ignoreLayer;

	[Tooltip("How much damage the bullet does")]
	public int damage = 10;

	void Update () {

		if(Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, ~ignoreLayer))
		{
			if(decalHitWall)
			{
				if(hit.transform.tag == "LevelPart")
				{
					Instantiate(decalHitWall, hit.point + hit.normal * floatInfrontOfWall, Quaternion.LookRotation(hit.normal));
					Destroy(gameObject);
				}

				if(hit.transform.tag == "Dummie")
				{
					// Blood effect
					Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));

					// DAMAGE SYSTEM ADDED
					Enemy enemy = hit.transform.GetComponent<Enemy>();
					if(enemy != null)
					{
						enemy.TakeDamage(damage);
					}

					Destroy(gameObject);
				}
			}

			Destroy(gameObject);
		}

		Destroy(gameObject, 0.1f);
	}
}