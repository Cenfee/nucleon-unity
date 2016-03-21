using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using PureMVC.Patterns;

namespace game.scene
{
    class GameScene : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            if (Application.loadedLevel == 0)
                game.ApplicationFacade.getInstance().startup();

            Facade.Instance.SendNotification(game.view.gameplay.GameMediator.NAME + "Show", null);   
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnDestroy()
        {
            
        }


    }
}
