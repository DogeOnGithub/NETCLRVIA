using System;
using System.Threading;

namespace EventDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            MailManager manager = new MailManager();
            MailClient client = new MailClient();
            MailClient client2 = new MailClient();
            manager.NewMail += client.OnNewMail;
            manager.NewMail += client2.OnNewMail;
            manager.OnNewMail(new NewMailEventArgs("tj", "tj2", "test"));
            Console.ReadKey();
        }
    }

    class NewMailEventArgs : EventArgs
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }

        public NewMailEventArgs(string from, string to, string subject)
        {
            From = from;
            To = to;
            Subject = subject;
        }
    }

    class MailManager
    {
        public event EventHandler<NewMailEventArgs> NewMail;

        public void OnNewMail(NewMailEventArgs e)
        {
            EventHandler<NewMailEventArgs> temp = Volatile.Read(ref NewMail);
            if (temp != null)
            {
                temp(this, e);
            }
        }
    }

    class MailClient
    {
        public void OnNewMail(object obj, NewMailEventArgs args)
        {
            Console.WriteLine(args.From);
            Console.WriteLine(args.To);
            Console.WriteLine(args.Subject);
        }
    }
}
