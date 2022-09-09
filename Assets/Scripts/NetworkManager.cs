using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] NetworkPrefabRef _playerprefab;
    Dictionary<PlayerRef, NetworkObject> _playersSpawned = new Dictionary<PlayerRef, NetworkObject>();
    NetworkRunner _networkRunner;

    #region Mono
    private void OnGUI()
    {
        if (_networkRunner == null)
        {
            if (GUI.Button(new Rect(0, 0, 200, 40), "Host"))
            {
                StartGame(GameMode.Host);
            }
            if (GUI.Button(new Rect(0, 40, 200, 40), "Join"))
            {
                StartGame(GameMode.Client);
            }
        }
    }
    #endregion

    #region Methods
    async void StartGame(GameMode mode)
    {
        // Create the Fusion runner and let it know that we will be providing user input
        _networkRunner = gameObject.AddComponent<NetworkRunner>();
        _networkRunner.ProvideInput = true;

        // Start or join (depends on gamemode) a session with a specific name
        await _networkRunner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "TestRoom",
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }
    #endregion  

    #region Callbacks
    public void OnConnectedToServer(NetworkRunner runner)
    {
    }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }
    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
    }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }
    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        Vector2 _input = Vector2.zero;
        NetworkInputData _inputData = new NetworkInputData();
        if (Input.GetKey(KeyCode.W))
            _input.y = 1;
        if (Input.GetKey(KeyCode.S))
            _input.y = -1;
        if (Input.GetKey(KeyCode.A))
            _input.x = -1;
        if (Input.GetKey(KeyCode.D))
            _input.x = 1;
        _inputData.SetInputs(_input);
        input.Set<NetworkInputData>(_inputData);
        Debug.Log("Send input");
    }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (!runner.IsServer)
            return;
        var _networkObject = runner.Spawn(_playerprefab, Vector3.zero, Quaternion.identity, player);
        _playersSpawned.Add(player, _networkObject);
    }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if(_playersSpawned.TryGetValue(player,out NetworkObject playerObejct))
        {
            runner.Despawn(playerObejct);
            _playersSpawned.Remove(player);
        }
    }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
    }
    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }
    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }
    #endregion
}
