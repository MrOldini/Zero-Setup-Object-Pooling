using UnityEngine;
using UnityEngine.Events;

namespace BetterPlan.Pool
{
	public class PooledObjectWithUnityEvents : PooledObject
	{
		[SerializeField] UnityEvent _onActivated;
		[SerializeField] UnityEvent _onPreDeactivate;
		
		public override void Activate()
		{
			base.Activate();
			_onActivated.Invoke();
		}

		public override void Deactivate()
		{
			_onPreDeactivate.Invoke();
			base.Deactivate();
		}
	}
}