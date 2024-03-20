mergeInto(LibraryManager.library, {
    InitializeSpeechRecognition: function (language) {
        useVoice(UTF8ToString(language));
    },
});
// using System.Runtime.InteropServices;
// public class WebGLVoiceToText : MonoBehaviour
// {
//     [DllImport("__Internal")]
//     private static extern void InitializeSpeechRecognition(string language);
// }
