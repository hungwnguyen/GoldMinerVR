using Colyseus.Schema;
using Colyseus;
using System.Collections.Generic;

[System.Serializable]
public class ExampleNetworkedEntity : ColyseusNetworkedEntity
{
    //public string updateHash;

    [Type(0, "string")]
    public string id = default(string);

    [Type(1, "string")]
    public string ownerId = default(string);

    [Type(2, "string")]
    public string creationId = default(string);

    [Type(3, "number")]
    public double xPos = default(double);

    [Type(4, "number")]
    public double yPos = default(double);

    [Type(5, "number")]
    public double zPos = default(double);

    [Type(6, "number")]
    public float xRot = default(float);

    [Type(7, "number")]
    public float yRot = default(float);

    [Type(8, "number")]
    public float zRot = default(float);

    [Type(9, "number")]
    public float wRot = default(float);

    [Type(10, "number")]
    public double a1 = default(double);

    [Type(11, "boolean")]
    public bool a2 = default(bool);

    [Type(12, "number")]
    public double a3 = default(double);

    [Type(13, "boolean")]
    public bool a4 = default(bool);

    [Type(14, "map", typeof(MapSchema<string>), "string")]
    public MapSchema<string> attributes = new MapSchema<string>();

    [Type(15, "number")]
    public double timestamp = default(double);


    // Make sure to update Clone fi you add any attributes
    public ExampleNetworkedEntity Clone()
    {
        return new ExampleNetworkedEntity()
        {
            id = id,
            ownerId = ownerId,
            creationId = creationId,
            xPos = xPos,
            yPos = yPos,
            zPos = zPos,
            xRot = xRot,
            yRot = yRot,
            zRot = zRot,
            wRot = wRot,
            attributes = attributes,
            a1 = a1,
            a2 = a2,
            a3 = a3,
            a4 = a4,
            timestamp = timestamp
        };
    }

}

[System.Serializable]
class EntityCreationMessage
{
    public string creationId;
    public Dictionary<string, object> attributes;
}

