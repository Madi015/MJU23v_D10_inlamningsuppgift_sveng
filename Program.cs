namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary;
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
        }
        static void Main(string[] args)
        {
            string path = @"C:\Users\Salim\source\repos\MJU23v_D10_inlamningsuppgift_sveng\dict\";
            string defaultFile = "sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            string command;
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                command = argument[0];
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                }
                else if (command == "help")
                {
                    PrintHelpCommands();
                }
                else if (command == "load")
                {
                    LoadAll(path, defaultFile, argument);
                }
                else if (command == "list")
                {
                    PrintOutTheList();
                }
                else if (command == "new")
                {
                    // TODO refactor the code.
                    if (argument.Length == 3)
                    {
                        dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                    }
                    else if(argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string newSwedishWord = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string newEnglishWord = Console.ReadLine();
                        dictionary.Add(new SweEngGloss(newSwedishWord, newEnglishWord));
                    }
                }
                else if (command == "delete")
                {
                    // TODO refactor code here
                    // FIXME It had to confirm for us wich word deleted 
                    // FIXME it has to tell us if this word doesn't exsist. 
                    if (argument.Length == 3)
                    {
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++) {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string deleteSwedishWord = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string deleteEnglishWord = Console.ReadLine();
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++)
                        {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == deleteSwedishWord && gloss.word_eng == deleteEnglishWord)
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                }
                else if (command == "translate")
                {
                    // FIXME It has to tell us if the word doesn't exsist. 
                    // TODO refactor code
                    if (argument.Length == 2)
                    {
                        foreach(SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == argument[1])
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == argument[1])
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word to be translated: ");
                        string wordToTranslate = Console.ReadLine();
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == wordToTranslate)
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == wordToTranslate)
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (command != "quit");
        }

        private static void PrintOutTheList()
        {
            foreach (SweEngGloss gloss in dictionary)
            {
                Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
            }
        }

        private static void LoadAll(string path, string defaultFile, string[] argument)
        {
            try
            {
                if (argument.Length == 2)
                {
                    using (StreamReader sr = new StreamReader(path + argument[1]))
                    {
                        LineReader(sr);
                    }
                }
                else if (argument.Length == 1)
                {
                    using (StreamReader sr = new StreamReader(path + defaultFile))
                    {
                        LineReader(sr);
                    }
                }
                else if (argument.Length >= 3)
                {
                    Console.WriteLine("you have to write just 'load' and 'thefile name' without space.");
                }
            }
            catch (System.IO.FileNotFoundException ) { Console.WriteLine(argument[1] +" dosen't exsist");}
            catch (System.IO.IOException) { Console.WriteLine("You have to write a correct file name, you cann't write a bad bath"); }

        }

        private static void LineReader(StreamReader sr)
        {
            dictionary = new List<SweEngGloss>(); // Empty it!
            string line = sr.ReadLine();
            while (line != null)
            {
                SweEngGloss gloss = new SweEngGloss(line);
                dictionary.Add(gloss);
                line = sr.ReadLine();
            }
        }

        private static void PrintHelpCommands()
        {
            Console.WriteLine("Here are all commands you can use: ");
            Console.WriteLine("quit:                                end this program");
            Console.WriteLine("load:                                load a file");
            Console.WriteLine("load 'filename':                     load a specific file");
            Console.WriteLine("list:                                list all words in the program");
            Console.WriteLine("new :                                add a new word");
            Console.WriteLine("new 'swedish word' ' english word':  add new word direkt");
            Console.WriteLine("delete:                              the programm will ask for which english and swedish would will you delete.");
            Console.WriteLine("delete 'swedísh word' 'english word':::delete a specific word direkt.");
            Console.WriteLine("translate:                           translate a word");
            Console.WriteLine("translate 'word':                    translate a specific word. ");
        }
    }
}