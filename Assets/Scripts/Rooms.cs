using UnityEngine;
using System.Collections;

/// <summary>
/// Zarządza stanem ambientu pokoju w zależności od pozycji gracza.
/// </summary>
public class Rooms : MonoBehaviour
{
    private Coroutine exitCoroutine;

    /// <summary>
    /// Wywoływane, gdy inny collider pozostaje wewnątrz triggera.
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        // Sprawdza, czy obiekt ma tag "Player".
        if (other.CompareTag("Player"))
        {
            // Anuluj opóźnienie jeśli gracz wrócił do środka.
            if (exitCoroutine != null)
            {
                StopCoroutine(exitCoroutine);
                exitCoroutine = null;
            }

            // Znajduje instancję RoomAmbient w scenie i ustawia flagę na true.
            RoomAmbient roomAmbient = FindObjectOfType<RoomAmbient>();
            if (roomAmbient != null)
            {
                roomAmbient.ambientActivated = true;
            }
        }
    }

    /// <summary>
    /// Wywoływane, gdy inny collider opuszcza trigger.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        // Sprawdza, czy obiekt ma tag "Player".
        if (other.CompareTag("Player"))
        {
            exitCoroutine = StartCoroutine(DelayedExit());
        }
    }

    /// <summary>
    /// Opóźnia wyłączenie ambientu aby uniknąć błysku przy skoku.
    /// </summary>
    private IEnumerator DelayedExit()
    {
        yield return new WaitForSeconds(0.5f);

        // Znajduje instancję RoomAmbient w scenie i ustawia flagę na false.
        RoomAmbient roomAmbient = FindObjectOfType<RoomAmbient>();
        if (roomAmbient != null)
        {
            roomAmbient.ambientActivated = false;
        }

        exitCoroutine = null;
    }
}