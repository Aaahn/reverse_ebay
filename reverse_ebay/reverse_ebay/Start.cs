namespace reverse_ebay
{
    class Start
    {
        static void Main(string[] args)
        {
            var programm = new TUI(new Fachkonzept1(new XMLDatenzugriff(@"C:\reverse_ebay\xml\")));

            programm.start();
        }
    }
}
