using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Game
{
    public class Exit : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("You Win! Reached the exit!");
            }
        }
    }
}
