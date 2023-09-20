namespace LB_4G
{
    public class C_DEPS
    {
        public int id { get; set; }
        public string region_code { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string slug { get; set; }

        public override string ToString()
        {
            return $"{name} {code}";
        }
    }
}
