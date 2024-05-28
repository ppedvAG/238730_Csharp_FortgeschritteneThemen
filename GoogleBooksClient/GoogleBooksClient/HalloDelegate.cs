


namespace GoogleBooksClient
{
    delegate void EinfacherDelegate();
    delegate void DeleMitPara(string text);
    delegate long CalcDelegate(int a, int b);

    class HalloDelegate
    {
        public HalloDelegate()
        {
            EinfacherDelegate meinDele = EinfacheMethode;
            Action einfacheMethodeAlsAction = EinfacheMethode;
            Action einfacheAction = delegate () { Console.WriteLine("Hallo"); };
            Action einfacheAction2 = () => { Console.WriteLine("Hallo"); };
            Action einfacheAction3 = () => Console.WriteLine("Hallo");

            DeleMitPara deleMitPara = MethodeMitPara;
            Action<string> methodeMitPara = MethodeMitPara;
            DeleMitPara deleMitParaAno = delegate (string txt) { Console.WriteLine(txt); };
            DeleMitPara deleMitParaAno2 = (string txt) => { Console.WriteLine(txt); };
            DeleMitPara deleMitParaAno3 = (txt) => Console.WriteLine(txt);
            DeleMitPara deleMitParaAno4 = x => Console.WriteLine(x);

            CalcDelegate calcDele = Multi;
            Func<int, int, long> calcFuncDele = Sum;
            CalcDelegate calcDeleAno = delegate (int a, int b) { return a + b; };
            CalcDelegate calcDeleAno2 = (int a, int b) => { return a + b; };
            CalcDelegate calcDeleAno3 = (a, b) => { return a + b; };
            CalcDelegate calcDeleAno4 = (a, b) => a + b;

            var texte = new List<string>();

            texte.Where(x => x.StartsWith("b"));
            texte.Where(Filter);
        }

        private bool Filter(string arg)
        {
            if (arg.StartsWith("b"))
                return true;
            else
                return false;
        }

        private long Multi(int a, int b)
        {
            return a * b;
        }

        private long Sum(int a, int b)
        {
            return a + b;
        }

        private void MethodeMitPara(string name)
        {
            Console.WriteLine("Hallo" + name);
        }

        private void EinfacheMethode()
        {
            Console.WriteLine("Hallo");
        }
    }
}
