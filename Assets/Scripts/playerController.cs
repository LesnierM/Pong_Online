using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class playerController : NetworkBehaviour
{
    NetworkCharacterControllerPrototype _networkCharacterController;
    private void Awake()
    {
        _networkCharacterController=GetComponent<NetworkCharacterControllerPrototype>();
    }
    public override void FixedUpdateNetwork()
    {
        if (GetInput<NetworkInputData>(out NetworkInputData _inputData))
        {
            Debug.Log($"Get input {_inputData.Input}");
        }
    }
}
