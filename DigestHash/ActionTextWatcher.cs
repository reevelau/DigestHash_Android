using System;

using Android.Text;
using Java.Lang;

namespace DigestHash
{
    public class ActionTextWatcher : Java.Lang.Object, ITextWatcher
    {
        Action<IEditable> MyAfterTextChanged;
        Action<ICharSequence, int, int, int> MyBeforeTextChanged;
        Action<ICharSequence, int, int, int> MyOnTextChange;
        
        public ActionTextWatcher(
            Action<IEditable> afterTextChanged, 
            Action<ICharSequence,int,int,int> beforeTextChanged, 
            Action<ICharSequence,int, int, int> onTextChanged)
        {
            MyAfterTextChanged = afterTextChanged;
            MyBeforeTextChanged = beforeTextChanged;
            MyOnTextChange = onTextChanged;
        }

        public void AfterTextChanged(IEditable s)
        {
            MyAfterTextChanged?.Invoke(s);
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
            MyBeforeTextChanged?.Invoke(s, start, count, after);
        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            MyOnTextChange?.Invoke(s, start, before, count);
        }
    }
}
