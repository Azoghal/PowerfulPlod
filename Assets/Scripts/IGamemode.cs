using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public interface IGamemode
    {
        void handlePlayerJoined(GameObject player);

        void handlePlayerDeath(V2PlayerManager playerManager);
    }
}
