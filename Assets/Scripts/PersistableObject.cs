using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistableObject : MonoBehaviour, IPersistableObject
{
    public void Load(GameDataReader reader)
    {
        transform.localPosition = reader.ReadVector3();
    }

    public void Save(GameDataWriter writer)
    {
        writer.Write(transform.localPosition);
    }
}
