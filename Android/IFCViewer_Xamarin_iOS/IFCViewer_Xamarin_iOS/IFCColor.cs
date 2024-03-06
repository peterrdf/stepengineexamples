using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFCViewer_Xamarin_iOS
{
    public class IFCColor
    {
        private float _R = 0.0f;
        private float _G = 0.0f;
        private float _B = 0.0f;

        public IFCColor(float color)
        {
            var colorBytes = BitConverter.GetBytes(color);

            uint iColor = BitConverter.ToUInt32(colorBytes);

            _R = (float)(iColor & ((uint)255 * 256 * 256 * 256)) / (256 * 256 * 256);
            _R /= 255.0f;

            _G = (float)(iColor & (255 * 256 * 256)) / (256 * 256);
            _G /= 255.0f;

            _B = (float)(iColor & (255 * 256)) / 256;
            _B /= 255.0f;
        }

        public float R
        {
            get => _R;
        }

        public float G
        {
            get => _G;
        }

        public float B
        {
            get => _B;
        }

        public void Log()
        {
            Debug.WriteLine(ToString());
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            IFCColor c = obj as IFCColor;
            if (c == null)
                return false;

            return (R == c.R) && (G == c.G) && (B == c.B);
        }

        public override int GetHashCode()
        {
            string hash = R.ToString() + "-" + G.ToString() + "-" + B.ToString();

            return hash.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("RGB: {0}, {1}, {2}", R, G, B);
        }
    }

    class IFCColorComparer : EqualityComparer<IFCColor>
    {
        public override bool Equals(IFCColor c1, IFCColor c2)
        {
            if (c1 == null && c2 == null)
                return true;
            else if (c1 == null || c2 == null)
                return false;

            return (c1.R == c2.R) && (c1.G == c2.G) && (c1.B == c2.B);
        }

        public override int GetHashCode(IFCColor c)
        {
            string hash = c.R.ToString() + "-" + c.G.ToString() + "-" + c.B.ToString();

            return hash.GetHashCode();
        }
    }
}

