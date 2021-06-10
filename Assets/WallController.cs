using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WallController : MonoBehaviour
{
    private List<Wall> _wallList = new List<Wall>();
    [SerializeField] private List<float> _waitTime;
    private List<Vector2> _positionList = new List<Vector2>();

    // Start is called before the first frame update
    void Awake()
    {


        SufflePositionChilds();

    }

    public void ChildCallMe()
    {
        foreach (var wall in _wallList)
        {
            wall.LemmeFadeOut();
        }
    }

    private void SufflePositionChilds()
    {
        foreach (var wall in GetComponentsInChildren<Wall>())
        {
            wall.wallControllerDaddy = this;
            _wallList.Add(wall);
        }
        foreach (var wall in _wallList)
        {

            _positionList.Add(new Vector2(wall.transform.position.x, wall.transform.position.z));

        }

        RandomList.Shuffle<Vector2>(_positionList);
        var stack = new Stack<Vector2>(_positionList);

        RandomList.Shuffle<float>(_waitTime);
        var _waitTimeStack = new Stack<float>(_waitTime);

        foreach (var wall in _wallList)
        {
            var pos = stack.Pop();
            wall.transform.position = new Vector3(pos.x, wall.transform.position.y, pos.y);
            wall.SetWaitTime(_waitTimeStack.Pop());
        }
        stack.Clear();
        _waitTimeStack.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
