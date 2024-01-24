using UnityEngine;

public class BTArgs
{
    public bool IsNull = true;
    public int IArg = 0;
    public float FArg = float.NaN;
    public Vector3 VArg = Vector3.zero;
    public Transform TArg = null;

    public void Reset()
    {
        IsNull = true;
        IArg = 0;
        FArg = float.NaN;
        VArg = Vector3.zero;
        TArg = null;
    }
}
