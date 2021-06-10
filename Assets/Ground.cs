using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour,IHittable
{
    private MeshRenderer _meshRenderer;
    private Material _defaultMat;
    [SerializeField] private Material _collidedMat;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        _defaultMat = _meshRenderer.material;
    }
    public void Collided()
    {
        Debug.Log(gameObject.name);
        _meshRenderer.material = _collidedMat;


    }

    public void ExitCollided()
    {
        _meshRenderer.material = _defaultMat;
    }

    public void ExitPreCollided()
    {
    }

    public void PreCollided()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
