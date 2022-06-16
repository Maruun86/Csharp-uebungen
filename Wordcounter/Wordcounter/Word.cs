using System;
using System.Collections.Generic;
using System.Text;

namespace Wordcounter
{
    class Word
    {

        string text;
        uint count;
        public Word(string text)
        {
            this.text = text;
            this.count = 1;
        }
        public string Text
        {
          get { return text; }
        }
        public void setText(string text)
        {
           this.text = text;
        }
        public uint Count
        {
            get { return count; }
        }
        public void AddCount(uint optionalCount = 1)
        {
            this.count += optionalCount;
        }
       
        
    }
public class MyItem
    {
        public string Word { get; set; }

        public uint Anzahl { get; set; }
    }

}
