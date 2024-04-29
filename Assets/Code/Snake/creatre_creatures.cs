using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatre_creatures : MonoBehaviour
{
    public Transform parentSegmentsTo = null;
    public bool makeRotator = false;


    public float partOffset = 0.0f;

    [Range(0.0f, Mathf.PI * 2.0f)]
    public float theta = 0.1f;

    [Range(0.0f, 10.0f)]
    public float frequency = 1.0f;


    [Range(1, 1000)]
    public int numParts = 5;

    [Range(-1000.0f, 1000.0f)]
    public float gap = 1;

    public Color color = Color.blue;
    public bool assignColors = true;

    [Range(1.0f, 5000.0f)]
    public float verticalSize = 1.0f;

    public bool flatten = false;

    public GameObject headPrefab;
    public GameObject bodyPrefab;
    public GameObject tailPrefab;

    public GameObject seatPrefab;



    public float lengthVariation = 0;

    Dictionary<string, GameObject> bodyParts = new Dictionary<string, GameObject>();

    GameObject GetCreaturePart(string key, GameObject prefab)
    {
        if (bodyParts.ContainsKey(key))
        {
            return bodyParts[key];
        }
        else
        {
            GameObject part = GameObject.Instantiate<GameObject>(prefab);

            bodyParts[key] = part;
            return part;
        }
    }

    public void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            List<CreaturePart> creatureParts = CreateCreatureParams();
            Gizmos.color = Color.yellow;
            foreach (CreaturePart cp in creatureParts)
            {
                Gizmos.DrawWireSphere(cp.position, cp.size * 0.5f);



            }
            LogParts(creatureParts);
        }
    }

    void LogParts(List<CreaturePart> creatureParts)
    {
        string cps = "";
        foreach (CreaturePart cp in creatureParts)
        {
            cps += cp;
        }
    }

    public void CreateCreature()
    {
        List<CreaturePart> creatureParts = CreateCreatureParams();

        // Iterate through each creature part
        for (int i = 0; i < creatureParts.Count; i++)
        {
            CreaturePart cp = creatureParts[i];

            // Get the corresponding GameObject for this part
            GameObject part = GetCreaturePart("body part " + i, cp.prefab);
            part.transform.position = cp.position;

            // Apply size and offset to the part
            if (i != 0)
            {
                part.transform.Translate(0, 0, partOffset);
            }

            // Set parent for hierarchical structure
            if (i == 0)
            {
                // This is the head part
                part.transform.parent = transform;

                // Apply size to the head part
                part.transform.localScale = new Vector3(cp.size, cp.size, cp.size);

                // Ensure the head's scale is always (1, 1, 1)
                part.transform.localScale = Vector3.one;
            }
            else
            {
                // Other body parts
                if (parentSegmentsTo != null)
                {
                    part.transform.parent = parentSegmentsTo;
                }
                else
                {
                    part.transform.parent = transform;
                }
            }

            // Apply rotation to the part
            part.transform.rotation = cp.rotation;
        }
    }


    public int seatPosition = 5;


    List<CreaturePart> CreateCreatureParams()
    {
        List<CreaturePart> cps = new List<CreaturePart>();
        float thetaInc = (Mathf.PI * frequency) / (numParts);
        float theta = this.theta;
        float lastPartSize = 0;
        Vector3 pos = transform.position;

        int half = (numParts / 2) - 1;

        for (int i = 0; i < numParts; i++)
        {

            float partSize = 0;
            if (makeRotator && i == 0)
            {
                partSize = 0.03f;
            }
            else
            {
                partSize = verticalSize * Mathf.Abs(Mathf.Sin(theta));
                theta += thetaInc;
            }
            pos -= ((((lastPartSize + partSize) / 2.0f) + gap) * transform.forward);
            if (flatten)
            {
                pos.y -= (partSize / 2);
            }
            lastPartSize = partSize;
            if (i == seatPosition && seatPrefab != null)
            {
                cps.Add(new CreaturePart(pos
                    , partSize
                    , CreaturePart.Part.seat
                    , seatPrefab
                    , Quaternion.identity));
            }
            else
            {
                cps.Add(new CreaturePart(pos
                    , partSize
                    , (i == 0) ? CreaturePart.Part.head : (i < numParts - 1) ? CreaturePart.Part.body : CreaturePart.Part.tail
                    , (i == 0) ? headPrefab : (i < numParts - 1) ? bodyPrefab : (tailPrefab != null) ? tailPrefab : bodyPrefab
                    , Quaternion.identity));

            }

        }
        return cps;
    }

    // Use this for initialization
    void Awake()
    {

        if (transform.childCount == 0)
        {
            CreateCreature();
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // CreateCreature();
        }
    }

}

struct CreaturePart
{
    public Vector3 position;
    public Quaternion rotation;
    public float size;
    public enum Part { head, body, tail, tenticle, seat };
    public Part part;
    public GameObject prefab;

    public CreaturePart(Vector3 position, float scale, Part part, GameObject prefab, Quaternion rotation)
    {
        this.position = position;
        this.size = scale;
        this.part = part;
        this.prefab = prefab;
        this.rotation = rotation;
    }

    public override string ToString()
    {
        return position + ", " + size + ", " + rotation;
    }
}
