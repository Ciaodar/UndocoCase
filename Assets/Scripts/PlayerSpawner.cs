using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private GameObject playerPrefab;
    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer) 
            Runner.Spawn(playerPrefab, new Vector3(2.5f, 0, -3.6f), Quaternion.identity, player);
    }
}