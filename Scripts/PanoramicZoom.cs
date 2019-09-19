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

    // Angulo Delta entre dois pontos de contato.
    private float turnAngleDelta = 0f;

    // Angulo entre dois pontos de contato.
    private float turnAngle = 0f;

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

            ////////// Atualização! //////////

            // Armazena a rotação da Camera.
            Quaternion desiredRotation = transform.rotation;

            Calculate();

            if (Mathf.Abs(turnAngleDelta) > 0)
            {
                // Rotate.
                Vector3 rotationDeg = Vector3.zero;

                rotationDeg.z = -turnAngleDelta;

                desiredRotation *= Quaternion.Euler(rotationDeg);
            }

            // Atualiza a rotação da camera!
            transform.rotation = desiredRotation;
        }

        // SE houver apenas 1 dedo sobre a tela...
        else if (Input.touchCount == 1)
        {
            Debug.Log("ONE Finger!");

            // Pegar o dedo que tocou a tela.
            Touch touchOne = Input.GetTouch(0);

            // Verificar SE a TouhPhase está no começo...
            if (touchOne.phase == TouchPhase.Began)
            {
                // Atribuir a posição do toque na tela para coordenadas globais.
                touchStart = Camera.main.ScreenToWorldPoint(
                    touchOne.position);
            }

            // Verificar SE a TouhPhase está movendo...
            if (touchOne.phase == TouchPhase.Moved)
            {
                // Atribuir a posição do toque na tela para coordenadas globais.
                Vector3 direction = touchStart -
                    Camera.main.ScreenToWorldPoint(touchOne.position);

                Debug.Log("direction: " + direction);

                direction.z = -10f;

                // Incrementar essa direção na camera.
                Camera.main.transform.position += direction;
            }
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

    public void Calculate()
    {
        Touch touch1 = Input.touches[0];
        Touch touch2 = Input.touches[1];

        if (touch1.phase == TouchPhase.Moved ||
            touch2.phase == TouchPhase.Moved)
        {
            // Verficiar o angulo entre a posição dos dois dedos.
            turnAngle = FingersAngle(
                touch1.position, touch2.position);

            float prevTurn = FingersAngle(
                touch1.position - touch1.deltaPosition,
                touch2.position - touch2.deltaPosition);

            turnAngleDelta = Mathf.DeltaAngle(
                prevTurn, turnAngle);
        }
    }

    private float FingersAngle(Vector2 posOne, Vector2 posTwo)
    {
        Vector2 from = posTwo - posOne;

        Vector2 to = new Vector2(1, 0);

        float resultAngle = Vector2.Angle(from, to);
        Debug.Log("resultAngle: " + resultAngle);

        Vector3 crossProduct = Vector3.Cross(from, to);

        if (crossProduct.z > 0)
        {
            resultAngle = 360f - resultAngle;
        }

        return resultAngle;
    }
}
