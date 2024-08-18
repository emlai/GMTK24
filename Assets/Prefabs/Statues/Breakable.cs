using UnityEngine;

public class ReplaceAndExplode : MonoBehaviour
{
	[SerializeField] private GameObject _replacement;
	[SerializeField] private float _explosionForce = 500f;
	[SerializeField] private float _explosionRadius = 5f;
	[SerializeField] private bool _broken;
	[SerializeField] private float _breakForce = 1;
	[SerializeField] private float _collisionMultiplier = 1;
	// [SerializeField] private float _playerPushForce = 1;

	// public void TriggerExplosion()
	// {
	// 	if (_broken ) return;
	//
	// 	_broken = true;
	//
	// 	GameObject brokenObject = Instantiate(_replacement, transform.position, transform.rotation);
	//
	// 	foreach (Rigidbody rb in brokenObject.GetComponentsInChildren<Rigidbody>())
	// 	{
	// 		rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
	// 	}
	//
	// 	Destroy(gameObject);
	// }

	public void OnTriggerEnter(Collider collider)
	{
		if (_broken) return;
		var player = collider.GetComponent<CharacterController>();
		if (player != null) // Only allow the player to break statues
		{
			var speed = player.velocity.magnitude;
			if (speed >= _breakForce)
			{
				_broken = true;

				// player.Move(-player.velocity * _playerPushForce);
				var replacement = Instantiate(_replacement, transform.position, transform.rotation);

				foreach (var rb in replacement.GetComponentsInChildren<Rigidbody>())
				{
					rb.AddExplosionForce(speed * _collisionMultiplier, transform.position, _explosionRadius);
					// rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
				}

				Destroy(gameObject);
			}
		}
	}
}
