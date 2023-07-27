using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(JoystickPlayerExample))]
public class Player : Character
{
   public float MoveSpeed => _moveSpeed;

   [Inject]
   private void Construct(GameLevelController gameLevelController)
   {
      Debug.Log("Construct Player");
      _gameLevelController = gameLevelController;
   }
   
   private void OnTriggerEnter(Collider collider)
   {
      if (collider.CompareTag("ExitDoor"))
      {
         _gameLevelController.PlayerWin();
      }
   }

   protected override void Death()
   {
      base.Death();
      _gameLevelController.PlayerDeath();
   }
}
