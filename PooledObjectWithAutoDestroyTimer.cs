using UnityEngine;

namespace BetterPlan.Pool
{
	public class PooledObjectWithAutoDestroyTimer : PooledObjectWithUnityEvents
	{
		[SerializeField] float _lifetime = 2f;

		float _timer = 0;

		public override void Activate()
		{
			_timer = 0;
			base.Activate();
		}

		private void Update()
		{
			_timer += Time.deltaTime;
			if (_timer > _lifetime)
				RequestDestroyThisObject();
		}
	}
}