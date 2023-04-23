using UnityEngine;

namespace SaveSystem
{
    public class ICanSave<T>
    {
        public string ToSaveGame()
        {
            return JsonUtility.ToJson(this);
        }

        public T FromSaveGame(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }
    }
}
