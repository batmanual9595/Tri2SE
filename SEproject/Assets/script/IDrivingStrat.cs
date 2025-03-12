using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDrivingStrat
{
    public virtual void swerve() { }
    public virtual void drive() { }
    public virtual void brake() { }
}
