using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRSimpleInteractable))]
public class Correct_btn : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject puerta;
    [SerializeField] private XRSimpleInteractable xrSimpleInteractable;
    [SerializeField] private ParticleSystem particleSystemToPlay;

    [Header("Comportamiento")]
    [SerializeField] private bool disableButtonAfterUse = true;

    private bool hasBeenUsed = false;

    private void Reset()
    {
        xrSimpleInteractable = GetComponent<XRSimpleInteractable>();
        particleSystemToPlay = GetComponent<ParticleSystem>();
    }

    private void Awake()
    {
        if (xrSimpleInteractable == null)
            xrSimpleInteractable = GetComponent<XRSimpleInteractable>();

        if (particleSystemToPlay == null)
            particleSystemToPlay = GetComponent<ParticleSystem>();

        if (xrSimpleInteractable != null && xrSimpleInteractable.interactionManager == null)
        {
            XRInteractionManager manager = FindFirstObjectByType<XRInteractionManager>();
            if (manager != null)
                xrSimpleInteractable.interactionManager = manager;
        }

        if (particleSystemToPlay != null)
        {
            var main = particleSystemToPlay.main;
            main.playOnAwake = false;
            main.loop = false;

            particleSystemToPlay.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    private void OnEnable()
    {
        if (xrSimpleInteractable == null)
            xrSimpleInteractable = GetComponent<XRSimpleInteractable>();

        if (xrSimpleInteractable != null)
            xrSimpleInteractable.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnDisable()
    {
        if (xrSimpleInteractable != null)
            xrSimpleInteractable.selectEntered.RemoveListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        Correcto();
    }

    public void Correcto()
    {
        if (hasBeenUsed)
            return;

        hasBeenUsed = true;

        if (puerta != null)
            puerta.SetActive(false);

        if (particleSystemToPlay != null)
            StartCoroutine(PlayParticlesAndDisable());
        else if (disableButtonAfterUse)
            gameObject.SetActive(false);
    }

    private IEnumerator PlayParticlesAndDisable()
    {
        particleSystemToPlay.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        particleSystemToPlay.Play();

        float totalDuration = particleSystemToPlay.main.duration;

        if (particleSystemToPlay.main.startLifetime.mode == ParticleSystemCurveMode.TwoConstants)
            totalDuration += particleSystemToPlay.main.startLifetime.constantMax;
        else if (particleSystemToPlay.main.startLifetime.mode == ParticleSystemCurveMode.Constant)
            totalDuration += particleSystemToPlay.main.startLifetime.constant;

        yield return new WaitForSeconds(totalDuration);

        particleSystemToPlay.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        if (disableButtonAfterUse)
            gameObject.SetActive(false);
    }
}