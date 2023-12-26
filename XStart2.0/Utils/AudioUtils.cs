using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using XStart2._0.Config;
using XStart2._0.Const;

namespace XStart2._0.Utils {
    class AudioUtils {
        public const int CHANGE = 0;
        public const int START = 1;
        static readonly System.Media.SoundPlayer changePlayer = new System.Media.SoundPlayer { SoundLocation = Configs.AppStartPath + Constants.AUDIO_CHANGE };
        static readonly System.Media.SoundPlayer player = new System.Media.SoundPlayer { SoundLocation = Configs.AppStartPath + Constants.AUDIO_START };
        public static void PlayWav(int index) {
            switch (index) {
                case CHANGE: changePlayer.Play(); break;
                case START: player.Play(); break;
                default: break;
            }
        }

        public static void Dispose() {
            changePlayer.Dispose();
            player.Dispose();
        }

        /// <summary>
        /// 获取当前设备音量
        /// </summary>
        /// <returns></returns>
        public static int GetDeviceVolume(string deviceName, DataFlow df = DataFlow.Capture, DeviceState ds = DeviceState.Active) {
            int volume = 0;
            MMDevice mMDevice = GetDevice(df, ds, deviceName);
            if (null != mMDevice) {
                volume = (int)(mMDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
            }
            return volume;
        }

        /// <summary>
        /// 设置麦克风音量
        /// </summary>
        /// <param name="volume">2-100</param>
        public static void SetDeviceVolume(string deviceName, int volume, DataFlow df = DataFlow.Capture, DeviceState ds = DeviceState.Active) {
            MMDevice mMDevice = GetDevice(df, ds, deviceName);
            if (null != mMDevice) {
                mMDevice.AudioEndpointVolume.MasterVolumeLevelScalar = volume / 100.0f;
            } else {
                throw new Exception("未找相关设备");
            }
        }
        public static bool GetDeviceMute(string deviceName, DataFlow df = DataFlow.Capture, DeviceState ds = DeviceState.Active) {
            MMDevice mMDevice = GetDevice(df, ds, deviceName);
            if (null != mMDevice) {
                return mMDevice.AudioEndpointVolume.Mute;
            }
            return false;
        }

        public static void SetDeviceMute(string deviceName, bool muted, DataFlow df = DataFlow.Capture, DeviceState ds = DeviceState.Active) {
            MMDevice mMDevice = GetDevice(df, ds, deviceName);
            if (null != mMDevice) {
                mMDevice.AudioEndpointVolume.Mute = muted;
            } else {
                throw new Exception("未找相关设备");
            }
        }

        public static MMDevice GetDevice(DataFlow dataFlow, DeviceState ds, string deviceName) {
            var enumerator = new MMDeviceEnumerator();
            IEnumerable<MMDevice> captureDevices = enumerator.EnumerateAudioEndPoints(dataFlow, ds).ToArray();
            if (captureDevices.Count() > 0) {
                foreach (MMDevice device in captureDevices) {
                    if (device.FriendlyName.Contains(deviceName)) {
                        return device;
                    }
                }
            }
            return null;
        }
    }
}
