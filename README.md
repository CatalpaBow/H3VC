# H3VC
Add 3d audio voicechat to H3MP.
## WARNING! this is alpha build so I'm not guaranteed stable works.this is still in developing.
# How to use
You can configure audio settings using the H3VC settings menu located within the H3VR wrist menu.
You can set the audio device, volume, mute status, and check the current volume of the audio device.
Additionally, there is a sound mode setting that sets the range in which the sound can be heard.
### SoundMode
Settings for how the sound sounds. Postional allows you to hear voices just like in real life.
localScene sounds non-positionally to users within the scene you are currently in.
global will be heard by users in all scenes, including other scenes.

# ChangeLog
0.1.1
 - Resubmission to thunderstore
0.1.0
### Added
 - Add GUI Audio Setting menu
 - Add SoundMode setting
 - Add Mute setting
 - Add AudioDevice setting
 - Add VoiceLevelMeter
 - Add volume setting
0.0.2 
Change: Changed microphone input implementation from Unity to NAudio
Fixed: Added processing to adjust the AudioPlayer playback position so that it does not exceed the head of the audio buffer.