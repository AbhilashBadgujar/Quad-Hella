using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GM : MonoBehaviour
{

    public static GM gm;

    public MatchSettings matchSettings;

    private void Awake()
    {
        if (gm != null)
        {
            print("More than one GM");
        }
        else
        {
            gm = this;
        }
    }

    #region ///player tracking

    const string PLAYER_ID_PREFIX = "Player ";
    public static Dictionary<string, Player> players = new Dictionary<string, Player>();


    public static void RegisterPlayer(string netID, Player player)
    {
        string playerID = PLAYER_ID_PREFIX + netID;
        players.Add(playerID, player);
        player.transform.name = playerID;
    }

    public static void UnRegisterPlayer(string playerID)
    {
        //players.Remove(playerID);
    }

    public static Player GetPlayerID(string playerID)
    {
        if (!players.ContainsKey(playerID))
        {
            Debug.Log("hey! this played id is no good! " + playerID);
            return null;
        }

        return players[playerID];
    }

    //private void OnGUI()
    //{
    //    GUILayout.BeginArea(new Rect(200, 200, 200, 500));
    //    GUILayout.BeginVertical();

    //    foreach (string playerID in players.Keys)
    //    {
    //        GUILayout.Label(playerID + " - " + players[playerID].transform.name);
    //    }

    //    GUILayout.EndVertical();
    //    GUILayout.EndArea();

    //}

#endregion

    
}