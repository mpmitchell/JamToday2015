﻿using UnityEngine;
using System.Collections.Generic;

public class Control : MonoBehaviour {
    private Dictionary<GameObject, PatternNode> nodes_ = new Dictionary<GameObject, PatternNode>();
    private float timer = 0.0f;

    private void Start() {
        foreach (Transform child in transform) {
            nodes_.Add(child.gameObject, child.GetComponent<PatternNode>());
        }
    }

    private void Update() {
        if (GameController.instance.currentPattern_ == null) {
            return;
        }

        if ((timer += Time.deltaTime) > GameController.instance.maxTimePerPattern_) {
            GameController.instance.RegisterAttempt();
            foreach (var node in nodes_) {
                node.Value.isActive = true;
            }
            timer = 0.0f;
        }

        if (Input.GetButton("Fire1")) {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("PatternNodes"))) {
                if (nodes_[hit.collider.gameObject].isActive) {
                    nodes_[hit.collider.gameObject].isActive = false;

                    if (GameController.instance.currentPattern_.playerAttempt.Count > 0) {
                        nodes_[hit.collider.gameObject].ConnectTo(GameController.instance.currentPattern_.playerAttempt.Last.Value);
                    }

                    GameController.instance.currentPattern_.playerAttempt.AddLast(nodes_[hit.collider.gameObject]);
                }
            }
        } else if (Input.GetButtonUp("Fire1")) {
            if (GameController.instance.currentPattern_.playerAttempt.Count > 1) {
                timer = 0.0f;
                GameController.instance.RegisterAttempt();
                foreach (var node in nodes_) {
                    node.Value.isActive = true;
                }
            } else {
                GameController.instance.currentPattern_.playerAttempt.Clear();
            }
        }
    }
}
