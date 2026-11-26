
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    [SerializeField] private float upwardSpeed = 3f;
    [SerializeField] private float downwardSpeed = 5f;
    [SerializeField] private float groundY = -1.5f;

    private float targetY;
    private bool isMovingUp = true;

    private void Start()
    {
        // Coin'in baþlangýç yüksekliðini kaydet.
        float startY = transform.position.y;

        // Coin zeminin altýndaysa doðrudan zemin seviyesine taþý.
        if (startY <= groundY)
        {
            transform.position = new Vector3(
                transform.position.x,
                groundY,
                transform.position.z
            );

            isMovingUp = false;
            return;
        }

        // Her Coin için 0.3 ile 0.5 arasýnda rastgele yükselme mesafesi belirle.
        float randomUpwardDistance = Random.Range(0.1f, 0.3f);

        // Coin'in ulaþacaðý yukarý yönlü hedef yüksekliði hesapla.
        targetY = startY + randomUpwardDistance;
    }

    private void Update()
    {
        if (isMovingUp)
        {
            // Coin'i belirlenen yukarý hedefe doðru hareket ettir.
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(
                    transform.position.x,
                    targetY,
                    transform.position.z
                ),
                upwardSpeed * Time.deltaTime
            );

            // Yukarý hareket tamamlanýnca aþaðý düþüþü baþlat.
            if (transform.position.y >= targetY)
            {
                isMovingUp = false;
            }
        }
        else
        {
            // Coin'i zemin seviyesine doðru indir.
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(
                    transform.position.x,
                    groundY,
                    transform.position.z
                ),
                downwardSpeed * Time.deltaTime
            );
        }
    }
}
