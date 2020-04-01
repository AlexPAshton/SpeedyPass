namespace CustomUnity
{
    public class InjectionMap
    {
        public string TypeName { get => this.typeName; set => this.typeName = value; }
        public string MapsTo { get => this.mapsTo; set => this.mapsTo = value; }

        private string typeName;
        private string mapsTo;
    }
}
