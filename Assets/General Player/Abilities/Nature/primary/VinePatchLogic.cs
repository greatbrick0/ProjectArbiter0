using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VinePatchLogic : MonoBehaviour
{

    public PlayerSpellNatureSpikes playerSpikeRef;

    

    

    [SerializeField]
    private VisualEffect vfxRef;

    [SerializeField]
    private GameObject triggerBox;

    [SerializeField]
    private float lifespan;

     [SerializeField]
    private List<float> spikeIntervals;

    private float spikeTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(RequestDestroy),lifespan);
    }

    private void Update()
    {
        spikeTimer += Time.deltaTime;
        foreach (float f in spikeIntervals)
        {
            if (Mathf.Abs(spikeTimer - f) <= 0.1)
            {
                Trigger();
                spikeIntervals.Remove(f);
                return;
            }
        }
    }
    private void RequestDestroy()
    {
        Destroy(this.gameObject);
    }

    private void Trigger()
    {

      
        triggerBox.SetActive(true);
        triggerBox.GetComponent<spikeDamager>().hitTargets.Clear();
        vfxRef.SendEvent("Spikes");

        Invoke(nameof(disableTrigger),1.1f);
    }

    private void disableTrigger()
    {
        triggerBox.SetActive(false);
    }

}
