using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.CognitiveServices.Speech;
using Notarius.Client.DataModel;
using Windows.Media.Capture;
using Windows.System;
using Windows.UI.Xaml;

namespace Notarius.Client.Pages
{
    public partial class NotesBase:ComponentBase
    {

        public string Text { get; set; } = string.Empty;
        public PatientUI Patient { get; set; } = new PatientUI { MRN = "New", Firstname = "", Address = "", Lastname = "", City = "", State = "", Zip = "" };

        protected async void DictateClicked()
        {
            if (await CheckEnableMicrophoneAsync())
                RecognitionStart();

        }
        protected async Task HandleValidSubmit()
        {
           
        }
        private async Task<bool> CheckEnableMicrophoneAsync()
        {
            var isMicAvailable = false;

            try
            {
                var mediaCapture = new MediaCapture();
                var settings = new MediaCaptureInitializationSettings
                {
                    StreamingCaptureMode = StreamingCaptureMode.Audio
                };

                await mediaCapture.InitializeAsync(settings);
                isMicAvailable = true;
            }
            catch
            {
                await Launcher.LaunchUriAsync
                    (new Uri("ms-settings:privacy-microphone"));
            }

            return isMicAvailable;
        }
        private async void RecognitionStart()
        {
            const string SpeechSubscriptionKey = "";
            const string SpeechRegion = "";
            const string Culture = "";

            var isMicAvailable = await CheckEnableMicrophoneAsync();
            if (!isMicAvailable)
            {
                return;
            }

            //RecognitionButton.Content = "Recognizing...";
            //RecognitionButton.IsEnabled = false;
            //RecognitionTextBox.Text = string.Empty;

            var config = SpeechConfig.FromSubscription
                (SpeechSubscriptionKey, SpeechRegion);

            // Starts recognition. It returns when the first utterance has been 
            // recognized.
            using var cognitiveRecognizer = new SpeechRecognizer(config, Culture);

            var result = await cognitiveRecognizer.RecognizeOnceAsync();

            // Checks result.
            if (result.Reason == ResultReason.RecognizedSpeech)
            {
                Text = result.Text;
            }
            else
            {
               
            }

            //RecognitionButton.Content = "Start recognition";
            //RecognitionButton.IsEnabled = true;
        }
    }
}
