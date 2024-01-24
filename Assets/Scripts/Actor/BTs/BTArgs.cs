using UnityEngine;

public class BTArgs
{
    public int IArg;
    public float FArg;
    public Vector3 VArg;
    public Transform TArg;

    public bool IsNull()
    {
        if (IArg == 0 && FArg == float.NaN && VArg == null && TArg == null)
            return true;

        return false;
    }

    public void Reset()
    {
        IArg = 0;
        FArg = float.NaN;
        VArg = Vector3.zero;
        TArg = null;
    }
}
