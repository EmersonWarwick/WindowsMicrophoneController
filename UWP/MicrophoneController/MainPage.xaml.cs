using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Devices;
using Windows.Media.Capture;
using System.Threading.Tasks;
using Windows.UI;
using System.Threading;
using Windows.UI.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MicrophoneController
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        AudioDeviceController adc;
        SolidColorBrush red = new SolidColorBrush(Color.FromArgb(200, 245, 0, 0));
        SolidColorBrush green = new SolidColorBrush(Color.FromArgb(200, 0, 205, 0));

        public MainPage()
        {
            this.InitializeComponent();
            Task.Run(() => GetAudioDeviceController());
        }

        private void MicrophoneControl_Click(object sender, RoutedEventArgs e)
        {
            if (adc != null)
            {
                adc.Muted = !adc.Muted;
                this.MicrophoneControl.Background = adc.Muted ? red : green;
            }
        }
        private async void GetAudioDeviceController()
        {
            MediaCapture mediaCapture = new MediaCapture();
            await mediaCapture.InitializeAsync(new MediaCaptureInitializationSettings
            {
                MediaCategory = MediaCategory.Speech,
                StreamingCaptureMode = StreamingCaptureMode.Audio
            });
            adc = mediaCapture.AudioDeviceController;
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                this.MicrophoneControl.Background = adc.Muted ? red : green;
            });
        }
    }
}
