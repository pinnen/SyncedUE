// AudioManager.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-28
// Edited by:
// Göran F
//
// 
// Status: Adapted for MonoGames without .xgs-file
// Info from:
// http://rbwhitaker.wikidot.com/playing-sound-effects
// http://www.codeproject.com/Articles/577375/TheplusdifferencesplusbetweenplusSoundEffectplusan
// https://www.youtube.com/watch?v=inJK28LdGbI&index=27
// 
// Notes:
// Use "SoundEffectInstance" instead of "SoundEffect" for higher level of control.
//

// ToDo: bara två ljudeffekter aktiva f.n.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Synced.Static_Classes
{
    static class AudioManager
    {
        // KB29: AudioManager...
        static SoundEffectInstance mStartSoundEffect;
        static SoundEffectInstance mGameSoundEffect;
        static SoundEffectInstance mCrystalCaptureSoundEffect;
        static SoundEffectInstance mCrystalShootSoundEffect;
        static SoundEffectInstance mApplauseSoundEffect;
        static SoundEffectInstance mScoreSoundEffect;
        static SoundEffectInstance mScoreContSoundEffect;
        static SoundEffectInstance mExpandZoneSoundEffect;
        static SoundEffectInstance mBlowUpZoneSoundEffect;
        static SoundEffectInstance mBarrierBreakSoundEffect;
        static SoundEffectInstance mBarrierCrystalCaptureSoundEffect;

        static bool mGameIsRunning;

        static void xxx(int i)
        {

        }

        static public void AudioLoadContent(Game _game)
        {
            // Prepare by loading used sound effects from xnb.-files (created by MonoGame tool "MonoGame Pileline")

            SoundEffect soundEffect; // ToDo: Behövs bara temp???

            soundEffect = _game.Content.Load<SoundEffect>(@"Audio\start");
            mStartSoundEffect = soundEffect.CreateInstance();

            soundEffect = _game.Content.Load<SoundEffect>(@"Audio\game");
            mGameSoundEffect = soundEffect.CreateInstance();

            soundEffect = _game.Content.Load<SoundEffect>(@"Audio\crystalCapture");
            mCrystalCaptureSoundEffect = soundEffect.CreateInstance();

            soundEffect = _game.Content.Load<SoundEffect>(@"Audio\crystalShoot-2");
            mCrystalShootSoundEffect = soundEffect.CreateInstance();

            soundEffect = _game.Content.Load<SoundEffect>(@"Audio\goalApplause");
            mApplauseSoundEffect = soundEffect.CreateInstance();

            soundEffect = _game.Content.Load<SoundEffect>(@"Audio\goal");
            mScoreSoundEffect = soundEffect.CreateInstance();

            soundEffect = _game.Content.Load<SoundEffect>(@"Audio\splash-to-drop");
            mScoreContSoundEffect = soundEffect.CreateInstance();

            soundEffect = _game.Content.Load<SoundEffect>(@"Audio\expand_zone");
            mExpandZoneSoundEffect = soundEffect.CreateInstance();

            soundEffect = _game.Content.Load<SoundEffect>(@"Audio\blow-up-zone");
            mBlowUpZoneSoundEffect = soundEffect.CreateInstance();

            soundEffect = _game.Content.Load<SoundEffect>(@"Audio\barrier_break");
            mBarrierBreakSoundEffect = soundEffect.CreateInstance();

            soundEffect = _game.Content.Load<SoundEffect>(@"Audio\barrier_crystal_capture");
            mBarrierCrystalCaptureSoundEffect = soundEffect.CreateInstance();


            PlayStartSound();
        }

        static public void UnloadAudioContent()
        {
            // ...
            mStartSoundEffect.Dispose();
            mGameSoundEffect.Dispose();
            mCrystalCaptureSoundEffect.Dispose();
            mCrystalShootSoundEffect.Dispose();
            mApplauseSoundEffect.Dispose();
            mScoreSoundEffect.Dispose();
            mScoreContSoundEffect.Dispose();
            mExpandZoneSoundEffect.Dispose();
            mBlowUpZoneSoundEffect.Dispose();
            mBarrierBreakSoundEffect.Dispose();
            mBarrierCrystalCaptureSoundEffect.Dispose();
            // ...
        }

        static public void AudioUpdate()
        {
            // ToDo: not used in MonoGame???
            // mAudioEngine.Update(); // As recommended on page 85 in XNA-book!
        }


        static public void PlayStartSound()
        {
            if (false == mGameIsRunning)
            {
                DebuggingHelper.AddLog("Audio - PlayStartSound", 0);
                mStartSoundEffect.IsLooped = true;

                // ToDo experiments...
                mStartSoundEffect.Volume = 0.2f;
                mStartSoundEffect.Pan = -0.5f;
                mStartSoundEffect.Pitch = 0.1f;

                mStartSoundEffect.Play();
                mGameSoundEffect.Stop();
            }
        }

        static public void PlayGameSound()
        {
            if (false == mGameIsRunning)
            {
                DebuggingHelper.AddLog("Audio - PlayGameSound", 0);
                mGameSoundEffect.IsLooped = true;
                mGameSoundEffect.Play();
                mStartSoundEffect.Stop();
                mGameIsRunning = true;
            }
        }

        static public void PlayCrystalCaptureSound()
        {
            DebuggingHelper.AddLog("Audio - PlayCrystalCaptureSound", 0);
            mCrystalCaptureSoundEffect.Play();
        }

        static public void PlayCrystalShootSound()
        {
            DebuggingHelper.AddLog("Audio - PlayCrystalShootSound", 0);
            mCrystalShootSoundEffect.Play();
        }

        static public void PlayScoreSound()
        {
            DebuggingHelper.AddLog("Audio - PlayScoreSound", 0);
            mScoreSoundEffect.Play();
            mApplauseSoundEffect.Play();
        }

        static public void PlayScoreContSound()
        {
            DebuggingHelper.AddLog("Audio - PlayScoreContSound", 0);
            mScoreContSoundEffect.Play();
        }

        static public void PlayExpandZoneSound()
        {
            DebuggingHelper.AddLog("Audio - PlayExpandZoneSound", 0);
            mExpandZoneSoundEffect.Play();
        }

        static public void PlayBlowUpZoneSound()
        {
            DebuggingHelper.AddLog("Audio - PlayBlowUpZoneSound", 0);
            mBlowUpZoneSoundEffect.Play();
        }

        static public void PlayBarrierBreakSound()
        {
            DebuggingHelper.AddLog("Audio - PlayBarrierBreakSound", 0);
            mBarrierBreakSoundEffect.Play();
        }

        static public void PlayBarrierCrystalCaptureSound()
        {
            DebuggingHelper.AddLog("Audio - PlayBarrierCrystalCaptureSound", 0);
            mBarrierCrystalCaptureSoundEffect.Play();
        }

    }
}
