using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace muno {

    class Entry {
        public String word1;
        public String word2;
        public String gen;

        public String toString(){
            return word1 + "," + word2 + "," + gen + "\n";
        }

        public Entry(String word1, String word2, String gen){
            this.word1 = word1;
            this.word2 = word2;
            this.gen = gen;
        }
    }

    class Program {
        static void Main(string[] args) {
            const String appid = "dj0zaiZpPWJNVzZhcEplcTFsOCZzPWNvbnN1bWVyc2VjcmV0Jng9MGM-";
            List<Entry> table = new List<Entry>();
            while (true) {
                String str = Console.ReadLine();
                if (str.Equals("end")) break;
                String url = "http://jlp.yahooapis.jp/MAService/V1/parse?appid=" + appid + "&results=ma&sentence=" + Uri.EscapeUriString(str);

                List<String> list = new List<String>();

                XmlReader reader = XmlReader.Create(url);
                int i = 0;
                while (reader.Read()) {
                    if (reader.NodeType == XmlNodeType.Element && reader.LocalName.Equals("surface")) {
                        list.Add(reader.ReadString());
                    }
                }

                table.Add(new Entry("--BEGIN--", list[0], list[1]));
                for (i = 0; i < list.Count - 2; i++) {
                    table.Add(new Entry(list[i], list[i + 1], list[i + 2]));
                }
                table.Add(new Entry(list[list.Count - 2], list[list.Count - 1], "--END--"));

            }
            /*
            for (int i = 0; i < table.Count; i++) {
                Console.Write(table[i].toString());
            }
            */
            while (true) {
                String res = "";
                List<Entry> elm = new List<Entry>();
                for (int i = 0; i < table.Count; i++) {
                    if (table[i].word1.Equals("--BEGIN--")) elm.Add(table[i]);
                }
                int rnd = new System.Random().Next(elm.Count);
                res = elm[rnd].word2 + elm[rnd].gen;
                String s1 = elm[rnd].word2;
                String s2 = elm[rnd].gen;

                while (true) {
                    elm.Clear();
                    for (int i = 0; i < table.Count; i++) {
                        if (table[i].word1.Equals(s1) && table[i].word2.Equals(s2)) elm.Add(table[i]);
                    }
                    if (elm.Count == 0) break;
                    rnd = new System.Random().Next(0, elm.Count);
                    if (elm[rnd].gen.Equals("--END--")) break;
                    res += elm[rnd].gen;
                    s1 = elm[rnd].word2;
                    s2 = elm[rnd].gen;
                }
                Console.WriteLine(res);
                String st = Console.ReadLine();
                if (st.Equals("end")) break;
            }
        }
    }
}
