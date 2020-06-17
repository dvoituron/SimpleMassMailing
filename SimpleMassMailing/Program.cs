using SimpleMassMailing.Data;
using System;
using System.Linq;

namespace SimpleMassMailing
{
    public class Program
    {
        static void Main(string[] args)
        {
            const int confirmationPosition = 80;

            var config = new Configuration(args);
            var content = System.IO.File.ReadAllText(config.ContentFile);
            var dataRows = System.IO.File.ReadAllLines(config.DataFile).Select(row => new DataRow(row)).Where(row => !string.IsNullOrEmpty(row.EMail));
            var mail = new OutlookDotComMail(config);

            int rowNumber = 0;
            int rowTotal = dataRows.Count();
            foreach (var row in dataRows)
            {
                rowNumber++;
                string confirmation = $"{rowNumber}/{rowTotal}. Send to {row.EMail.Left(confirmationPosition-22)} (Y/N)? ";
                Console.Write(confirmation);
                if (row.IsEnabled)
                {
                    var key = Console.ReadKey();
                    if (key.KeyChar == 'Y' || key.KeyChar == 'y')
                    {
                        mail.SendMail(row.EMail, content, row.Parameters);
                        Console.WriteLine($"{String.Empty.PadLeft(confirmationPosition - confirmation.Length, ' ')} ... Sent");
                    }
                    else
                    {
                        Console.WriteLine($"{String.Empty.PadLeft(confirmationPosition - confirmation.Length, ' ')} ... Skipped");
                    }
                }
                else
                {
                    Console.WriteLine($"{String.Empty.PadLeft(confirmationPosition - confirmation.Length, ' ')}  ... Commented");
                }
            }

            Console.WriteLine("Completed");
            if (System.Diagnostics.Debugger.IsAttached)
                Console.ReadLine();
        }

      
    }
}
