using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    CharacterController character_controller;

    public GameObject body;
    public float _moveSpeed = 6.0f;
    public float _rotateSpeed = 2.0f;
    private Vector3 _moveDirection = Vector3.zero;

    void Start() {
        character_controller = GetComponent<CharacterController>();
    }

    void Update() {
        _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        _moveDirection *= _moveSpeed;

        body.GetComponent<Animator>().SetBool("walking", _moveDirection.magnitude > 0.0f);

        _moveDirection += Physics.gravity;
        character_controller.Move(_moveDirection * Time.deltaTime);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Vector3 point = hit.point;
            point.y = transform.position.y;
            Vector3 targetDir = point - transform.position;

            float step = _rotateSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(body.transform.forward, targetDir, step, 0.0f);

            body.transform.rotation = Quaternion.LookRotation(newDir);
        }
    }
}
