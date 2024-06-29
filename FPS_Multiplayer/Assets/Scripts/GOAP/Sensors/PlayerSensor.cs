using System;
using System.Collections.Generic;
using Entities.Entity;
using Services;
using Services.DependencyInjection;
using UnityEngine;

namespace GOAP.Sensors
{
    public interface IPlayerSensor
    {
        Observer<string, bool> IsUserInRange { get; }
        void UpdatePlayerList(List<GamePlayer> newPlayers);
    }
    
    [RequireComponent(typeof(SphereCollider))]
    public class PlayerSensor : MonoBehaviour, IPlayerSensor, IDependencyProvider
{
    [SerializeField] private string key;
    
    public new SphereCollider collider;
    public delegate void PlayerEnterEvent(Transform player);
    public delegate void PlayerExitEvent(Vector3 lastKnownPosition);
    public event PlayerEnterEvent OnPlayerEnter;
    public event PlayerExitEvent OnPlayerExit;
    
    public Observer<string, bool> IsUserInRange { get; private set; }

    private readonly HashSet<GamePlayer> playersInRange = new();

    [Provide]
    public IPlayerSensor ProviderSensor()
    {
        return this;
    }

    public void SetKey(string sensorKey) => key = sensorKey;
    
    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
        IsUserInRange = new Observer<string, bool>(key, false);
    }

    public void UpdatePlayerList(List<GamePlayer> newPlayers)
    {
        foreach (var player in newPlayers)
        {
            CheckPlayerInRange(player);
        }
        
        playersInRange.RemoveWhere(p => !newPlayers.Contains(p));
        
        UpdateIsUserInRange();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GamePlayer player))
        {
            playersInRange.Add(player);
            OnPlayerEnter?.Invoke(player.transform);
            UpdateIsUserInRange();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out GamePlayer player))
        {
            playersInRange.Remove(player);
            OnPlayerExit?.Invoke(other.transform.position);
            UpdateIsUserInRange();
        }
    }

    private void CheckPlayerInRange(GamePlayer player)
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= collider.radius)
        {
            if (playersInRange.Add(player))
            {
                OnPlayerEnter?.Invoke(player.transform);
            }
        }
        else
        {
            if (playersInRange.Remove(player))
            {
                OnPlayerExit?.Invoke(player.transform.position);
            }
        }
    }

    private void UpdateIsUserInRange()
    {
        var isAnyPlayerInRange = playersInRange.Count > 0;
        if (isAnyPlayerInRange != IsUserInRange.Value2)
        {
            IsUserInRange.Value1 = key;
            IsUserInRange.Value2 = isAnyPlayerInRange;
            IsUserInRange.Invoke();
        }
    }
}
}
