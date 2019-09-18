using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanoramicZoom : MonoBehaviour
{
    // Armazernar a posição do toque na tela.
    private Vector3 touchStart;

    // O minimo de zoom.
    [Range(0f, 10f)]
    public float zoomMinimum = 1f;

    // O máximo de zoom.
    [Range(1f, 170f)]
    public float zoomMaximum = 10f;

    // Sensibilidade do zoom.
    [Range(0f, 10f)]
    public float zoomSensitivity = 1f;

    void Start() { }

    void Update()
    {
        // SE houver apenas 2 dedos sobre a tela...
        if (Input.touchCount == 2)
        {
            Debug.Log("TWO Fingers!");
            // Pegar o primeiro dedo que tocou na tela.
            Touch touchOne = Input.GetTouch(0);

            // Pegar o segundo dedo que tocou na tela.
            Touch touchTwo = Input.GetTouch(1);

            // A posição absoluta do primeiro toque.
            Vector2 touchOnePreviousPos = 
                touchOne.position - touchOne.deltaPosition;

            // A posição absoluta do segundo toque.
            Vector2 touchTwoPreviousPos = 
                touchTwo.position - touchTwo.deltaPosition;

            // Distancia absoluta anterior.
            float previousMagnitude = (
                touchOnePreviousPos - touchTwoPreviousPos).magnitude;

            // Distancia absoluta atual.
            float currentMagnitude = (
                touchOne.position - touchTwo.position).magnitude;

            // diferença entre as distancias.
            float differenceMagnitude = 
                currentMagnitude - previousMagnitude;

            TouchZoom(differenceMagnitude * zoomSensitivity);
        }
        // SE houver apenas 1 dedo sobre a tela...
        else if (Input.touchCount == 1)
        {
            Debug.Log("ONE Finger!");

            // Pegar o dedo que tocou a tela.

            // Verificar SE a TouhPhase está no começo...
                // Atribuir a posição do toque na tela para coordenadas globais.

            // Verificar SE a TouhPhase está movendo...
                // Vector3 direction ==
                // Atribuir a posição do toque na tela para coordenadas globais.

                // Incrementar essa direção na camera.
        }
    }

    private void TouchZoom(float increment)
    {
        // Se a camera for ortografica...
        if (Camera.main.orthographic)
        {
            float orthoSize = Mathf.Clamp(
                Camera.main.orthographicSize - increment,
                zoomMinimum, zoomMaximum);

            Camera.main.orthographicSize = orthoSize;
        }
        else // Se a camera for em perspectiva...
        {
            float fieldSize = Mathf.Clamp(
                Camera.main.fieldOfView - increment,
                zoomMinimum, zoomMaximum);

            Camera.main.fieldOfView = fieldSize;
        }
    }
}
