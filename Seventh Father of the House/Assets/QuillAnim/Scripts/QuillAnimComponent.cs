using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace QuillAnim {
public class QuillAnimComponent : MonoBehaviour {
    public int frameRate = 12;
    public bool persistFrames = false;
    private QuillAnimation _animation;
    public bool playOnStart;
    private bool _isPlaying;

    public void Play()
    {
       _isPlaying = true;
    }

    void Start() {
        _animation = new QuillAnimation(this.transform, frameRate, persistFrames);

        _animation.SetFrame(0);
    }

    void Update() {
            if (!_isPlaying)
                return;

        _animation.Update();
    }
    
    public void Reset() {
        _animation.SetFrame(0);
    }
}
}
