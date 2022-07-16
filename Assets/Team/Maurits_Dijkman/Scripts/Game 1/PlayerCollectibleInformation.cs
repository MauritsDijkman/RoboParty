using UnityEngine;

public class PlayerCollectibleInformation : MonoBehaviour
{
    [System.Serializable]
    public class CollisionInformation
    {
        public string tag = "";
        public int points = 0;
    }

    [Header("Score objects")]
    [SerializeField] public CollisionInformation[] objects;
}
