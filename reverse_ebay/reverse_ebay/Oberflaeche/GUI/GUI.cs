namespace reverse_ebay
{
    class GUI:IOberflaeche
    {
        private IFachkonzept fachkonzept;

        public GUI(IFachkonzept _fachkonzept)
        {
            this.fachkonzept = _fachkonzept;
        }

        public void start()
        {
            GUI_Main view = new GUI_Main(fachkonzept);
            view.ShowDialog();
        }
    }
}
