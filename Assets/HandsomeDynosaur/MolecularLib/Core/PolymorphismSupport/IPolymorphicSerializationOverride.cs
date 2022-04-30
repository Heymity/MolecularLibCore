using System.IO;

namespace MolecularLib.PolymorphismSupport
{
    public interface IPolymorphicSerializationOverride
    {
        object Deserialize(string reader);

        void Serialize(StringWriter writer);
    }
}