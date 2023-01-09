namespace OfficeApiWithMongoDB.Helpers
{
    public class ImageHelper
    {
        public byte[] GetImages(string sBase64string)
        {
            byte[] bytes = default!;

            if (!string.IsNullOrEmpty(sBase64string)) 
            {
                bytes = Convert.FromBase64String(sBase64string);
            }

            return bytes;
        }
    }
}
