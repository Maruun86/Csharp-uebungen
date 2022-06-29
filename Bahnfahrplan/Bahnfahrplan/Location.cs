namespace Bahnfahrplan
{
    class Location
    {
        public string name;
        public string id;

        public override string ToString()
        {
            return name;
        }
        public string Id
        {
            get { return id; }
            set { id = value; }
        }


    }
}
