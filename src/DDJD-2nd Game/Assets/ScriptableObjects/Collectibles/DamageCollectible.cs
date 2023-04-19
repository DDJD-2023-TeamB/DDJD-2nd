// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [CreateAssetMenu(menuName = "Scriptable Objects/Collectibles/Damage")]
// public class Damage : Collectible
// {
//     [SerializeField] private float speedMultiplier;

//     public override void Apply(GameObject target) {
//         PlayerMovement movementController = target.GetComponent<PlayerMovement>();
//         movementController.velocity *= speedMultiplier;
//     }

//     public override void Remove(GameObject target) {
//         PlayerMovement movementController = target.GetComponent<PlayerMovement>();
//         movementController.velocity /= speedMultiplier;
//     }
// }
