using ScreenRecorderLib;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using VL.Core.CompilerServices;
using VL.Lib.Collections;

namespace VL.ScreenRecorder
{
    [Serializable]
    public class AudioInputsEnum : DynamicEnumBase<AudioInputsEnum, AudioInputsEnumDefinition>
    {
        public AudioInputsEnum(string value) : base(value)
        {
        }

        [CreateDefault]
        public static AudioInputsEnum CreateDefault()
        {
            //use method of base class if nothing special required
            return CreateDefaultBase();
        }
    }

    public class AudioInputsEnumDefinition : DynamicEnumDefinitionBase<AudioInputsEnumDefinition>
    {
        //return the current enum entries
        protected override IReadOnlyDictionary<string, object> GetEntries()
        {
            var inputs = new Dictionary<string, object>();

            inputs.Add("None", null);

            foreach (var d in Recorder.GetSystemAudioDevices(AudioDeviceSource.InputDevices))
            {
                //the return dictionary holds the names of the entries as key with an optional "tag"
                //here the tag is null but you can provide any object that you want to associate with the entry
                inputs[d.FriendlyName] = d.DeviceName;
            }

            inputs.Add("Default", "");

            return inputs;
        }

        protected override IObservable<object> GetEntriesChangedObservable()
        {
            return Observable.Empty<object>();
        }
    }

    [Serializable]
    public class AudioOutputsEnum : DynamicEnumBase<AudioOutputsEnum, AudioOutputsEnumDefinition>
    {
        public AudioOutputsEnum(string value) : base(value)
        {
        }

        [CreateDefault]
        public static AudioOutputsEnum CreateDefault()
        {
            //use method of base class if nothing special required
            return CreateDefaultBase();
        }
    }

    public class AudioOutputsEnumDefinition : DynamicEnumDefinitionBase<AudioOutputsEnumDefinition>
    {
        //return the current enum entries
        protected override IReadOnlyDictionary<string, object> GetEntries()
        {
            var outputs = new Dictionary<string, object>();

            outputs.Add("None", null);

            foreach (var d in Recorder.GetSystemAudioDevices(AudioDeviceSource.OutputDevices))
            {
                //the return dictionary holds the names of the entries as key with an optional "tag"
                //here the tag is null but you can provide any object that you want to associate with the entry
                outputs[d.FriendlyName] = d.DeviceName;
            }

            outputs.Add("Default", "");

            return outputs;
        }

        protected override IObservable<object> GetEntriesChangedObservable()
        {
            return Observable.Empty<object>();
        }
    }
}
