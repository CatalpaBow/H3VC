# H3VC
Add 3d audio voicechat to H3MP.
WARNING! this is alpha build so I'm not guaranteed stable works.this is still in developing.
# How to use
H3VC runs automatically when hosting and joining H3MP.
However, at this stage it is not possible to select the audio input device within the game, so before starting H3VC, please select the audio device to be used preferentially from InputDevice in the Windows volume mixer.

# ChangeLog
0.0.2
Change: Changed microphone input implementation from Unity to NAudio
Fixed: Added processing to adjust the AudioPlayer playback position so that it does not exceed the head of the audio buffer.