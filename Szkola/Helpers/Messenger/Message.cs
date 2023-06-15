using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szkola.Helpers.Messenger
{
    //Klasa jest stworzona na potrzeby zidentyfikowania wysyłanej wiadomosci do ktorej zakladki ją wysyłamy
    //Opis problemu: W przypadku gdy kilka zakladek pobiera element z tej samej zakladki, przy wysylaniu elementu każda zakładka ustawi go jako swój element pobrany
    public class Message<T>
    {
        public string messageInfo; //informacja do której zakładki wysyłana jest wiadomość
        public T element; //wysyłany element
        public Message(string messageInfo, T element)
        {
            this.messageInfo = messageInfo;
            this.element = element;
        }
    }
}
