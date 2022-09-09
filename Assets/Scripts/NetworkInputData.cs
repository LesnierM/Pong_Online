using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public struct NetworkInputData : INetworkInput
{
    //Input axies values
    byte _horizontalAxyInput;
    byte _verticalAxyInput;
    /// <summary>
    /// Sets the input.
    /// </summary>
    public void SetInputs(Vector2 direction)
    {
        direction.Normalize();
        _horizontalAxyInput = (byte)direction.x;
        _verticalAxyInput = (byte)direction.y;
    }
    /// <summary>
    /// Gets the input.
    /// </summary>
    public Vector2 Input => new Vector2(getCoprrectValue(_horizontalAxyInput),getCoprrectValue(_verticalAxyInput));
    /// <summary>
    /// Converts byte to float due to bytes only suports from 0 to 255.
    /// </summary>
    float getCoprrectValue(byte value)=> value == 255 ? -1 :value==0?0:1;
}
