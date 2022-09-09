using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public struct NetworkInputData : INetworkInput
{
    //Input axies values
    sbyte _horizontalAxyInput;
    sbyte _verticalAxyInput;
    /// <summary>
    /// Sets the input.
    /// </summary>
    public void SetInputs(Vector2 direction)
    {
        direction.Normalize();
        _horizontalAxyInput = (sbyte)direction.x;
        _verticalAxyInput = (sbyte)direction.y;
    }
    /// <summary>
    /// Gets the input.
    /// </summary>
    public Vector2 Input => new Vector2(_horizontalAxyInput,_verticalAxyInput);
}
