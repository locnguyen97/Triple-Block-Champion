using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] GameObject particleVFX;

    private bool _dragging;

    private Vector2 _offset;


    [SerializeField]private float min_X = -2.3f;

    [SerializeField]private float max_X = 2.3f;

    [SerializeField]private float min_Y = -4.7f;

    [SerializeField]private float max_Y = 4.7f;

    private void OnMouseDown()
    {
        _offset = GetMousePos() - (Vector2)transform.position;
    }

    private void Update()
    {
        if (transform.position.x < min_X)
        {
            Vector3 moveDirX = new Vector3(min_X, transform.position.y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.x > max_X)
        {
            Vector3 moveDirX = new Vector3(max_X, transform.position.y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.y < min_Y)
        {
            Vector3 moveDirX = new Vector3(transform.position.x, min_Y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.y > max_Y)
        {
            Vector3 moveDirX = new Vector3(transform.position.x, max_Y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.x < min_X && transform.position.y < min_Y)
        {
            Vector3 moveDirX = new Vector3(min_X, min_Y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.x < min_X && transform.position.y > max_Y)
        {
            Vector3 moveDirX = new Vector3(min_X, max_Y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.x > max_X && transform.position.y > max_Y)
        {
            Vector3 moveDirX = new Vector3(max_X, max_Y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.x > max_X && transform.position.y < min_Y)
        {
            Vector3 moveDirX = new Vector3(max_X, min_Y, 0f);
            transform.position = moveDirX;
        }
    }

    private void OnMouseDrag()
    {
        var mousePosition = GetMousePos();

        transform.position = mousePosition - _offset;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == collision.gameObject.tag)
        {
            GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(collision.gameObject);
            GameManager.Instance.CheckLevelUp();
            Destroy(collision.gameObject);
            Destroy(gameObject);
            GameObject explosion = Instantiate(particleVFX, transform.position, transform.rotation);
            Destroy(explosion, .75f);
        }
    }

    private void OnMouseUp()
    {
        
    }

    private Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}