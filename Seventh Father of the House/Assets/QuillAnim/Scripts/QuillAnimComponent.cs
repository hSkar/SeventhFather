using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace QuillAnim {
public class QuillAnimComponent : MonoBehaviour {
    public int frameRate = 12;
    public bool persistFrames = false;
        private bool _isPlaying = false;
    private QuillAnimation _animation;

    void Start() {
        _animation = new QuillAnimation(this.transform, frameRate, persistFrames);
        _animation.SetFrame(0);
    }

   public void Play()
    {
        _isPlaying = true;
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
