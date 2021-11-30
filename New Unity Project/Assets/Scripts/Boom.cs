using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private float _force = 400f;

    private float _radius = 4f;

    private bool _isDestroy = false;

    public GameObject Replace;

    private Transform _thisTransform;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _thisTransform = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        GameManager.Instance.Contain.Add(gameObject, this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(_rigidbody.velocity.y) > 5 && _isDestroy == false)
        {
            gameObject.SetActive(false);
            GameManager.Instance.CountDestroy--;
            GameObject obj = Instantiate(GameManager.Instance.Contain[gameObject].Replace, _thisTransform.position, _thisTransform.rotation);
            ObjectPooler.InstancePool.SpawnFromPool(_thisTransform.position, Quaternion.identity);
            obj.transform.Rotate(90, 0, 0);
            foreach (Transform child in obj.GetComponentsInChildren<Transform>())
            {
                child.GetComponent<Rigidbody>().velocity = _rigidbody.velocity;
            }
            _isDestroy = true;
        }

    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.Contain.ContainsKey(gameObject) && GameManager.Instance._countBoom > 0)
        {
            GameManager.Instance._countBoom--;
            gameObject.SetActive(false);
            GameManager.Instance.CountDestroy--;
            GameObject obj = Instantiate(GameManager.Instance.Contain[gameObject].Replace, _thisTransform.position, Quaternion.identity);
            ObjectPooler.InstancePool.SpawnFromPool(_thisTransform.position, Quaternion.identity);
            _isDestroy = true;

            Collider[] collider = Physics.OverlapSphere(obj.transform.position, _radius);

            foreach (Collider col in collider)
            {
                Rigidbody _rb = col.attachedRigidbody;
                if (_rb)
                {
                    _rb.AddExplosionForce(_force, obj.transform.position, _radius);
                }
            }
        }
    }


}
