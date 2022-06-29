namespace Bahnfahrplan
{
    class ADTime
    {
        public string name;
        public string type;
        public string dateTimeArrival;
        public string dateTimeDeparture;
        public string origin;
        public string track;

        public override string ToString()
        {
            FormatDateTime();
            return name + " : " + type + "\n"
                + "Ziel: " + origin + "\n"
                + "Ankunft: " + dateTimeArrival
                + "\n"
                + "Abfahrt: " + dateTimeDeparture
                + "\n"
                + "Gleis: " + track;
        }
        private void FormatDateTime()
        {
            string[] split = dateTimeArrival.Split("T");
            if (split.Length > 1)
            {
                dateTimeArrival = split[1];
                split = dateTimeDeparture.Split("T");
                dateTimeDeparture = split[1];
            }
        }
    }
}
