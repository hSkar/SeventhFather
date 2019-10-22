﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuillAnim {

/*
	Global counter singleton
 */
public class QuillAnimSystem : SingletonObject<QuillAnimSystem> {
	// fps -> millisecond timer
	List<int> _frameRates = new List<int>(5);
	List<float> _frameRateCounters = new List<float>(5);

	public void RequestFPSCounter(int fps) {
		if(_frameRates.Contains(fps))
			return; // only keep one counter for each frame rate

		_frameRates.Add(fps);
		_frameRateCounters.Add(0.0f);
	}

	public bool ShouldUpdate(int fps) {
		return _frameRateCounters[_frameRates.FindIndex(id => id == fps)] == 0.0f;
	}

	void Update () {
		for(int i = 0; i < _frameRates.Count; ++i) {
			_frameRateCounters[i] += Time.deltaTime * 580;
			if(_frameRateCounters[i] >= 500 / _frameRates[i]) {
				_frameRateCounters[i] = 0.0f;
			}
		}
	}
}

/*
	Class that manages Quill FBX animation logic
	(i.e. switching frame GameObjects on and off and counting frames)

	This is in its own class to allow multiple ways of playing back the animation.
	(like QuillAnimNode for state machines and QuillAnimComponent for standalone use)
*/
public class QuillAnimation {
	private GameObject[][] _layersFrames;
    private int _overallFrameCount;
    private int _currentFrame = 0;
    private Dictionary<GameObject, int[]> _layersFrameNumbers;
    private Dictionary<GameObject, int> _activeLayerFrame;
	private bool _persist_frames;
	private int _frameRate;
        private bool _loop;
    public int _startFrameDelay;
        
	public QuillAnimation(Transform animation_root, int frameRate, bool keep_layers_on, bool loop, Transform[] layers = null) {
        bool hasCustomLayers = (layers != null && layers.Length > 0);
        _loop = loop;
        _overallFrameCount = 0;
        _persist_frames = keep_layers_on;
		_frameRate = frameRate;
		QuillAnimSystem.instance.RequestFPSCounter(_frameRate);

		var root = animation_root.Find("BakedMesh");
		if(root) {
			root = root.Find("Root");
		}
		else {
			root = animation_root.Find("Root");
		}

		int layer_count = root.childCount;
            if(layers != null)
            {
                layer_count = layers.Length;
            }

		    _layersFrames = new GameObject[layer_count][];  // create arrays for each layer
                                                        //_layersFrameNumbers = new int[layer_count][];
            _layersFrameNumbers = new Dictionary<GameObject, int[]>();
            _activeLayerFrame = new Dictionary<GameObject, int>();

            for (int i = 0; i < layer_count; ++i) {          // for each layer, add the frames
            var layer = hasCustomLayers ? layers[i] : root.GetChild(i);
            
            var frame_count = layer.childCount;                 // get the number of frames
            _layersFrames[i] = new GameObject[frame_count];     // create an array for the frames
                _layersFrameNumbers.Add(layer.gameObject, new int[frame_count]);
                _activeLayerFrame.Add(layer.gameObject, 0);
            for(int f = 0; f < frame_count; ++f) {              // add all the frames
                _layersFrames[i][f] = layer.GetChild(f).gameObject;
                var frameName = layer.GetChild(f).gameObject.name;
                var splitName = frameName.Split('_');
                frameName = splitName[splitName.Length - 1];

                var frameNumber = int.Parse(frameName);
                _layersFrameNumbers[layer.gameObject][f] = frameNumber;
                    if (frameNumber > _overallFrameCount) _overallFrameCount = frameNumber;

                if(f > 0)
                {
                    _layersFrames[i][f].SetActive(false);
                }
            }
        }

		// take the longest frame as the length of the animation
      
        //foreach(var layer in _layersFrames) {
        //    if(layer.Length > _overallFrameCount)
        //        _overallFrameCount = layer.Length;
        //}

            //Getting the largest frame count
        //foreach(var layerIndex in _layersFrameNumbers)
        //    {
        //        Debug.Log("Length = " + layerIndex.Value.Length);
        //        if (layerIndex.Value[layerIndex.Value.Length - 1] > _overallFrameCount)
        //            _overallFrameCount = layerIndex.Value[layerIndex.Value.Length - 1];
        //    }

            //SetFirstFrames();
	}

        //Disabling all frames except the first
        private void SetFirstFrames()
        {
            foreach(var layer in _layersFrames)
            {
                for(int i = 1; i < layer.Length; i++)
                {
                    layer[i].SetActive(false);
                }
            }
        }

        public void Update() {
		if(QuillAnimSystem.instance.ShouldUpdate(_frameRate)) {
            int next_frame = _currentFrame + 1;
            if(next_frame > _overallFrameCount) {
                next_frame = 0;
            }

			SetFrame(next_frame);
		}
	}
    
        private float _lastTimestamp = 0.0f;
	public void SetFrame(int next_frame) {
            int index = 0;
            //Debug.Log("Current Frame = " + _currentFrame);
        foreach(var layer in _layersFrameNumbers)
        {
            int currentIndex = _activeLayerFrame[layer.Key];
            if(next_frame == 0 && _loop)
            {
                _layersFrames[index][currentIndex].SetActive(false);
                _layersFrames[index][0].SetActive(true);
                _activeLayerFrame[layer.Key] = 0;
            }

            else if (currentIndex + 1 < _layersFrames[index].Length)
            {
                var nextFrame = _layersFrameNumbers[layer.Key][currentIndex + 1];

                if (next_frame >= nextFrame)
                {
                    _layersFrames[index][currentIndex].SetActive(false);
                    _layersFrames[index][currentIndex + 1].SetActive(true);
                    _activeLayerFrame[layer.Key] += 1;
                }
            }
                
            index++;
        }

            //foreach(var layer in _layersFrames) {
            //              //Match next frame with current index

            //	if(_currentFrame < layer.Length)
            //		layer[_currentFrame].SetActive(false);

            //	if(next_frame < layer.Length) {
            //		layer[next_frame].SetActive(true);
            //	}
            //	else {
            //		if(_persist_frames) {
            //			layer[layer.Length - 1].SetActive(true);
            //		}
            //	}
            //}

            //Debug.Log("Diff = " + (Time.time - _lastTimestamp));
        _lastTimestamp = Time.time;
		_currentFrame = next_frame;
	}
}

}
