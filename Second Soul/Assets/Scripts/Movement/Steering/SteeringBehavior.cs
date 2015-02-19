using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteeringAgent))]
public abstract class SteeringBehavior : MonoBehaviour
{
    public float MaxAcceleration;

    public virtual Vector3 Acceleration {
        get
        {
            return Vector3.zero;
        }
    }

    public virtual bool HaltTranslation {
        get
        {
            return false;
        }
    }

    public virtual bool HaltRotation {
        get
        {
            return false;
        }
    }
}
