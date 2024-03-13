using Firebase.Auth;
using Firebase.Storage;
using System.Security.Cryptography;

namespace TravelsProject2024.WEB.Helpers
{
    public class ImageHelper
    {
        public static async Task<string> SubirArchivo(Stream archivo, string nombre)
        {
            string fileName = GetRandomNumber().ToString() + nombre;
            string email = "renesaandoval22@gmail.com";
            string clave = "Sandoval22!";
            string ruta = "touristplaces2024.appspot.com";
            string api_key = "AIzaSyBncagmg3P_GBDLS94ZBOZl8BBdhgGagBQ";

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(api_key));
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, clave);

            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                ruta,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(auth.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child("Travels_Image")
                .Child(fileName)

                .PutAsync(archivo, cancellation.Token);

            try
            {
                var downloadURL = await task;
                return downloadURL;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return "";
        }

        static int GetRandomNumber()
        {
            using (RNGCryptoServiceProvider rngCrypt = new RNGCryptoServiceProvider())
            {
                byte[] tokenBuffer = new byte[4];
                rngCrypt.GetBytes(tokenBuffer);
                return BitConverter.ToInt32(tokenBuffer, 0);
            }
        }
    }
}
