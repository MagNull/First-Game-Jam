using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LinkedSwapper))]
public class Linker : MonoBehaviour, ILinkable
{
    [SerializeField] private float _linkSpeed = 1;
    [SerializeField] private float _planeHeight = 0.7f;
    [SerializeField] private float _linkDistanceToStop = 0.1f;
    [SerializeField] private GameState _gameState;
    private LineRenderer _lineRenderer;

    private Transform _linkingTransform;
    private LinkedSwapper _linkedSwapper;
    
    private bool _isLinking;
    private bool _isLinked;

    private Plane _plane;
    private Camera _camera;


    private void Awake()
    {
        _lineRenderer = GetComponentInChildren<LineRenderer>();
        _linkedSwapper = GetComponent<LinkedSwapper>();
    }

    void Start()
    {
        _lineRenderer.SetPosition(1,transform.position);
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(2, transform.position);
        _lineRenderer.gameObject.SetActive(false);
        _camera = Camera.main;
        _plane = new Plane(Vector3.up, Vector3.up * _planeHeight);
    }

    
    void Update()
    {
        ShootLinker();
        if (_linkingTransform)
        {
            _lineRenderer.SetPosition(0, _linkingTransform.position);
            CheckLink();
        }
    }

    private void ShootLinker()
    {
        _lineRenderer.SetPosition(1, transform.position);
        if(_isLinked || _isLinking) _lineRenderer.SetPosition(2, transform.position);
        if (Input.GetMouseButtonDown(0) && !_isLinking)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (_plane.Raycast(ray, out float enter))
            {
                StartCoroutine(Linking(ray.GetPoint(enter)));
            }
        }
    }

    private IEnumerator Linking(Vector3 point)
    {
        _isLinking = true;
        _linkedSwapper.IsLinking = _isLinking;
        _lineRenderer.gameObject.SetActive(true);
        
        int endIndex = _isLinked ? 2 : 0;
        _lineRenderer.SetPosition(endIndex,transform.position);
        Vector3 lineEndPosition = _lineRenderer.GetPosition(endIndex);
        
        while ((lineEndPosition - point).sqrMagnitude > _linkDistanceToStop * _linkDistanceToStop && _isLinking)
        {
            lineEndPosition = Vector3.Lerp(lineEndPosition, point, Time.deltaTime * _linkSpeed);
            _lineRenderer.SetPosition(endIndex, lineEndPosition);
            CheckLinkingRay(lineEndPosition);
            yield return null;
        }
        
        _lineRenderer.SetPosition(endIndex, transform.position);
        if (!_isLinked)
        {
            _lineRenderer.gameObject.SetActive(false);
        }
        else
        {
            _gameState.StartGame();
        }
        _isLinking = false;
        _linkedSwapper.IsLinking = _isLinking;
    }

    private void CheckLinkingRay(Vector3 point)
    {
        Ray ray = new Ray(transform.position, (point - transform.position));
        if (Physics.Raycast(ray, out RaycastHit hit, (point - transform.position).magnitude))
        {
            if (hit.collider.TryGetComponent(out ILinkable linkable))
            {
                if (_linkingTransform && _linkingTransform != hit.transform)
                {
                    _linkingTransform.GetComponent<ILinkable>().Unlink();
                }

                Link(hit.rigidbody);
                linkable.Link(GetComponent<Rigidbody>());
                _isLinked = true;
                _isLinking = false;
            }
        }
    }

    private void CheckLink()
    {
        Ray ray = new Ray(transform.position, (_linkingTransform.position - transform.position));
        if (!_isLinking &&
            Physics.Raycast(ray, out RaycastHit hit, (_linkingTransform.position - transform.position).magnitude, 3))
        {
            _gameState.Lose();
        }
    }

    public void Link(Rigidbody rigidbody)
    {
        _linkingTransform = rigidbody.transform;
        _linkedSwapper.LinkedTransform = _linkingTransform;
    }

    public void Unlink()
    {
        _linkingTransform = null;
        _linkedSwapper.LinkedTransform = _linkingTransform;
    }
}
