using Android.App;
using Android.Widget;
using Android.OS;

using System.Security.Cryptography;

using System.Diagnostics;
using PCLCrypto;


using Android.Content;

namespace DigestHash
{
    [Activity(Label = "DigestHash", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            UpdateDigest();


            EditText et = FindViewById<EditText>(Resource.Id.etMessage);

            et.AddTextChangedListener(new ActionTextWatcher(null,null,(s, start, before,count)=>{
                UpdateDigest();
            } ));


            FindViewById<Button>(Resource.Id.btnCopyMd5).Click += (sender, e) => {
                CopyHashToClipboard(PCLCrypto.HashAlgorithm.Md5);
            };


            FindViewById<Button>(Resource.Id.btnCopySha1).Click += (sender, e) => {
                CopyHashToClipboard(PCLCrypto.HashAlgorithm.Sha1);
            };

            FindViewById<Button>(Resource.Id.btnCopySha256).Click += (sender, e) => {
                CopyHashToClipboard(PCLCrypto.HashAlgorithm.Sha256);
            };

            FindViewById<Button>(Resource.Id.btnCopySha384).Click += (sender, e) => {
                CopyHashToClipboard(PCLCrypto.HashAlgorithm.Sha384);
            };

            FindViewById<Button>(Resource.Id.btnCopy512).Click += (sender, e) => {
                CopyHashToClipboard(PCLCrypto.HashAlgorithm.Sha512);
            };

            FindViewById<Button>(Resource.Id.btnClearClipboard).Click += (sender, e) => {
                CopyToClipboard("");
            };
        }

        protected void UpdateDigest()
        {

            var original = FindViewById<EditText>(Resource.Id.etMessage).Text;


            var md5 = GetHash(PCLCrypto.HashAlgorithm.Md5, original);
            var sha1 = GetHash(PCLCrypto.HashAlgorithm.Sha1, original);
            var sha256 = GetHash(PCLCrypto.HashAlgorithm.Sha256, original);
            var sha384 = GetHash(PCLCrypto.HashAlgorithm.Sha384, original);
            var sha512 = GetHash(PCLCrypto.HashAlgorithm.Sha512, original);

            FindViewById<TextView>(Resource.Id.md5Digest).Text = md5;
            FindViewById<TextView>(Resource.Id.sha1Digest).Text = sha1;
            FindViewById<TextView>(Resource.Id.sha256Digest).Text = sha256;
            FindViewById<TextView>(Resource.Id.sha384Digest).Text = sha384;
            FindViewById<TextView>(Resource.Id.sha512Digest).Text = sha512;

        }


        protected void CopyHashToClipboard(PCLCrypto.HashAlgorithm alg)
        {
            var original = FindViewById<EditText>(Resource.Id.etMessage).Text;

            string copyValue = GetHash(alg, original);

            CopyToClipboard(copyValue);
        }

        protected void CopyToClipboard(string copyValue)
        {
            ClipboardManager mgr = (ClipboardManager)GetSystemService(Context.ClipboardService);
            ClipData data = ClipData.NewPlainText("hash", copyValue);
            mgr.PrimaryClip = data;
        }

        public static string ByteArrayToString(byte[] ba)
        {
            string hex = System.BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        public static string GetHash(PCLCrypto.HashAlgorithm alg, string input)
        {
            var hasher = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(alg);
            string hexStr = ByteArrayToString(hasher.HashData(System.Text.UTF8Encoding.UTF8.GetBytes(input)));

            return hexStr.ToLower();
        }
    }
}

