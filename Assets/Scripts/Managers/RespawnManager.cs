using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

/// <summary>
/// User to respawn car in predefined points of the track
/// </summary>
public class RespawnManager : MonoBehaviour
{
    [Header("Car")]
    [SerializeField] GameObject player;

    [Header("Respawn points")]
    [SerializeField] List<Transform> respawnPoints;

    [SerializeField] TagField respawnPointsTag;

    // Update is called once per frame
    void Update()
    {
        // Run on press r button
        if (Input.GetKeyDown(KeyCode.R))
        {
            var closestRespawnPoint = GetClosetRespawnToPlayer();
            if (!closestRespawnPoint) return;

            var rb = player.GetComponent<Rigidbody>();
            rb.position = closestRespawnPoint.position;
            rb.rotation = closestRespawnPoint.rotation;
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    /// <summary>
    /// Try to find closest respawn point to the player
    /// </summary>
    /// <returns>Return closest respawn point to the player or null if there no respawn points</returns>
    private Transform GetClosetRespawnToPlayer()
    {
        Transform bestTarget = null;
        var closestDistanceSqr = Mathf.Infinity;
        var currentPosition = player.GetComponent<Transform>().position;
        foreach(var potentialTarget in respawnPoints)
        {
            // Get diff between player and respawn points
            var directionToTarget = potentialTarget.position - currentPosition;
            // Calculate square magnitude, because its cheaper that calculating distance
            var dSqrToTarget = directionToTarget.sqrMagnitude;
            // If this is smallest magnitude - update state variables
            if (!(dSqrToTarget < closestDistanceSqr)) continue;
            closestDistanceSqr = dSqrToTarget;
            bestTarget = potentialTarget;
        }
     
        return bestTarget;
    }
}
