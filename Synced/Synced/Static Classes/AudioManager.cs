// AudioManager.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-26
// Edited by:
// Göran F
//
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// using MonoGame.Framework;
// using MonoGame.Framework.Content.Pipeline;  "Content"" felar???
using Microsoft.Xna.Framework.Audio;

namespace Synced.Static_Classes
{
    static class AudioManager
    {
        // KB29: AudioManager...
        static AudioEngine mAudioEngine;
        static WaveBank mWaveBank;
        static SoundBank mSoundBank;
        static Cue mStartCue;
        static Cue mGameCue;
        static Cue mCrystalCaptureCue;
        static Cue mCrystalShootCue;
        static Cue mApplauseCue;
        static Cue mScoreCue;
        static Cue mScoreContCue;
        static Cue mExpandZoneCue;
        static Cue mBlowUpZoneCue;
        static Cue mBarrierBreakCue;
        static Cue mBarrierCrystalCaptureCue;

        static bool mGameIsRunning;

        static public void AudioLoadContent()
        {
            mAudioEngine = new AudioEngine(@"Content\Audio\GameAudio.xgs"); // sic! compiled version of .xap
            mWaveBank = new WaveBank(mAudioEngine, @"Content\Audio\Wave Bank.xwb");
            mSoundBank = new SoundBank(mAudioEngine, @"Content\Audio\Sound Bank.xsb"); ;
            PlayStartSound();
        }

        static public void AudioUpdate()
        {
            mAudioEngine.Update(); // As recommended on page 85
        }


        static public void PlayStartSound()
        {
            if (false == mGameIsRunning)
            {
                DebuggingHelper.AddLog("Audio - PlayStartSound", 0);
                mStartCue = mSoundBank.GetCue("start");
                mStartCue.Play();
            }
        }

        static public void PlayGameSound()
        {
            if (false == mGameIsRunning)
            {
                DebuggingHelper.AddLog("Audio - PlayGameSound", 0);
                mGameCue = mSoundBank.GetCue("game");
                mGameCue.Play();
                mStartCue.Stop(AudioStopOptions.Immediate);
                mGameIsRunning = true;
            }
        }

        static public void PlayCrystalCaptureSound()
        {
            DebuggingHelper.AddLog("Audio - PlayCrystalCaptureSound", 0);
            mCrystalCaptureCue = mSoundBank.GetCue("crystalCapture");
            mCrystalCaptureCue.Play();
        }

        static public void PlayCrystalShootSound()
        {
            DebuggingHelper.AddLog("Audio - PlayCrystalShootSound", 0);
            mCrystalShootCue = mSoundBank.GetCue("crystalShoot-2");
            mCrystalShootCue.Play();
        }

        static public void PlayScoreSound()
        {
            DebuggingHelper.AddLog("Audio - PlayScoreSound", 0);
            mScoreCue = mSoundBank.GetCue("goal");
            mScoreCue.Play();
            mApplauseCue = mSoundBank.GetCue("goalApplause");
            mApplauseCue.Play();
        }

        static public void PlayScoreContSound()
        {
            DebuggingHelper.AddLog("Audio - PlayScoreContSound", 0);
            mScoreContCue = mSoundBank.GetCue("splash-to-drop");
            mScoreContCue.Play();
        }

        static public void PlayExpandZoneSound()
        {
            DebuggingHelper.AddLog("Audio - PlayExpandZoneSound", 0);
            mExpandZoneCue = mSoundBank.GetCue("expand-zone");
            mExpandZoneCue.Play();
        }

        static public void PlayBlowUpZoneSound()
        {
            DebuggingHelper.AddLog("Audio - PlayBlowUpZoneSound", 0);
            mBlowUpZoneCue = mSoundBank.GetCue("blow-up-zone");
            mBlowUpZoneCue.Play();
        }

        static public void PlayBarrierBreakSound()
        {
            DebuggingHelper.AddLog("Audio - PlayBarrierBreakSound", 0);
            mBarrierBreakCue = mSoundBank.GetCue("barrier_break");
            mBarrierBreakCue.Play();
        }

        static public void PlayBarrierCrystalCaptureSound()
        {
            DebuggingHelper.AddLog("Audio - PlayBarrierCrystalCaptureSound", 0);
            mBarrierCrystalCaptureCue = mSoundBank.GetCue("barrier_crystal_capture");
            mBarrierCrystalCaptureCue.Play();
        }

    }
}
