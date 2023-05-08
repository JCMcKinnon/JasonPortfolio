using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateGraph : MonoBehaviour
{
    public List<NPCEdge> Edges;
    public enum NPCStates
    {
        Idle,
        Rest,
        Patrol,
        Attack
    }
    public enum NPCActions
    {
        SmokeCigarette,
        TwiddleThumbs,
        Walk,
        Slap
    }

    public struct NPCEdge
    {
        public NPCStates state;
        public NPCStates destinationState;
        public NPCActions action;
        public int cost;
    }

    public void Start()
    {
        Edges = new List<NPCEdge>()
        {
            new NPCEdge(){state = NPCStates.Idle, destinationState = NPCStates.Rest, action = NPCActions.SmokeCigarette, cost = 1},
            new NPCEdge(){state = NPCStates.Idle, destinationState = NPCStates.Rest, action = NPCActions.TwiddleThumbs, cost = 1},
            new NPCEdge(){state = NPCStates.Idle, destinationState = NPCStates.Patrol, action = NPCActions.Walk, cost = 1},
            new NPCEdge(){state = NPCStates.Idle, destinationState = NPCStates.Attack, action = NPCActions.Slap, cost = 1},
            new NPCEdge(){state = NPCStates.Rest, destinationState = NPCStates.Patrol, action = NPCActions.Walk, cost = 1},
            new NPCEdge(){state = NPCStates.Rest, destinationState = NPCStates.Attack, action = NPCActions.Slap, cost = 1},
            new NPCEdge(){state = NPCStates.Idle, destinationState = NPCStates.Patrol, action = NPCActions.Walk, cost = 1},

        };

    }
}
