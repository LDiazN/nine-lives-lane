using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class UseCurvedMaterial : MonoBehaviour
{

    [SerializeField] private Material CurvedMaterial;
    [SerializeField] private Material DefaultMaterial;


    private bool _useCurved = true;

    private MeshRenderer _renderer;

    [SerializeField]
    public bool UseCurved
    {
        get { return _useCurved; }
        set { SetCurved(value); }
    }

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        if (_renderer == null)
        {
            Debug.LogWarning("Mesh Renderer not set for road piece");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // DEBUG: DELETE LATER
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseCurved = !UseCurved;
        }
    }

    private void SetCurved(bool newVal)
    {
        if (newVal == _useCurved)
            return;

        _useCurved = newVal;

        if (_useCurved)
        {
            _renderer.material = CurvedMaterial;
        }
        else
        {
            _renderer.material = DefaultMaterial;
        }
    }
}
