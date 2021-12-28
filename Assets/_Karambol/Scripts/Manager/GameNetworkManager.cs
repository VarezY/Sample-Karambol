using System.Collections;
using System.Collections.Generic;
using Niantic.ARDK.AR.Networking;
using Niantic.ARDK.AR.Networking.ARNetworkingEventArgs;
using Niantic.ARDK.Extensions;
using Niantic.ARDKExamples.Pong;
using UnityEngine;
using UnityEngine.UI;

public class GameNetworkManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private Button joinButton;
  
    [SerializeField]
    private GameObject startGameButton = null;
    
    [SerializeField]
    private FeaturePreloadManager preloadManager = null;
    
    private IARNetworking _arNetworking;
    private MessagingManager _messagingManager;


  
    private void Start()
    {
        startGameButton.SetActive(false);
        ARNetworkingFactory.ARNetworkingInitialized += OnAnyARNetworkingSessionInitialized;

        if (preloadManager.AreAllFeaturesDownloaded())
            OnPreloadFinished(true);
        else
            preloadManager.ProgressUpdated += PreloadProgressUpdated;
    }

    private void PreloadProgressUpdated(FeaturePreloadManager.PreloadProgressUpdatedArgs args)
    {
        if (args.PreloadAttemptFinished)
        {
            preloadManager.ProgressUpdated -= PreloadProgressUpdated;
            OnPreloadFinished(args.FailedPreloads.Count == 0);
        }
    }

    private void OnPreloadFinished(bool success)
    {
        if (success)
            joinButton.interactable = true;
        else
            Debug.LogError("Failed to download resources needed to run AR Multiplayer");
    }
    
    private void OnAnyARNetworkingSessionInitialized(AnyARNetworkingInitializedArgs args)
    {
        _arNetworking = args.ARNetworking;
        // _arNetworking.PeerStateReceived += OnPeerStateReceived;
        //
        // _arNetworking.ARSession.FrameUpdated += OnFrameUpdated;
        // _arNetworking.Networking.Connected += OnDidConnect;
    }
    private void OnDestroy()
    {
        ARNetworkingFactory.ARNetworkingInitialized -= OnAnyARNetworkingSessionInitialized;

        if (_arNetworking != null)
        {
            // _arNetworking.PeerStateReceived -= OnPeerStateReceived;
            // _arNetworking.ARSession.FrameUpdated -= OnFrameUpdated;
            // _arNetworking.Networking.Connected -= OnDidConnect;
        }
    }
}
