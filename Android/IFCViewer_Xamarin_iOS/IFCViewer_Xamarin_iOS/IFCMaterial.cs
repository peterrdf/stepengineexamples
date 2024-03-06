using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFCViewer_Xamarin_iOS
{
    public class IFCMaterial
    {
        public IFCMaterial(float ambient, float diffuse, float emissive, float specular)
        {
            Ambient = new IFCColor(ambient);
            Diffuse = new IFCColor(diffuse);
            Emissive = new IFCColor(emissive);
            Specular = new IFCColor(specular);

            var colorBytes = BitConverter.GetBytes(ambient);

            uint iColor = (uint)BitConverter.ToUInt32(colorBytes);

            float fA = (float)(iColor & (255)) / 255;

            A = fA;
        }

        public IFCColor Ambient
        {
            get;
            private set;
        } = null;

        public IFCColor Diffuse
        {
            get;
            private set;
        } = null;

        public IFCColor Emissive
        {
            get;
            private set;
        } = null;

        public IFCColor Specular
        {
            get;
            private set;
        } = null;

        public float A
        {
            get;
            private set;
        } = 1.0f;

        public int IndicesOffset
        {
            get;
            set;
        } = 0;

        public int IndicesCount
        {
            get;
            set;
        } = 0;

        public int MetalBufferID
        {
            get;
            set;
        } = -1;

        public void Log()
        {
            Debug.WriteLine(string.Format("Ambient: {0}, Diffuse: {1}, Emissive: {2}, Specular: {3}, A: {4}", Ambient.ToString(), Diffuse.ToString(), Emissive.ToString(), Specular.ToString(), A));
        }
    }

    class IFCMaterialComparer : EqualityComparer<IFCMaterial>
    {
        public override bool Equals(IFCMaterial m1, IFCMaterial m2)
        {
            if (m1 == null && m2 == null)
                return true;
            else if (m1 == null || m2 == null)
                return false;

            return m1.Ambient.Equals(m2.Ambient) &&
                m1.Diffuse.Equals(m2.Diffuse) &&
                m1.Emissive.Equals(m2.Emissive) &&
                m1.Specular.Equals(m2.Specular) &&
                (m1.A == m2.A);
        }

        public override int GetHashCode(IFCMaterial m)
        {
            string hash = m.Ambient.ToString() + "-" +
                m.Diffuse.ToString() + "-" +
                m.Emissive.ToString() + "-" +
                m.Specular.ToString() + "-" +
                m.A.ToString();

            return hash.GetHashCode();
        }
    }
}

