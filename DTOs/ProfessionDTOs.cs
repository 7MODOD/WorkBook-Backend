namespace WorkBook.DTOs
{
    public record ProfessionRes(int id, string name);
    
    

    public class ProfessionWorkers
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<WorkersForProfessionRes> workers { get; set; }
    }

    public class TopProfessionWorkers
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<TopWorkersForProfessionRes> Workers { get; set; }
    }

    public class TopWorkersForProfessionRes
    {
        public int id { get; set; }
        public string name { get; set; }
        public double avg_rate { get; set; }
        public List<string> descriptions { get; set; }


    }
    public class WorkersForProfessionRes
    {
        public int id { get; set; }
        public string name { get; set; }
    }


}
