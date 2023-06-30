using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField]
    private string _floorType = "Wood";

    private static Dictionary<Terrain, TerrainTypes> _terrainTypes =
        new Dictionary<Terrain, TerrainTypes>();

    private static Dictionary<Terrain, float[,,]> _terrainTextureMaps =
        new Dictionary<Terrain, float[,,]>();

    private Terrain _terrain;

    public float[] _textureValues = new float[8];

    private string[] _floorTypes = new string[8]
    {
        "Dirt",
        "Wood",
        "Stone",
        "Stone",
        "Stone",
        "Stone",
        "Grass",
        "Wood"
    };

    private SoundEmitter _soundEmitter;

    private FMOD.Studio.PARAMETER_ID _floorTypeParameterId;

    private bool _skipNextStep = false;

    private int _posX;

    private int _posZ;

    public static void LoadTerrains()
    {
        //Find all terrains in the scene
        Terrain[] terrains = FindObjectsOfType<Terrain>();
        foreach (Terrain terrain in terrains)
        {
            //Add the terrain to the hashmap
            _terrainTypes.Add(terrain, terrain.GetComponent<TerrainTypes>());
            float[,,] alphaMap = terrain.terrainData.GetAlphamaps(
                0,
                0,
                terrain.terrainData.alphamapWidth,
                terrain.terrainData.alphamapHeight
            );
            _terrainTextureMaps.Add(terrain, alphaMap);
        }
    }

    protected void Awake()
    {
        _soundEmitter = GetComponent<SoundEmitter>();
    }

    protected void Start()
    {
        _floorTypeParameterId = _soundEmitter.GetParameterId("footstep", "Floor Type");
        //_soundEmitter.SetParameterWithLabel("footsteps", _floorTypeParameterId, _floorType, false);
    }

    public void ChangeFloorType(string type)
    {
        _floorType = type;
    }

    // Update is called once per frame
    void Update() { }

    public void PlayFootstep(AnimationEvent animationEvent)
    {
        if (_skipNextStep)
        {
            _skipNextStep = false;
            return;
        }

        float weight = animationEvent.animatorClipInfo.weight;
        if (weight >= 0.48f)
        {
            GetTerrainTexture();
            Play();
            _skipNextStep = weight <= 0.5f && weight >= 0.48f;
        }
    }

    public void Play()
    {
        _soundEmitter.Play("footstep");
    }

    public void GetTerrainTexture()
    {
        _terrain = GetTerrain();
        if (_terrain == null)
        {
            //Default is stone
            _soundEmitter.SetParameterWithLabel("footstep", _floorTypeParameterId, "stone", false);
            return;
        }
        ConvertPositionToMap(transform.position);

        CheckTexture();
    }

    void ConvertPositionToMap(Vector3 position)
    {
        Vector3 terrainPosition = position - _terrain.transform.position;
        Vector3 mapPosition = new Vector3(
            terrainPosition.x / _terrain.terrainData.size.x,
            0,
            terrainPosition.z / _terrain.terrainData.size.z
        );
        float xCoord = mapPosition.x * _terrain.terrainData.alphamapWidth;
        float zCoord = mapPosition.z * _terrain.terrainData.alphamapHeight;
        _posX = (int)xCoord;
        _posZ = (int)zCoord;
    }

    void CheckTexture()
    {
        float[,,] aMap = _terrainTextureMaps[_terrain];

        for (int i = 0; i < _terrainTypes[_terrain].GetCount(); i++)
        {
            if (aMap[_posZ, _posX, i] > 0.5f)
            {
                _floorType = _terrainTypes[_terrain].GetFloorType(i);
                _soundEmitter.SetParameterWithLabel(
                    "footstep",
                    _floorTypeParameterId,
                    _floorType,
                    false
                );
                return;
            }
        }
    }

    Terrain GetTerrain()
    {
        RaycastHit hit;
        if (
            Physics.Raycast(
                transform.position,
                Vector3.down,
                out hit,
                0.5f,
                LayerMask.GetMask("Environment")
            )
        )
        {
            return hit.collider.gameObject.GetComponent<Terrain>();
        }
        return null;
    }
}
