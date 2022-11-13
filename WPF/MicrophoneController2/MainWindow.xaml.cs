using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NAudio.CoreAudioApi;

namespace MicrophoneController2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isMicMuted;

        public MainWindow()
        {
            InitializeComponent();
            // Set Mute to true at start
            SetMic(true);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Set Mute opposite to current status
            SetMic(!isMicMuted);
        }


        private void SetMic(bool targetMicMuteCondition)
        {
            isMicMuted = targetMicMuteCondition;

            MMDeviceEnumerator MMDE = new MMDeviceEnumerator();
            MMDeviceCollection devices = MMDE.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
            foreach (var device in devices)
            {
                device.AudioEndpointVolume.Mute = isMicMuted;
            }

            // Change button colour
            MicButton.Background = isMicMuted ? Brushes.Red : Brushes.Green;
        }
    }
}
