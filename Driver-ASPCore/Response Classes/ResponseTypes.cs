using Microsoft.Win32;

namespace ASCOMCore
{
    public static class ResponseTypes
    {
        // Enum to describe Camera.ImageArray and ImageArrayVCariant array types
        public enum ImageArrayElementTypes
        {
            Unknown = 0,
            Short = 1,
            Int = 2,
            Double = 3
        }
    }
}
