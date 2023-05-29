using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoRuntime : MonoBehaviour, IPersistableObject
{
    [SerializeField]
    public PersistableObject persistableObjectPrefab;
    [SerializeField]
    public int totalObject;

    private PersistentStorage _persistentStorage;
    private List<PersistableObject> _objectList;

    private void Awake()
    {
        _persistentStorage = new PersistentStorage(Application.persistentDataPath);
        _objectList = new List<PersistableObject>();
    }

    public void NewGame()
    {
        foreach (PersistableObject obj in _objectList)
        {
            Destroy(obj.gameObject);
        }
        _objectList.Clear();

        for (int i = 0; i < totalObject; i++)
        {
            PersistableObject obj = Instantiate<PersistableObject>(persistableObjectPrefab);
            obj.transform.localPosition = Random.insideUnitCircle * 5;
            _objectList.Add(obj);
        }
    }

    public void SaveGame()
    {
        _persistentStorage.Save(this);
    }

    public void LoadGame()
    {
        foreach (PersistableObject obj in _objectList)
        {
            Destroy(obj.gameObject);
        }
        _objectList.Clear();

        _persistentStorage.Load(this);
    }

    void IPersistableObject.Save(GameDataWriter writer)
    {
        writer.Write(_objectList.Count);
        if (_objectList.Count > 0)
        {
            foreach (PersistableObject obj in _objectList)
            {
                obj.Save(writer);
            }
        }
    }

    void IPersistableObject.Load(GameDataReader reader)
    {
        int objectCount = reader.ReadInt();
        if (objectCount > 0)
        {
            for (int i = 0; i < objectCount; i++)
            {
                PersistableObject obj = Instantiate<PersistableObject>(persistableObjectPrefab);
                obj.Load(reader);
                _objectList.Add(obj);
            }
        }
    }
}
