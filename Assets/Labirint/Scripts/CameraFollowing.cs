using UnityEngine;

namespace CameraSettings
{
    public class CameraFollowing : MonoBehaviour
    {
      [SerializeField] [Range(0,1)]private float _time;
      [SerializeField] private Vector3 _offset = new Vector3();
      [SerializeField] private Transform _target;
      public bool IsFollow = true;

      public void Initialize(Transform target)
      {
         _target = target;
         IsFollow = true;
      }
      public void UnInitialize()
      {
         IsFollow = false;
         _target = null;
      }



      private void FixedUpdate()
      {
           
           if(IsFollow == true)
           {
               Vector3 desiredPosition = _target.position + _offset;
               Vector3 smoothedPosition = Vector3.Lerp(transform.position,desiredPosition,_time);
               transform.position = smoothedPosition;
           }
      }
    }
}
