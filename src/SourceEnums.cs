using ScreenRecorderLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using VL.Core.CompilerServices;
using VL.Lib.Collections;

namespace VL.ScreenRecorder
{
    [Serializable]
    public class DisplaysEnum : DynamicEnumBase<DisplaysEnum, DisplaysEnumDefinition>
    {
        public DisplaysEnum(string value) : base(value)
        {
        }

        [CreateDefault]
        public static DisplaysEnum CreateDefault()
        {
            //use method of base class if nothing special required
            return CreateDefaultBase();
        }
    }

    public class DisplaysEnumDefinition : DynamicEnumDefinitionBase<DisplaysEnumDefinition>
    {
        //return the current enum entries
        protected override IReadOnlyDictionary<string, object> GetEntries()
        {
            var displays = new Dictionary<string, object>();

            foreach (var d in Recorder.GetDisplays())
            {
                displays[d.FriendlyName] = d.DeviceName;
            }

            return displays;
        }

        protected override IObservable<object> GetEntriesChangedObservable()
        {
            return Observable.Empty<object>();
        }
    }

    [Serializable]
    public class WindowsEnum : DynamicEnumBase<WindowsEnum, WindowsEnumDefinition>
    {
        public WindowsEnum(string value) : base(value)
        {
        }

        [CreateDefault]
        public static WindowsEnum CreateDefault()
        {
            //use method of base class if nothing special required
            return CreateDefaultBase();
        }
    }

    public class WindowsEnumDefinition : DynamicEnumDefinitionBase<WindowsEnumDefinition>
    {
        //return the current enum entries
        protected override IReadOnlyDictionary<string, object> GetEntries()
        {
            var windows = new Dictionary<string, object>();

            foreach (var d in Recorder.GetWindows())
            {
                windows[d.Title] = d;
            }

            return windows;
        }

        protected override IObservable<object> GetEntriesChangedObservable()
        {
            return Observable.Empty<object>();
        }
    }

    [Serializable]
    public class RecorderSourcesEnum : DynamicEnumBase<RecorderSourcesEnum, RecorderSourcesEnumDefinition>
    {
        public RecorderSourcesEnum(string value) : base(value)
        {
        }

        [CreateDefault]
        public static RecorderSourcesEnum CreateDefault()
        {
            //use method of base class if nothing special required
            return CreateDefaultBase();
        }
    }

    public class RecorderSourcesEnumDefinition : DynamicEnumDefinitionBase<RecorderSourcesEnumDefinition>
    {
        Subject<object> UpdateEnum = new Subject<object>();

        //return the current enum entries
        protected override IReadOnlyDictionary<string, object> GetEntries()
        {
            var sources = new Dictionary<string, object>();
            
            sources["Display: Main"] = DisplayRecordingSource.MainMonitor;

            foreach (var d in Recorder.GetDisplays().OrderBy(w => w.FriendlyName))
            {
                if (!string.IsNullOrWhiteSpace(d.FriendlyName))
                    sources["Display: " + d.FriendlyName] = d;
            }

            foreach (var w in Recorder.GetWindows().OrderBy(w => w.Title))
                sources["Window: " + w.Title] = w;

            foreach (var d in Recorder.GetSystemVideoCaptureDevices())
                sources["Devíce: " + d.FriendlyName] = d;
            
            return sources;
        }

        public void Update()
        {
            UpdateEnum.OnNext(null);
        }

        protected override IObservable<object> GetEntriesChangedObservable()
        {
            return UpdateEnum;
        }

        protected override bool AutoSortAlphabetically => false;
    }

    [Serializable]
    public class CaptureDeviceEnum : DynamicEnumBase<CaptureDeviceEnum, CaptureDeviceEnumDefinition>
    {
        public CaptureDeviceEnum(string value) : base(value)
        {
        }

        [CreateDefault]
        public static CaptureDeviceEnum CreateDefault()
        {
            //use method of base class if nothing special required
            return CreateDefaultBase();
        }
    }

    public class CaptureDeviceEnumDefinition : DynamicEnumDefinitionBase<CaptureDeviceEnumDefinition>
    {
        //return the current enum entries
        protected override IReadOnlyDictionary<string, object> GetEntries()
        {
            var devices = new Dictionary<string, object>();

            foreach (var d in Recorder.GetSystemVideoCaptureDevices())
            {
                devices[d.FriendlyName] = d.DeviceName;
            }

            return devices;
        }

        protected override IObservable<object> GetEntriesChangedObservable()
        {
            return Observable.Empty<object>();
        }
    }
}
