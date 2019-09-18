using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScript : MonoBehaviour
{
    // Inserir o material de Physics do sprite bola.
    public PhysicsMaterial2D PhysicsMat2D;

    // Armazenando a posição do toque na tela.
    private Vector2 touchPosition;

    // Permitir que o sprite bola seja movido?
    private bool moveAllowed = false;

    // Referencia ao componente "Rigidbody2D".
    private Rigidbody2D myRigidbody2D;

    void Start()
    {
        // Referencia do componente.
        myRigidbody2D = GetComponent<Rigidbody2D>();

        // Atribuição do PhysicsMaterial2D ao component Collider2D.
        GetComponent<CircleCollider2D>().sharedMaterial = PhysicsMat2D;
    }

    void FixedUpdate()
    {
        Debug.Log("touchCount: " + Input.touchCount);

        // Iniciando o evento de toque.
        if(Input.touchCount > 0)
        {
            // Armazenando a referencia do toque.
            // Pegando o status do dedo que toca a tela.
            Touch myTouch = Input.GetTouch(0);

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(
                myTouch.position);
            //Debug.Log("touchPos: " + touchPos);

            switch (myTouch.phase)
            {
                // Usuário toca com o dedo na tela...
                case TouchPhase.Began:
                    Debug.Log("TouchPhase.Began");
                    // Assim que o usuário tocar no GameObject...
                    // SE a posição do toque sobrepor a posição do collider...
                    if (GetComponent<Collider2D>() == 
                            Physics2D.OverlapPoint(touchPos))
                    {
                        Debug.Log("Is overlap!");
                        // Armazenar a posição em x,y para obter o...
                        // ...delocamento entre a posição do toque e...
                        // ... a posição do GameObject.
                        touchPosition.x = touchPos.x - transform.position.x;
                        touchPosition.y = touchPos.y - transform.position.y;

                        // SE o toque for dentro do gameobject...
                        // ...então será permitido mover com ele.
                        moveAllowed = true;

                        // Restringir algumas propriedade do Rigidbody2D.
                        myRigidbody2D.freezeRotation = true;
                        myRigidbody2D.velocity = new Vector2(0f, 0f);
                        myRigidbody2D.gravityScale = 0f;

                        // Remover o PhysicsMaterial2D do collider.
                        GetComponent<CircleCollider2D>().sharedMaterial = null;  
                    }
                    else
                    {
                        Debug.Log("Overlap the screen only!");
                    }

                    break;
                // Usuário move o dedo sobre a tela...
                case TouchPhase.Moved:
                    Debug.Log("TouchPhase.Moved");

                    if (GetComponent<CircleCollider2D>() ==
                            Physics2D.OverlapPoint(touchPos) && moveAllowed)  
                    {
                        // Atualizar a posição do meu GameObject.
                        myRigidbody2D.MovePosition(new Vector2(
                            touchPos.x - touchPosition.x,
                            touchPos.y - touchPosition.y));
                    }

                    break;
                // Usuário remove o dedo da tela...
                case TouchPhase.Ended:
                    Debug.Log("TouchPhase.Ended");

                    // Não permitir mover o gameobject.
                    moveAllowed = false;

                    // Restaurar alguns parametros...
                    myRigidbody2D.freezeRotation = false;
                    myRigidbody2D.gravityScale = 2;

                    GetComponent<CircleCollider2D>().sharedMaterial = PhysicsMat2D;  

                    break;
            }
        }

    }
}
