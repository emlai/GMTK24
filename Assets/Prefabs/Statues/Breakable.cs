using UnityEngine;

public class ReplaceAndExplode : MonoBehaviour
{
	[SerializeField] private GameObject _replacement;
	[SerializeField] private float _explosionForce = 500f;
	[SerializeField] private float _explosionRadius = 5f;
	[SerializeField] private bool _broken;  

	public void TriggerExplosion()
	{
		if (_broken ) return;

		_broken = true;

		GameObject brokenObject = Instantiate(_replacement, transform.position, transform.rotation);

		foreach (Rigidbody rb in brokenObject.GetComponentsInChildren<Rigidbody>())
		{
			rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
		}

		Destroy(gameObject);
	}

	//public void OnCollision(Collision collision)
	//{
	//	if (_broken) return;
	//	if (collision.relativeVelocity.magnitude >= _breakForce)
	//	{
	//		_broken = true;

	//		var replacement = Instantiate(_replacement, transform.position, transform.rotation);

	//		foreach (Rigidbody rb in replacement.GetComponentsInChildren<Rigidbody>())
	//		{
	//			rb.AddExplosionForce(collision.relativeVelocity.magnitude * _collisionMultiplier, collision.contacts[0].point, _explosionRadius);
	//		}

	//		Destroy(gameObject);
	//	}
	//}
}